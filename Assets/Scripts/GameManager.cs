using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    [SerializeField] private int score = 0;
    [SerializeField] private AudioSource scoreSound;

    [SerializeField] private AnimationCurve popCurve;
    [SerializeField] private float popDuration = 0.2F;
    [SerializeField] private float popStrength = 0.3f;

    public static float HalfWidth { get; private set; }

    void Awake()
    {
        HalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        Debug.Log(GameManager.HalfWidth);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private IEnumerator ScorePop()
    {
        float t = 0f;
        while (t < popDuration)
        {
            t += Time.deltaTime;
            float scale = 1f + popCurve.Evaluate(t / popDuration) * popStrength;
            scoreText.transform.localScale = Vector3.one * scale;
            yield return null;
        }
        scoreText.transform.localScale = Vector3.one;
    }

    public enum GameState { Ready, Playing, GameOver }

    public static event Action<GameState> OnStateChanged;
    private GameState currentState = GameState.Ready;
    public void SetState(GameState newState)
    {
        currentState = newState;
        OnStateChanged?.Invoke(newState);
    }
    public void AddScore(int amount)
    {
        score += amount;
        StartCoroutine(ScorePop());
        scoreSound.Play();
        Debug.Log(score);
        scoreText.text = "Score: " + score.ToString();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetState(GameState.Ready);
    }
}