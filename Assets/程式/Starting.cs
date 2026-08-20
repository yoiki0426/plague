using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Starting : MonoBehaviour
{
    public GameObject Setting;
    private void Start()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("BGM");

        if (!PlayerPrefs.HasKey("AP"))
        {
            PlayerPrefs.SetInt("AP", 24);
        }
        if (!PlayerPrefs.HasKey("NowTime"))
        {
            PlayerPrefs.SetInt("NowTime", 6);
        }
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", 1000);
        }
        if (!PlayerPrefs.HasKey("Season"))
        {
            PlayerPrefs.SetInt("Season", 1);
        }
        if (!PlayerPrefs.HasKey("Day"))
        {
            PlayerPrefs.SetInt("Day", 1);
        }
        if (!PlayerPrefs.HasKey("Renown"))
        {
            PlayerPrefs.SetInt("Renown", 0);
        }
        if (!PlayerPrefs.HasKey("Year"))
        {
            PlayerPrefs.SetInt("Year", 1);
        }
        if (!PlayerPrefs.HasKey("BGM"))
        {
            PlayerPrefs.SetFloat("BGM", 100);
        }
        if (!PlayerPrefs.HasKey("SE"))
        {
            PlayerPrefs.SetFloat("SE", 100);
        }
    }
    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }
    public void SettingOpen()
    {
        Instantiate(Setting, GameObject.Find("Canvas").transform).GetComponent<SettingMenu>().gameCtrl = gameObject.GetComponent<GameCtrl>();
    }
}
