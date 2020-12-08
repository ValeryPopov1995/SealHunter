using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundEvents : MonoBehaviour
{
    public AudioClip[] WinWaves, Defeat, BuyWeapon, Battle, StartWave;
    [Space]
    public AudioClip HornNextWave;
    public AudioMixerGroup Sound, Effects;

    AudioSource effsource, battleloop;

    private void Start()
    {
        effsource = gameObject.AddComponent<AudioSource>();
        effsource.loop = false;
        effsource.playOnAwake = false;
        effsource.outputAudioMixerGroup = Effects;

        battleloop = gameObject.AddComponent<AudioSource>();
        battleloop.loop = true;
        battleloop.volume = 0;
        battleloop.outputAudioMixerGroup = Sound;
        battleloop.clip = Battle[Random.Range(0, Battle.Length)];
    }

    public void WinWave()
    {
        effsource.clip = WinWaves[Random.Range(0, WinWaves.Length)];
        effsource.Play();
    }

    public void DefeatWave()
    {
        effsource.clip = Defeat[Random.Range(0, WinWaves.Length)];
        effsource.Play();
    }

    public void Buy()
    {
        effsource.clip = BuyWeapon[Random.Range(0, WinWaves.Length)];
        effsource.Play();
    }

    public void NextWave()
    {
        effsource.clip = HornNextWave;
        effsource.Play();

        StartCoroutine(PlayBattleMusic());

        StartCoroutine(SetBattleLoopVolume(1f));
    }

    public IEnumerator SetBattleLoopVolume(float vol)
    {
        float startvol = battleloop.volume;
        for (int i = 0; i < 10; i++)
        {
            battleloop.volume += (vol - startvol) / 10;
            yield return new WaitForSeconds(.2f);
        }
    }

    IEnumerator PlayBattleMusic()
    {
        AudioClip mus = Battle[Random.Range(0, Battle.Length)];
        if (Battle.Length > 1)
            while (mus == battleloop.clip)
            {
                mus = Battle[Random.Range(0, Battle.Length)];
                yield return new WaitForEndOfFrame();
            }
        battleloop.clip = mus;
        battleloop.Play();
    }
}
