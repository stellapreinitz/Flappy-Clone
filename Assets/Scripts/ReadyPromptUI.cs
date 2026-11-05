using UnityEngine;

public class ReadyPromptUI : MonoBehaviour
{

    [SerializeField] TMPro.TextMeshProUGUI readyText;

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
        readyText.enabled = (newState == GameManager.GameState.Ready);
    }
}