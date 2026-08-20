using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionGame : MonoBehaviour
{
    [Header("基礎設定")]
    public int MaxTime = 100;
    public int TotalSec = 10;
    public int NowTime = 0;
    public RectTransform TimeBar;
    bool start = false;
    public Text CountDown;
    public GameObject CountDownMenu;

    public int score;
    public Text ScoreText;
    float Random_x, Random_y, Random_time;
    public float Stun_time;
    public GameObject Stun_red;

    public GameObject[] Random_Herbs;
    public Transform Herbs;

    public bool stun = false;
    bool AddHerb_ = true;
    public float HerbContinued_Time;

    public Sprite[] Player;
    public Image PlayerImage;
    [HideInInspector] public int SpriteIndex = 0;

    public AudioClip[] audioClip;
    public AudioSource audioSource;

    [Header("結算設定")]
    public Transform GetItemTransform;
    public GameObject FinishMenu, GetItemText;
    public Text scoreTextFinish;
    bool finish = false;
    int amount;

    [HideInInspector] public Item[] PassbleHerbs1;
    [HideInInspector] public Item[] PassbleHerbs2;
    [HideInInspector] public Item[] PassbleHerbs3;
    [HideInInspector] public int StageID;

    Dictionary<Item, int> GetItems = new Dictionary<Item, int>();
    Item RandomItem;

    
    void Start()
    {
        audioSource = GameObject.Find("GameCtrl").GetComponent<AudioSource>();
        audioSource.Play();
        audioSource.clip = audioClip[1];
        audioSource.Play();
        InvokeRepeating("TimeRedeuce", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (score > 9)
            ScoreText.text = "A";
        else if (score > 4)
            ScoreText.text = "B";

        if (AddHerb_ && start && !finish)
        {
            AddHerb_ = false;
            Invoke("AddHerb", Random_time);

            Random_time = Random.Range(0.3f, 0.6f);
        }
        #region 結算
        if (NowTime == TotalSec && !finish)
        {
            finish = true;
            CancelInvoke();
            audioSource.clip = audioClip[0];
            audioSource.Play();

            Debug.Log(score);

            if (score > 9)
            {
                scoreTextFinish.text = "A";
                amount = Random.Range(18, 26);
            }
            else if (score > 4)
            {
                scoreTextFinish.text = "B";
                amount = Random.Range(13, 21);
            }
            else
                amount = Random.Range(10, 16);
            for (int i = 0; i < amount; i++)
            {
                int random = Random.Range(1, 101);
                int index;

                #region 抽Item
                switch (StageID)
                {
                    case 0:
                        if (random <= 49)
                        {
                            index = Random.Range(0, PassbleHerbs1.Length);
                            RandomItem = PassbleHerbs1[index];
                        }
                        else if (random <= 80)
                        {
                            index = Random.Range(0, PassbleHerbs2.Length);
                            RandomItem = PassbleHerbs2[index];
                        }
                        else
                        {
                            index = Random.Range(0, PassbleHerbs3.Length);
                            RandomItem = PassbleHerbs3[index];
                        }
                        break;
                    case 1:
                        if (random <= 63)
                        {
                            index = Random.Range(0, PassbleHerbs1.Length);
                            RandomItem = PassbleHerbs1[index];
                        }
                        else if (random <= 90)
                        {
                            index = Random.Range(0, PassbleHerbs2.Length);
                            RandomItem = PassbleHerbs2[index];
                        }
                        else
                        {
                            index = Random.Range(0, PassbleHerbs3.Length);
                            RandomItem = PassbleHerbs3[index];
                        }
                        break;
                }
                RandomItem.Count++;
                if (!RandomItem.have)
                    RandomItem.have = true;
                // Debug.Log(RandomItem.Name);


                if (GetItems.ContainsKey(RandomItem))
                {
                    GetItems[RandomItem]++;
                }
                else
                {
                    GetItems[RandomItem] = 1;
                }
            }
            #endregion
            foreach (var getItem in GetItems)
            {
                Text text = Instantiate(GetItemText, GetItemTransform).GetComponent<Text>();
                text.text = getItem.Key.Name + "      X" + getItem.Value;
            }

            FinishMenu.SetActive(true);
        }
        #endregion
    }
    void TimeRedeuce()
    {
        NowTime++;
        if (!start)
        {
            if (NowTime == 1)
                CountDown.text = "2";
            else if (NowTime == 2)
                CountDown.text = "1";
            else
            {
                NowTime = 0;
                CountDownMenu.SetActive(false);
                for (int i = 0; i < 3; i++)
                {
                    AddHerb();
                }
                Random_time = Random.Range(0.3f, 0.6f);
                start = true;
            }
        }
        else
        {
            TimeBar.sizeDelta = new Vector2(MaxTime / TotalSec * (TotalSec - NowTime), TimeBar.sizeDelta.y);
        }

    }
    void AddHerb()
    {
        int random = Random.Range(0, Random_Herbs.Length);
        Random_x = Random.Range(-334.4f, 267.1f);
        Random_y = Random.Range(-156.9f, 128.4f);




        GameObject newHerb = Instantiate(Random_Herbs[random], Vector3.zero, Quaternion.identity, Herbs);
        newHerb.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random_x, Random_y);
        newHerb.GetComponent<Herbs>().collectionGame = gameObject.GetComponent<CollectionGame>();

        //Debug.Log("World Position: " + newHerb.transform.position);
        //Debug.Log("Local Position: " + newHerb.transform.localPosition);

        AddHerb_ = true;
    }

    public void EndThis()
    {
        Destroy(gameObject);
    }
}
