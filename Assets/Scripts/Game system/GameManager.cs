using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int EnemiesLeft = 0, Wave = 0;
    public Transform[] EnemySpowners;
    public Animator FinishAnimator, NextWave;
    //public GameObject NewEnemyMassage;
    [Space]
    [SerializeField]
    public Wave[] Waves;
    [Space]
    public ObjectsToActive[] SetActiveObjectsAfterRestart;

    public enum finishMode { Play, Defeat, Win};
    public static finishMode FinishMode = finishMode.Play;
    PlayerParams player;
    static SoundEvents soundEvents;
    public static bool GamePaused = false, NearDefeat = false, Defeat = false, FirstStart = true;

    private void Start()
    {
        Application.targetFrameRate = 30;

        player = FindObjectOfType<PlayerParams>();
        soundEvents = FindObjectOfType<SoundEvents>();
        EnemiesLeft = 0;
        Wave = 0;

        if (!FirstStart)
        {
            FinishMode = finishMode.Play;
            foreach (var item in SetActiveObjectsAfterRestart) item.Object.SetActive(item.SetActive);
            StartWaves();
            SetPaused(false);
        }
    }

    private void Update()
    {
        #region time scaling
        if (GamePaused)
        {
            if (Time.timeScale > .3f) Time.timeScale -= Time.unscaledDeltaTime;
        }
        else
        {
            if (Defeat)
            {
                if (Time.timeScale < 1) Time.timeScale += Time.unscaledDeltaTime;
            }
            else
            {
                if (NearDefeat)
                {
                    if (Time.timeScale > .6f) Time.timeScale -= Time.unscaledDeltaTime;
                }
                else if (Time.timeScale < 1) Time.timeScale += Time.unscaledDeltaTime;
            }

        }
        #endregion
    }

    public void StartWaves()
    {
        StartCoroutine(StartWaveIEnumerator());
        FirstStart = false;
    }

    IEnumerator StartWaveIEnumerator()
    {
        NextWave.SetTrigger("play");
        player.TellReplica(Waves[Wave].PlayerReplica);

        yield return new WaitForSeconds(5f); // between waves
        soundEvents.NextWave();
        Debug.Log(Wave + " wave begun");

        for (int i = 0; i < Waves[Wave].Enemies.Length; i++)
        {
            for (int j = 0; j < Waves[Wave].Enemies[i].Count; j++)
            {
                var pos = EnemySpowners[UnityEngine.Random.Range(0, EnemySpowners.Length)];
                Instantiate(Waves[Wave].Enemies[i].EnemyPrefab, pos.position, pos.rotation);
                yield return new WaitForSeconds(UnityEngine.Random.Range(.3f, 1.5f)); // between spowning
            }
        }
    }

    public void ChangeEnemiesLeft()
    {
        EnemiesLeft--;
        //Debug.Log(EnemiesLeft + "enemies left");
        if (EnemiesLeft == 1) StartCoroutine(soundEvents.SetBattleLoopVolume(0f));
        if (EnemiesLeft == 0)
        {
            Wave++;
            soundEvents.WinWave();
            if (Wave < Waves.Length) StartCoroutine(StartWaveIEnumerator());
            else Finish(finishMode.Win);
        }
    }

    public void SetPaused(bool state)
    {
        GamePaused = state;
        Debug.Log("paused: " + state);
    }

    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    public void Finish(finishMode mode)
    {
        if (FinishMode == finishMode.Play)
        {
            if (mode == finishMode.Defeat)
            {
                FinishMode = finishMode.Defeat;
                Debug.LogWarning("Defeat!");
                FinishAnimator.SetTrigger("defeat");
                soundEvents.DefeatWave();
            }
            else if (mode == finishMode.Win)
            {
                FinishMode = finishMode.Win;
                Debug.LogWarning("Win!");
                FinishAnimator.SetTrigger("win");
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}

[Serializable]
public class Wave
{
    public string WaveName = "Wave #", PlayerReplica;
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

[Serializable]
public class ObjectsToActive
{
    public GameObject Object;
    public bool SetActive = true;
}
