using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    [Range(.01f, .99f)]
    public float Lerp;
    public Transform StartPosition;

    Animator animator;
    Transform position;

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
