using UnityEngine;

public class PipeMovement : MonoBehaviour
{

    [SerializeField] private float speed = 5f;

    private float despawnX;

    private GameManager.GameState currentState;

    void Start()
    {
        despawnX = GameManager.HalfWidth + 2;
    }
    public void SetState(GameManager.GameState newState)
    {
        currentState = newState;
    }

    void OnEnable()
    {
        GameManager.OnStateChanged += HandleStateChanged;
    }

    void OnDisable()
    {
        GameManager.OnStateChanged -= HandleStateChanged;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentState == GameManager.GameState.Playing)
        {
            transform.Translate(new Vector3(speed * -1, 0, 0) * Time.deltaTime);
            if (transform.position.x < -despawnX)
            {
                Destroy(gameObject);
            }
        }
    }

    private void HandleStateChanged(GameManager.GameState newState)
    {
        currentState = newState;
    }
}
