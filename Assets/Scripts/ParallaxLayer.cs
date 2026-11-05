using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1f;
    [SerializeField] private float seamOverlap = 0.05f; // world units of deliberate tile overlap, prevents sub-pixel seam gaps

    private GameManager.GameState currentState;
    private SpriteRenderer sprite;
    private float tileHalfWidth;
    private float cameraHalfWidth;
    private float tileSpacing; // distance between tile centers, tightened by seamOverlap

    [SerializeField] private bool isOriginal = true;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        tileHalfWidth = (sprite.bounds.size.x / 2);
        cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        tileSpacing = (tileHalfWidth * 2) - seamOverlap;

        if (isOriginal)
        {
            CloneTile();
        }
    }

    void Update()
    {
        if (currentState != GameManager.GameState.GameOver)
        {
            transform.Translate(new Vector3(scrollSpeed * -1, 0, 0) * Time.deltaTime);
        }

        if (transform.position.x < -(cameraHalfWidth + tileHalfWidth))
        {
            transform.Translate(new Vector3(tileSpacing * 2, 0, 0));
        }
    }

    public void OnEnable()
    {
        GameManager.OnStateChanged += HandleStateChanged;
    }

    public void OnDisable()
    {
        GameManager.OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(GameManager.GameState newState)
    {
        currentState = newState;
    }

    private void CloneTile()
    {
        isOriginal = false;
        Instantiate(this.gameObject, transform.position + new Vector3(tileSpacing, 0, 0), transform.rotation);
    }
}