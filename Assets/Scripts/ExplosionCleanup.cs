using UnityEngine;

public class ExplosionCleanup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Animator anim = GetComponent<Animator>();
        float clipLength = anim.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, clipLength);
    }
}
