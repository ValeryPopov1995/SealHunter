using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int EnemiesLeft = 0, Wave = 0;
    public Transform EnemySpowner;

    [SerializeField]
    public Wave[] Waves;

    bool GamePaused = false, NearDefeat = false;

    private void Update()
    {
        if (GamePaused)
        {
            if (Time.timeScale > .1f) Time.timeScale -= Time.unscaledDeltaTime;
        }
        else
        {
            if (NearDefeat)
            {
                if (Time.timeScale > .5f) Time.timeScale -= Time.unscaledDeltaTime;
            }
            else
            {
                if (Time.timeScale < 1) Time.timeScale += Time.unscaledDeltaTime;
            }
        }
    }

    public void StartWaves()
    {
        StartCoroutine(StartWaveIEnumerator());
    }

    IEnumerator StartWaveIEnumerator()
    {
        for (int i = 0; i < Waves[Wave].Enemies.Length; i++)
        {
            for (int j = 0; j < Waves[Wave].Enemies[i].Count; j++)
            {
                Instantiate(Waves[Wave].Enemies[i].EnemyPrefab, EnemySpowner.position, Quaternion.identity);
                yield return new WaitForSeconds(.9f); // between spowning
            }
        }
    }

    public void ChangeEnemiesLeft()
    {
        EnemiesLeft--;
        if (EnemiesLeft == 0)
        {
            Wave++;
            StartCoroutine(StartWaveIEnumerator());
        }
    }

    public void SetPaused(bool state)
    {
        GamePaused = state;
    }

    public void Quit()
    {
        Application.Quit();
    }
}

[Serializable]
public class Wave
{
    public string WaveName = "Wave #";
    [SerializeField]
    public EnemyWavePrefab[] Enemies;
}

[Serializable]
public class EnemyWavePrefab
{
    public string EnemyName = "Enemy #";
    public GameObject EnemyPrefab;
    public int Count;
}
