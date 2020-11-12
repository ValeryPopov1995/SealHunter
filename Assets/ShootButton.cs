using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShootButton : MonoBehaviour
{
    PlayerParams pars;
    Button btn;

    private void Start()
    {
        pars = FindObjectOfType<PlayerParams>();
        btn = GetComponent<Button>();
    }
}
