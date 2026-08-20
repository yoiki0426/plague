using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCtrl : MonoBehaviour
{
    public int NowTime, Ap, Time_index, Tube_index, Money, Day, Season, Year;
    int SeasonDayMax = 2;

    public SpriteRenderer BackGround;
    Sprite[] BackGround_Sprite;
    Image BackGround_Time;
    Sprite[] BackGround_TimeSprite;

    public Image ShortHand;
    public Image[] TestTube;

    public Text AP_text;
    public Text Money_text;
    public Text Day_text;

    public GameObject Map;
    public int Renown;
    public AudioSource audioSource;

    [Header("選單")]
    public GameObject MemuClose;
    public GameObject MemuOpen;
    public GameObject Backpack;
    public GameObject Setting;
    public GameObject Certificate;

    [Header("選單")]
    public GameObject[] tutorial;

    void Start()
    {
        #region Key重製
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
        if (!PlayerPrefs.HasKey("Year"))
        {
            PlayerPrefs.SetInt("Year", 1);
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
        if (!PlayerPrefs.HasKey("BGM"))
        {
            PlayerPrefs.SetFloat("BGM", 100);
        }
        if (!PlayerPrefs.HasKey("SE"))
        {
            PlayerPrefs.SetFloat("SE", 100);
        }
        if (!PlayerPrefs.HasKey("Tutorial"))
        {
            PlayerPrefs.SetInt("Tutorial", 0);
        }
        #endregion
       
        BackGround_Sprite = BackGround.GetComponent<BackGroundCtrl>().BackGround_Sprite;

        BackGround_Time = GameObject.Find("早中晚顯示").GetComponent<Image>();
        BackGround_TimeSprite = BackGround.GetComponent<BackGroundCtrl>().BackGround_TimeSprite;
        NowTime = PlayerPrefs.GetInt("NowTime");
        Ap = PlayerPrefs.GetInt("AP");
        Money = PlayerPrefs.GetInt("Money");
        Day = PlayerPrefs.GetInt("Day");
        Season = PlayerPrefs.GetInt("Season");
        Renown = PlayerPrefs.GetInt("Renown");
        Year = PlayerPrefs.GetInt("Year");
        HospitalTime_Change();
        AP_tube();
        AP_text.text = Ap + " AP";
        Money_text.text = Money.ToString();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume= PlayerPrefs.GetFloat("BGM");

        Vector3 newRotation = new Vector3(0, 0, -30 * NowTime);
        ShortHand.GetComponent<RectTransform>().localEulerAngles = newRotation;

        SeasonChange();

       // Debug.Log("PlayerPrefs=" + PlayerPrefs.GetInt("Tutorial"));
        if(PlayerPrefs.GetInt("Tutorial")==0 &&  SceneManager.GetActiveScene().name == "診所")
        {
            Tutorial(0);
        }

    }
    private void Update()
    {
        if (Year > 1)
        {
            if (Renown >= 1300)
                PlayerPrefs.SetInt("End", 2);
            else
                PlayerPrefs.SetInt("End", 1);

            SceneManager.LoadScene("結局");
        }
    }

    #region 時間背景影響
  public  void HospitalTime_Change()
    {
        if (NowTime >= 6 && NowTime < 14 && BackGround.sprite != BackGround_Sprite[0] && Time_index != 1)
        {
            Time_index = 1;
            BackGround.sprite = BackGround_Sprite[0];
            BackGround_Time.sprite = BackGround_TimeSprite[0];
        }
        else if (NowTime >= 14 && NowTime < 22 && BackGround.sprite != BackGround_Sprite[1] && Time_index != 2)
        {
            Time_index = 2;
            BackGround.sprite = BackGround_Sprite[1];
            BackGround_Time.sprite = BackGround_TimeSprite[1];
        }
        else if (NowTime >= 22 || NowTime < 6 && BackGround.sprite != BackGround_Sprite[2] && Time_index != 3)
        {
            Time_index = 3;
            BackGround.sprite = BackGround_Sprite[2];
            BackGround_Time.sprite = BackGround_TimeSprite[2];
        }
    }
    #endregion

    #region Ap增減與AP條更動
    public void AP_Reduce(int x)
    {
        if (Ap > x)
        {
            Ap -= x;
            if (NowTime + x >= 24)
            {
                int i = NowTime + x - 24;
                NowTime = 0 + i;

                Day++;
                if (Day > SeasonDayMax)
                {
                    Day = 1;
                    Season++;

                    if (Season > 4)
                    {
                        Season = 1;
                        Year++;
                    }

                }
                SeasonChange();
            }
            else
            {
                NowTime += x;
            }
            Vector3 newRotation = new Vector3(0, 0, -30 * NowTime);

            ShortHand.GetComponent<RectTransform>().localEulerAngles = newRotation;
            AP_text.text = Ap + " AP";
            HospitalTime_Change();
            AP_tube();
        }
    }
    public void AP_Add(int x, int y)
    {
        if (Ap + x > 24)
        {
            Ap = 24;
        }
        else
            Ap += x;

        if (NowTime + y >= 24)
        {
            NowTime = NowTime + y - 24;

            Day++;
            if (Day > SeasonDayMax)
            {
                Day = 1;
                Season++;

                if (Season > 4)
                {
                    Season = 1;
                    Year++;
                }

            }
            SeasonChange();
        }
        else
        {
            NowTime += y;
        }
        Vector3 newRotation = new Vector3(0, 0, -30 * NowTime);

        ShortHand.GetComponent<RectTransform>().localEulerAngles = newRotation;
        AP_text.text = Ap + " AP";
        HospitalTime_Change();
        AP_tube();
    }
    void AP_tube()
    {
        float NowAP;

        if (Ap == 1)
        {
            TestTube[0].gameObject.SetActive(false);
            TestTube[1].gameObject.SetActive(false);
            TestTube[2].color = new Color32(217, 30, 32, 255);
            Tube_index = 4;
        }
        else if (Ap == 2)
        {
            TestTube[1].gameObject.SetActive(true);
            for (int i = 1; i < 3; i++)
                TestTube[i].color = new Color32(217, 30, 32, 255);

            TestTube[0].gameObject.SetActive(false);
            Tube_index = 4;
        }
        else if (Ap < 9 && Tube_index != 1)
        {
            TestTube[0].gameObject.SetActive(true);
            TestTube[1].gameObject.SetActive(true);
            for (int i = 0; i < 3; i++)
                TestTube[i].color = new Color32(217, 30, 32, 255);

            Tube_index = 1;
        }
        else if (Ap > 8 && Ap < 17 && Tube_index != 2)
        {
            TestTube[0].gameObject.SetActive(true);
            TestTube[1].gameObject.SetActive(true);
            for (int i = 0; i < 3; i++)
                TestTube[i].color = new Color32(239, 194, 71, 255);

            Tube_index = 2;
        }
        else if (Ap > 16 && Tube_index != 3)
        {
            TestTube[0].gameObject.SetActive(true);
            TestTube[1].gameObject.SetActive(true);
            for (int i = 0; i < 3; i++)
                TestTube[i].color = new Color32(43, 154, 153, 255);

            Tube_index = 3;
        }

        if (TestTube[0].gameObject.activeSelf)
        {
            NowAP = 0.045454545f * (Ap - 2);
            TestTube[0].GetComponent<RectTransform>().localScale = new Vector3(NowAP, 1, 1);
        }
    }
    #endregion

    #region 選單相關


    public void OpenMenu()
    {
        MemuClose.SetActive(false);
        MemuOpen.SetActive(true);
    }
    public void CloseMenu()
    {
        Animator animator = MemuOpen.GetComponent<Animator>();
        animator.SetBool("OUT", true);
        Invoke("Menu_C", 0.35f);
    }
    void Menu_C()
    {
        MemuOpen.SetActive(false);
        MemuClose.SetActive(true);
    }

    #endregion

    #region 資格證
    public void CertificateOpen()
    {
        Instantiate(Certificate, GameObject.Find("Canvas").transform).GetComponent<Certificate>().gameCtrl = gameObject.GetComponent<GameCtrl>();
        CloseMenu();
    }

    #endregion

    #region 背包
    public void BackpackOpen()
    {
        Instantiate(Backpack, GameObject.Find("Canvas").transform);
    }
    #endregion

    #region 設定
    public void SettingOpen()
    {
        Instantiate(Setting, GameObject.Find("Canvas").transform).GetComponent<SettingMenu>().gameCtrl = gameObject.GetComponent<GameCtrl>();
        CloseMenu();
    }
    #endregion

    #region 開啟地圖
    public void OpenMap()
    {
        Instantiate(Map, GameObject.Find("Canvas").transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
    }
    #endregion

    #region 日期相關

    public void SeasonChange()
    {
        switch (Season)
        {
            case 1:
                Day_text.text = " 第一年\n秋季 " + Day + "日";
                break;
            case 2:
                Day_text.text = " 第一年\n冬季 " + Day + "日";
                break;
            case 3:
                Day_text.text = " 第一年\n春季 " + Day + "日";
                break;
            case 4:
                Day_text.text = " 第一年\n夏季 " + Day + "日";
                break;
        }
    }

    #endregion

    #region Key相關
    public void KeySet()
    {
        PlayerPrefs.SetInt("AP", Ap);
        PlayerPrefs.SetInt("NowTime", NowTime);
        PlayerPrefs.SetInt("Money", Money);
        PlayerPrefs.SetInt("Season", Season);
        PlayerPrefs.SetInt("Day", Day);
        PlayerPrefs.SetInt("Renown", Renown);
        PlayerPrefs.SetInt("Year", Year);

    }
    #endregion
    public void Tutorial(int index)
    {
        Instantiate(tutorial[index], GameObject.Find("Canvas").transform);

        Debug.Log("新手教學");
        if (PlayerPrefs.GetInt("Tutorial") == index)
        {
            int x = PlayerPrefs.GetInt("Tutorial") + 1;
            PlayerPrefs.SetInt("Tutorial", x);
        }
    }

}

