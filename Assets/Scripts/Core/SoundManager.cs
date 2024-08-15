using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private UISettingGame uiSettingGame;
    public static SoundManager instance { get; private set; }
    private AudioSource source;
    private void Awake()
    {
        uiSettingGame.GetComponent<UISettingGame>().LoadSetting();

        source = GetComponent<AudioSource>();
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }
}
