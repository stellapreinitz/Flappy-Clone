using UnityEngine;

public class PipeSpawner : MonoBehaviour
{

    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private float spawnInterval = 2f;

    private float timeElapsed = 0f;
    private float randomHeight = 0f;

    private GameManager.GameState currentState;
    private float spawnX;

    void Start()
    {
            spawnX = GameManager.HalfWidth + 2;
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
    void Update()
    {
        if (currentState == GameManager.GameState.Playing)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > spawnInterval)
            {
                randomHeight = Random.Range(-4, 4);
                GameObject newPipe = Instantiate(pipePrefab, new Vector3(spawnX, randomHeight, 0), Quaternion.identity);
                PipeMovement pipeMovement = newPipe.GetComponent<PipeMovement>();
                pipeMovement.SetState(GameManager.GameState.Playing);
                timeElapsed = 0;
            }
        }
    }

    private void HandleStateChanged(GameManager.GameState newState)
    {
        currentState = newState;
    }
}
