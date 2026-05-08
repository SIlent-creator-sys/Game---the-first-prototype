using UnityEngine;

public class spideranim : MonoBehaviour
{
    Animator animator;
    void Update()
    {
        animator = GetComponent<Animator>();
        // animator.Play();
    }
}
