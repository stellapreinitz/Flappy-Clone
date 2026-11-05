using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    private GameManager gamemanager;

    private BoxCollider2D colliderZone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        colliderZone = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        colliderZone.enabled = false;
        gamemanager.AddScore(1);
        Debug.Log("Score");
        Debug.Log(GetEntityId());
    }
}
