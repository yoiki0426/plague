using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour
{
    public Text BGM_Text, SE_Text;
    public float BGM_, SE_;
    public Slider BGM_slider, SE_slider;
    public GameCtrl gameCtrl;
    public GameObject[] Menu;
    Item[] Potions;
    Item[] material;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("BGM"))
        {
            PlayerPrefs.SetFloat("BGM", 100);
        }
        if (!PlayerPrefs.HasKey("SE"))
        {
            PlayerPrefs.SetFloat("SE", 100);
        }
        Potions = Resources.LoadAll<Item>("Potion");
        material = Resources.LoadAll<Item>("material");

        BGM_ = PlayerPrefs.GetFloat("BGM");
        SE_ = PlayerPrefs.GetFloat("SE");

        try
        {
            BGM_slider.value = BGM_;
            SE_slider.value = SE_;
        }
        catch { }

    }
    public void Close()
    {
        Destroy(gameObject);
        PlayerPrefs.SetFloat("BGM", BGM_);
        PlayerPrefs.SetFloat("SE", SE_);

    }

    public void BGM(float value)
    {
        BGM_ = value;
        BGM_Text.text = (Mathf.Floor(BGM_ * 100)).ToString();
       
        try
        {
            gameCtrl.audioSource.volume = BGM_;
        }
        catch
        {
            GameObject.Find("GameCtrl").GetComponent<AudioSource>().volume = BGM_;
        }

    }
    public void SE(float value)
    {
        SE_ = value;
        SE_Text.text = ((int)value * 100).ToString();
    }

    public void MenuSwitch(int index)
    {
        for (int i = 0; i < Menu.Length; i++)
        {
            Menu[i].SetActive(false);
        }
        Menu[index].SetActive(true);
    }

    #region 開發人員選項

    public void SeasonAdd()
    {
        if (SceneManager.GetActiveScene().name != "標題畫面")
        {
            gameCtrl.Season++;
            if (gameCtrl.Season > 4)
                gameCtrl.Year++;
            
            gameCtrl.SeasonChange();
          
        }

    }
    public void EndSwitch(int x)
    {
        PlayerPrefs.SetInt("End", x);
        PlayerPrefs.SetString("StorySence", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("結局");
    }
    public void RenownAdd()
    {
        if (SceneManager.GetActiveScene().name != "標題畫面")
        {
            gameCtrl.Renown += 1300;
        }

    }
    public void ClearKey()
    {

        for (int i = 0; i < material.Length; i++)
        {
            material[i].Count = 5;
            Debug.Log("ID:" + material[i].id + "|名字:" + material[i].Name + "|數量 :" + material[i].Count);
            material[i].have = true;
        }
        foreach (Item potions in Potions)
        {
            potions.Count = 1;
            potions.have = true;
        }

        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("SE", SE_);
        PlayerPrefs.SetFloat("BGM", BGM_);
        if (SceneManager.GetActiveScene().name != "結局")
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Timectrl(int TargetTime)
    {
        if (SceneManager.GetActiveScene().name != "標題畫面")
        {
            int TimeCheck = 0;
            if (TargetTime + 6 > 24)
                TimeCheck = 5;
            else
                TimeCheck = TargetTime + 6;

            if (gameCtrl.NowTime > TargetTime && gameCtrl.NowTime > TimeCheck)
                gameCtrl.Day++;

            gameCtrl.NowTime = TargetTime;
            gameCtrl.HospitalTime_Change();

            gameCtrl.SeasonChange();
            Vector3 newRotation = new Vector3(0, 0, -30 * gameCtrl.NowTime);

            gameCtrl.ShortHand.GetComponent<RectTransform>().localEulerAngles = newRotation;
            Destroy(gameObject);
        }


    }

    #endregion
}
