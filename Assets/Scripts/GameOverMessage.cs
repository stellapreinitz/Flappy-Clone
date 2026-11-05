using UnityEngine;

public class GameOverMessage : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI gameOverText;

    void OnEnable()
    {
        GameManager.OnStateChanged += HandleStateChanged;
    }

    void OnDisable()
    {
        GameManager.OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(GameManager.GameState newState)
    {
        gameOverText.enabled = (newState == GameManager.GameState.GameOver);
    }
}