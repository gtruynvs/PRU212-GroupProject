using UnityEngine;

public class PointExploder : MonoBehaviour
{
    private Animator animator;
    private bool hasExploded = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ExplodeAndDestroy()
    {
        if (hasExploded) return;

        hasExploded = true;
        animator.SetTrigger("Explode");
        Destroy(gameObject, 0f);
    }
}
