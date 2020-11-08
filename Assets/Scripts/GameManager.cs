using System;
using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int EnemiesLeft = 0, Wave = 0;
    public Transform EnemySpowner;
    public Animator FinishAnimator;
    static Animator finishAnimator;

    [SerializeField]
    public Wave[] Waves;

    public enum finishMode { Play, Defeat, Win};
    public static finishMode FinishMode = finishMode.Play;
    public static bool GamePaused = false, NearDefeat = false;

    private void Start()
    {
        finishAnimator = FinishAnimator;
    }

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
        yield return new WaitForSeconds(5f); // between waves
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
            if (Wave < Waves.Length) StartCoroutine(StartWaveIEnumerator());
            else Finish(finishMode.Win);
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

    public static void Finish(finishMode mode)
    {
        if (FinishMode == finishMode.Play)
        {
            if (mode == finishMode.Defeat)
            {
                FinishMode = finishMode.Defeat;
                Debug.LogWarning("Defeat");
                finishAnimator.SetTrigger("defeat");
            }
            else if (mode == finishMode.Win)
            {
                FinishMode = finishMode.Win;
                Debug.LogWarning("Win");
                finishAnimator.SetTrigger("win");
            }
        }
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
