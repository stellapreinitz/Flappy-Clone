using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdBehaviour : MonoBehaviour

{
    [SerializeField] private float flapForce = 5f;
    [SerializeField] private float maxAngle = 45f;
    [SerializeField] private float minAngle = -90f;
    [SerializeField] private float someMultiplier = 1f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private AudioSource birdAudio;
    [SerializeField] private AudioSource death;
    [SerializeField] private float deathInputPause = 1f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private PolygonCollider2D polyCollider;

    public bool flapping = false;

    private GameManager gameManager;
    private GameManager.GameState currentState;
    private float timeDeath;

    private string scene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("VSync Count: " + QualitySettings.vSyncCount);
        Debug.Log("Target Frame Rate " + Application.targetFrameRate);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        polyCollider = GetComponent<PolygonCollider2D>();
    }

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scene = SceneManager.GetActiveScene().name;
}

    public void OnEnable()
    {
        GameManager.OnStateChanged += HandleStateChanged;
    }

    public void OnDisable()
    {
        GameManager.OnStateChanged -= HandleStateChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0)))
        {
            if (currentState == GameManager.GameState.Ready || currentState == GameManager.GameState.Playing)
            {
                if (currentState == GameManager.GameState.Ready)
                {
                    gameManager.SetState(GameManager.GameState.Playing);
                }
                rb.linearVelocity = Vector2.up * flapForce;
                StartCoroutine(FlapReset());
            }
            else if (currentState == GameManager.GameState.GameOver && Time.time > timeDeath + deathInputPause)
            {
                SceneManager.LoadScene(scene);
            }
        }

        float targetAngle = Mathf.Clamp(rb.linearVelocity.y * someMultiplier, minAngle, maxAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation ,Quaternion.Euler(0, 0, targetAngle), rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    private IEnumerator FlapReset()
    {
        flapping = true;
        anim.SetBool("flapping", flapping);
        birdAudio.Play();
        yield return new WaitForSeconds(0.15f);
        flapping = false;
        anim.SetBool("flapping", flapping);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        cameraShake.StartCoroutine(cameraShake.Shake(0.4f, 3f));
        death.Play();
        sprite.enabled = false;
        anim.enabled = false;
        polyCollider.enabled = false;
        gameManager.SetState(GameManager.GameState.GameOver);
        Debug.Log("Collision");
    }

    private void HandleStateChanged(GameManager.GameState newState)
    {
        currentState = newState;
        if (currentState != GameManager.GameState.Playing)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
        Debug.Log("State changed to: " + newState);

        if (currentState == GameManager.GameState.GameOver)
        {
            timeDeath = Time.time;
        }
    }

}
