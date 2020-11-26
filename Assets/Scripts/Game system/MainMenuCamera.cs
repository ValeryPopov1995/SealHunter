using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (!GameManager.FirstStart) animator.SetTrigger("start");
    }

    public void PlayModePosition()
    {
        animator.SetTrigger("start");
    }
}
