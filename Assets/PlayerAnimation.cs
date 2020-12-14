using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    FixedJoystick stick;

    private void Start()
    {
        anim = GetComponent<Animator>();
        stick = GetComponentInParent<PlayerMovenment>().FixedStick;
    }

    private void Update()
    {
        anim.SetFloat("BlendX", stick.Horizontal);
        anim.SetFloat("BlendY", stick.Vertical);
    }
}
