using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;

    private AudioClip[] audioClips = null;

    [SerializeField]
    private AudioSource BGM = null;
    [SerializeField]
    private AudioSource Sound = null;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }

            return instance;
        }
    }

    public enum SoundType
    {
        BGM,
        Button,
        Effect
    }

    public AudioClip[] AudioClips 
    { 
        get => audioClips; 
        set => audioClips = value; 
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        audioClips = Resources.LoadAll<AudioClip>("Sound/");
    }

    public void PlaySound(SoundType _type, string _soundname = "")
    {
        if (_type == SoundType.BGM)
        {
            BGM.Play();
        }
        else if (_type == SoundType.Button)
        {
            Sound.PlayOneShot(AudioClip(_soundname));
        }
        else if (_type == SoundType.Effect)
        {
            Sound.PlayOneShot(AudioClip(_soundname));
        }
    }

    public void StopSound(SoundType _type, string _soundname = "")
    {
        if (_type == SoundType.BGM)
        {
            BGM.Stop();
        }
    }

    AudioClip AudioClip(string _soundname)
    {
        AudioClip clip = null;

        for (int i = 0; i < audioClips.Length; i++)
        {
            if (string.Compare(audioClips[i].name, _soundname, true) == 0)
            {
                clip = audioClips[i];

                break;
            }
        }

        return clip;
    }



}
