using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakePotion : MonoBehaviour
{
    Item[] Potions;
    Item[] material;
    int NowIndex = 0;
    int MaxPage;
    public Text Page;
    public GameObject Recipe_Page, Potion_Page;

    public Image[] ImageRecipe;
    public Image[] BackImageRecipe;
    public Text[] CountRecipe;

    [Header("配方頁")]
    public Text[] Name;
    public Text[] pro;
    public Text[] count;
    public Image[] images;
    public Sprite Imege_null;
    public int[] ID;

    [Header("步驟頁")]
    public Text Potion_step;
    public Text Name2;
    public Text pro2;
    public Text count2;
    public Image images2;
    public Image Deviece;
    [SerializeField] Item NowItem;

    [Header("結算")]
    public GameObject ProGame_;
    public GameObject Game_notStart;

    void OnEnable()
    {
        ShowRecipe(true);
        Potion_Page.SetActive(false);
        Recipe_Page.SetActive(true);

        Potions = Resources.LoadAll<Item>("Potion");
        material = Resources.LoadAll<Item>("material");

        MaxPage = Potions.Length / 3;

        if (Potions.Length % 3 != 0)
            MaxPage++;

        Page.text = "page " + (NowIndex + 1) + " / " + MaxPage;
        ShowData();
    }
    #region 上一頁下一頁
    public void PreviousPage()
    {
        if (NowIndex != 0)
        {
            NowIndex--;
            Page.text = "page " + (NowIndex + 1) + " / " + MaxPage;
            ShowData();
        }
    }
    public void NextPage()
    {
        if (NowIndex < MaxPage - 1)
        {
            NowIndex++;
            Page.text = "page " + (NowIndex + 1) + " / " + MaxPage;
            ShowData();
        }
    }
    #endregion
    void ShowData()
    {
        for (int i = 0; i < 3; i++)
        {
            bool found = false;

            foreach (Item item in Potions)
            {
                if (item.id == (NowIndex * 3) + 1 + i)
                {
                    found = true;

                    Name[i].text = item.Name;
                    pro[i].text = "熟練度 : " + item.Pro;
                    count[i].text = "持有數量 : " + item.Count;
                    ID[i] = item.id;
                    images[i].sprite = item.sprite;
                    break;
                }
            }
            if (!found)
            {
                Name[i].text = "無";
                pro[i].text = "";
                ID[i] = 999;
                images[i].sprite = Imege_null;
            }
        }
    }
    void ShowRecipe(bool Null)
    {
        if (!Null)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i < NowItem.Recipe_items.Length)
                {
                    ImageRecipe[i].sprite = NowItem.Recipe_items[i].sprite;
                    CountRecipe[i].text = NowItem.Recipe_items[i].Count + " / " + NowItem.Recipe_count[i];
                    BackImageRecipe[i].gameObject.SetActive(true);

                }
                else
                {
                    ImageRecipe[i].sprite = Imege_null;
                    CountRecipe[i].text = "";
                    BackImageRecipe[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                ImageRecipe[i].sprite = Imege_null;
                CountRecipe[i].text = "";
                BackImageRecipe[i].color = new Color(140, 140, 140, 255);
            }
        }

    }
    #region 切換鍊藥/步驟/遊戲
    public void ToPotionPage(int i)
    {
        foreach (Item item in Potions)
        {
            if (item.id == ID[i])
            {
                Name2.text = item.Name;
                pro2.text = "熟練度 : " + item.Pro;
                count2.text = "持有數量 : " + item.Count;
                images2.sprite = item.sprite;
                Potion_step.text = "";
                for (int j = 0; j < item.PotionDetails.Length; j++)
                {
                    Potion_step.text = Potion_step.text + item.PotionDetails[j] + "\n\n";
                }

                Recipe_Page.SetActive(false);
                Potion_Page.SetActive(true);
                NowItem = item;
                Deviece.sprite = item.Device[0].sprite;
                ShowRecipe(false);
                break;
            }
        }
    }
    public void ToRecipePage()
    {
        Recipe_Page.SetActive(true);
        Potion_Page.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            BackImageRecipe[i].gameObject.SetActive(false);
        }
        ShowRecipe(true);
    }

    public void StartProGame()
    {
        bool GameStart = true;

        for (int i = 0; i < NowItem.Recipe_items.Length; i++)
        {
            if (NowItem.Recipe_count[i] > NowItem.Recipe_items[i].Count)
                GameStart = false;
        }
        if (GameStart)
        {
            ProGame_.SetActive(true);
            ProGame_.GetComponent<ProGame>().item = NowItem;
            Potion_Page.SetActive(false);
            for (int i = 0; i < NowItem.Recipe_items.Length; i++)
            {
                NowItem.Recipe_items[i].Count -= NowItem.Recipe_count[i];
                if (NowItem.Recipe_items[i].Count == 0)
                    NowItem.Recipe_items[i].have = false;
            }
        }
        else
        {
            Game_notStart.SetActive(true);
        }

    }
    #endregion
    public void End()
    {
        ProGame_.SetActive(false);
        gameObject.SetActive(false);
    }
    public void CloseGame_notStart()
    {
        Game_notStart.SetActive(false);
    }
    public void PotionEnd()
    {
        Destroy(gameObject);
    }
}
