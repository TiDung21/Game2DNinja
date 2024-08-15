using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISettingGame : MonoBehaviour
{
    //[SerializeField] private AudioSource audioSource;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private GameObject buttonsSetting;
    [SerializeField] private Button[] buttons;
    [SerializeField] private TextMeshProUGUI[] textKeysSetting;

    private void Awake()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.5f);
            LoadSetting();
        }
        else
        {
            LoadSetting();
        }
        ButtonsSetting();
    }
    private void Update()
    {
        ButtonsSetting();
    }
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveSetting();
    }

    public void ButtonsSetting()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(ChangeButton);
        }
    }
    private void ChangeButton()
    {
        //ButtonsToArray();
        foreach(KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            for(int i = 0; i < buttons.Length; i++)
            {
                if(Input.GetKeyDown(keyCode))
                {
                    textKeysSetting[i].text = keyCode.ToString();
                }
            }
        }
    }
    private void ButtonsToArray()
    {
        int childCount = buttonsSetting.transform.childCount;
        Debug.Log(childCount);
        buttons = new Button[childCount];
        textKeysSetting = new TextMeshProUGUI[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = buttonsSetting.transform.GetChild(i).gameObject.GetComponent<Button>();
            textKeysSetting[i] = buttons[i].transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        }
    }
    public void LoadSetting()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    public void SaveSetting()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
