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
    }

    public void WinWave()
    {
        effsource.clip = WinWaves[Random.Range(0, WinWaves.Length)];
        effsource.Play();

        StartCoroutine(SetVolume(0));
    }

    public void DefeatWave()
    {
        effsource.clip = Defeat[Random.Range(0, WinWaves.Length)];
        effsource.Play();

        StartCoroutine(SetVolume(0));
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

        AudioClip mus;
        do { mus = Battle[Random.Range(0, WinWaves.Length)]; } while (mus != battleloop.clip);

        battleloop.clip = Battle[Random.Range(0, WinWaves.Length)];
        battleloop.Play();

        StartCoroutine(SetVolume(1));
    }

    public IEnumerator SetVolume(float volume)
    {
        float startvol = battleloop.volume;
        if (startvol != volume)
        {
            for (int i = 0; i < 10; i++)
            {
                if (startvol > volume) battleloop.volume -= (startvol - volume) / 10;
                else battleloop.volume += (volume - startvol) / 10;
                yield return new WaitForSeconds(.2f);
            }
        }
    }
}
