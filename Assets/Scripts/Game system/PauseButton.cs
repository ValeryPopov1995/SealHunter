using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseButton : MonoBehaviour
{
    [SerializeField]
    public setactive[] SetActive;
    static bool isAndroid = false;
    GameManager manager;

    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android) isAndroid = true;
        manager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isAndroid)
        {
            foreach (var item in SetActive)
            {
                item.target.SetActive(item.active);
            }
            manager.SetPaused(true);
        }
    }
}

[Serializable]
public class setactive
{
    public GameObject target;
    public bool active;
}
