using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSilder;
    [SerializeField] Slider sfxSlider;
    float originalValueMaster;
    float originalValueMusic;
    float originalValueSFX;

    public Image masterImage;
    public Image musicImage;
    public Image SFXImage;
    public Sprite audioOn;
    public Sprite audioOff;

    public const string MIXER_MASTER = "Master";
    public const string MIXER_MUSIC = "Music";
    public const string MIXER_SFX = "SFX";

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat(AudioManager.MASTER_KEY, 1f);
        musicSilder.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
    }
    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSilder.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }
    // Update is called once per frame
    void Update()
    {
        if(masterSlider.value > masterSlider.minValue)
        {
            masterImage.overrideSprite = audioOn;
        }
        else if (masterSlider.value == masterSlider.minValue)
        {
            masterImage.overrideSprite = audioOff;
        }
        if (musicSilder.value > masterSlider.minValue)
        {
            musicImage.overrideSprite = audioOn;
        }
        else if (musicSilder.value == musicSilder.minValue)
        {
            musicImage.overrideSprite = audioOff;
        }
        if (sfxSlider.value > masterSlider.minValue)
        {
            SFXImage.overrideSprite = audioOn;
        }
        else if (sfxSlider.value == sfxSlider.minValue)
        {
            masterImage.overrideSprite = audioOff;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseScreen.SetActive(!pauseScreen.activeInHierarchy);
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MASTER_KEY, masterSlider.value);
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSilder.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
    }

    void SetMasterVolume(float value)
    {
        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(value) * 20);
    }

    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }

    void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }

    public void MuteMaster(Slider volumeSlider)
    {
        if (volumeSlider.value == volumeSlider.minValue)
        {
            volumeSlider.value = originalValueMaster;
            mixer.SetFloat(MIXER_MASTER, Mathf.Log10(volumeSlider.value) * 20);
            masterImage.overrideSprite = audioOn;
        }
        else
        {
            originalValueMaster = volumeSlider.value;
            mixer.SetFloat(MIXER_MASTER, Mathf.Log10(volumeSlider.minValue) * 20);
            volumeSlider.value = volumeSlider.minValue;
            masterImage.overrideSprite = audioOff;
        }
    }

    public void MuteMusic(Slider volumeSlider)
    {
        if (volumeSlider.value == volumeSlider.minValue)
        {
            volumeSlider.value = originalValueMusic;
            mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(volumeSlider.value) * 20);
            musicImage.overrideSprite = audioOn;
        }
        else
        {
            originalValueMusic = volumeSlider.value;
            mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(volumeSlider.minValue) * 20);
            volumeSlider.value = volumeSlider.minValue;
            musicImage.overrideSprite = audioOff;
        }
    }
    public void MuteSFX(Slider volumeSlider)
    {
        if (volumeSlider.value == volumeSlider.minValue)
        {
            volumeSlider.value = originalValueSFX;
            mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(volumeSlider.value) * 20);
            SFXImage.overrideSprite = audioOn;
        }
        else
        {
            originalValueSFX = volumeSlider.value;
            mixer.SetFloat(MIXER_SFX, Mathf.Log10(volumeSlider.minValue) * 20);
            volumeSlider.value = volumeSlider.minValue;
            SFXImage.overrideSprite = audioOff;
        }
    }
}
