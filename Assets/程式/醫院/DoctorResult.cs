using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class DoctorResult : MonoBehaviour
{
    [Header("基本設定")]
    public GameObject fileBoard;
    [HideInInspector] public List<int> result = new List<int>();
    public GameObject[] page;
    int page_index;

    public GameObject[] Buttom;
    Animator animator;

    public Text NowPage;
    public Text finish;
    public Text finish_Button;
    public Text finishPotion;

    Sick[] sicks;
    public Sick NoSick;

    public int HumanID; //總族

    public Sprite NoneImage;


    [Header("結果處理")]
    public List<Sick> sicks_1;
    public GameObject SickMenu;
    public GameObject SickMenu_;
    public GameObject PossibleSick_text;
    public GameObject PotionMenu;
    public GameObject PotionMenu_;
    [HideInInspector] public Sick ChoseSick;
    List<Sick> PossibleSick = new List<Sick>();
    public GameObject Warning;
    public int CostomerSick;
    public RectTransform Scroll;
    int PotiomRenown, PotionMoney;
    public Text DoctorMoney;
    GameCtrl gameCtrl;
    private void Start()
    {
        page_index = 0;
        NowPage.text = "Page " + (page_index + 1) + "/" + page.Length;
        animator = GetComponent<Animator>();
        sicks = Resources.LoadAll<Sick>("Sick");

        if (SceneManager.GetActiveScene().name != "港口")
            DoctorMoney.text = "診療費  500$";
        else
            DoctorMoney.text = "義診不收取費用";
        gameCtrl = gameObject.GetComponent<GameCtrl>();

    }

    #region  暫時關閉診斷書(END)
    public void Back_FileBoard()
    {
        fileBoard.SetActive(true);
        animator.SetBool("out", true);
        Invoke("End", 0.3f);
    }
    void End()
    {
        gameObject.SetActive(false);
    }
    #endregion

    #region 頁面跳轉控制

    #region 上一頁下一頁
    public void NextPage()
    {
        page_index++;
        page[page_index].SetActive(true);
        page[page_index - 1].SetActive(false);

        if (page_index == page.Length - 1)
        {
            Buttom[1].SetActive(false);
            Buttom[2].SetActive(true);
            Finish();
        }

        if (page_index == 1)
            Buttom[0].SetActive(true);

        NowPage.text = "Page " + (page_index + 1) + "/" + page.Length;
    }
    public void PreviousPage()
    {
        page_index--;
        page[page_index].SetActive(true);
        page[page_index + 1].SetActive(false);

        if (page_index == page.Length - 2)
        {
            Buttom[1].SetActive(true);
            Buttom[2].SetActive(false);
        }

        if (page_index == 0)
            Buttom[0].SetActive(false);

        NowPage.text = "Page " + (page_index + 1) + "/" + page.Length;
        SickMenu_OFF();
    }
    #endregion

    #region 跳轉各大介面
    public void ToPage(int p)
    {
        if (page_index == 0 && p != 0)
            Buttom[0].SetActive(true);

        if (page_index == page.Length - 1)
        {
            Buttom[0].SetActive(true);
            Buttom[1].SetActive(true);
            Buttom[2].SetActive(false);
        }

        page[page_index].SetActive(false);
        page_index = p;
        page[page_index].SetActive(true);
        NowPage.text = "Page " + (page_index + 1) + "/" + page.Length;

        if (page_index == 0)
            Buttom[0].SetActive(false);
    }
    #endregion

    #region 跳轉診段介面
    public void GoTo_Result()
    {
        page[page_index].SetActive(false);
        if (page_index == page.Length - 1)
        {
            page_index = 0;
            Buttom[2].SetActive(false);
            Buttom[0].SetActive(false);
            Buttom[1].SetActive(true);
            finish_Button.text = ("診斷完畢");
        }
        else
        {
            page_index = page.Length - 1;
            Buttom[1].SetActive(false);
            Buttom[2].SetActive(true);
            Buttom[0].SetActive(true);
            Finish();
            finish_Button.text = ("回到病徵");
        }
        page[page_index].SetActive(true);
        NowPage.text = "Page " + (page_index + 1) + "/" + page.Length;
        SickMenu_OFF();
        Debug.Log("疾病ID" + CostomerSick);
    }
    #endregion

    #endregion

    #region 診斷結果
    void Finish()
    {
        if (sicks_1.Count == 0)
        {
            Debug.Log(HumanID);
            foreach (Sick sicks in sicks)
            {
                if (sicks.Human == 1 || sicks.Human == HumanID)
                {
                    sicks_1.Add(sicks);
                }
            }
        }

        PossibleSick.Clear();
        PossibleSick.TrimExcess();
        PossibleSick = sicks_1.Where(sick => result.All(r => sick.Diagnosis.Contains(r))).OrderBy(sick => sick.ID).ToList();
        // sicks_1.Where(sick => result.All(r => sick.Diagnosis.Contains(r)))  塞選勾選病症的內容


    }
    #endregion

    #region 完成診察與初始化
    public void Reset_Result()
    {
        if (ChoseSick != null)
        {
            #region 結算金錢與聲望

            GameCtrl gameCtrl = GameObject.Find("GameCtrl").GetComponent<GameCtrl>();

            if (SceneManager.GetActiveScene().name != "港口")
                gameCtrl.Money += 500 + PotionMoney;

            gameCtrl.Money_text.text = gameCtrl.Money.ToString();
            PlayerPrefs.SetInt("Money", gameCtrl.Money);

            if (ChoseSick.ID == CostomerSick && SceneManager.GetActiveScene().name == "港口")
            {
                gameCtrl.Renown += PotiomRenown + 50;
            }
            else if (ChoseSick.ID == CostomerSick)
            {
                gameCtrl.Renown += PotiomRenown + 50;
            }
            else
                gameCtrl.Renown -= 70;

            for (int i = 0; i < ChoseSick.potion.Length; i++)
            {
                if (ChoseSick.potion[i].Count > 0)
                {
                    ChoseSick.potion[i].Count--;
                    if (ChoseSick.potion[i].Count == 0)
                    {
                        ChoseSick.potion[i].have = false;
                    }
                }
            }



            #endregion

            #region 初始化
            page[page_index].SetActive(false);
            page_index = 0;
            page[page_index].SetActive(true);
            NowPage.text = "Page " + (page_index + 1) + "/" + page.Length;
            Buttom[2].SetActive(false);
            Buttom[0].SetActive(false);
            Buttom[1].SetActive(true);
            finish_Button.text = ("診斷完畢");
            finish.text = "";
            finishPotion.text = "";
            sicks_1.Clear();
            ChoseSick = null;
            Scroll.anchoredPosition = new Vector2(Scroll.anchoredPosition.x, -0.03096771f);
            DoctorMoney.text = "診療費  500$";
            PotionMoney = 0;
            PotiomRenown = 0;
            //

            GameObject.Find("GameCtrl").GetComponent<Hospital>().Buttom.SetActive(true);
            Destroy(fileBoard.GetComponent<FileBoard>().costomer);

            result.Clear();

            List<Diagnosis> diagnosisList = new List<Diagnosis>();

            foreach (GameObject obj in GetAllObjectsInScene())
            {
                // 查找对象上是否有Diagnosis组件
                Diagnosis diagnosis = obj.GetComponent<Diagnosis>();

                // 如果对象上有Diagnosis组件，将其添加到列表中
                if (diagnosis != null)
                {
                    diagnosisList.Add(diagnosis);
                }
            }
            for (int i = 0; i < diagnosisList.Count; i++)
            {
                diagnosisList[i].NowCheck = false;
            }
            GameObject.Find("GameCtrl").GetComponent<Hospital>().DialogImage.sprite = NoneImage;
            #endregion

            animator.SetBool("out", true);

            Invoke("End", 0.3f);
        }
        else
        {
            Warning.transform.GetChild(1).GetComponent<Text>().text = "請先選擇病症";
            Warning.SetActive(true);
        }

    }
    public void Warning_Off()
    {
        Warning.SetActive(false);
    }


    // 获取场景中所有的对象，包括未激活的对象
    private List<GameObject> GetAllObjectsInScene()
    {
        List<GameObject> allObjects = new List<GameObject>();
        GameObject[] rootObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject rootObject in rootObjects)
        {
            GetAllChildObjects(rootObject, allObjects);
        }

        return allObjects;
    }

    // 递归获取对象的所有子对象
    private void GetAllChildObjects(GameObject obj, List<GameObject> allObjects)
    {
        allObjects.Add(obj);
        foreach (Transform child in obj.transform)
        {
            GetAllChildObjects(child.gameObject, allObjects);
        }
    }

    #endregion

    #region 病症選單開關

    public void SickMenu_ON()
    {
        if (PossibleSick.Count > 0)
        {
            if (PossibleSick[0].ID != 0)
            {
                GameObject PossibleSick_Text = Instantiate(PossibleSick_text, transform.position, Quaternion.identity, SickMenu_.transform);

                PossibleSick_Text.GetComponent<PossibleSicks>().sick = sicks_1[0];
                PossibleSick_Text.GetComponent<Text>().text = sicks_1[0].Name;
            }

            for (int i = 0; i < PossibleSick.Count; i++)
            {
                GameObject PossibleSick_Text = Instantiate(PossibleSick_text, transform.position, Quaternion.identity, SickMenu_.transform);

                PossibleSick_Text.GetComponent<PossibleSicks>().sick = PossibleSick[i];
                PossibleSick_Text.GetComponent<Text>().text = PossibleSick[i].Name;
            }
        }
        else
        {
            GameObject PossibleSick_Text = Instantiate(PossibleSick_text, transform.position, Quaternion.identity, SickMenu_.transform);

            PossibleSick_Text.GetComponent<PossibleSicks>().sick = sicks_1[0];
            PossibleSick_Text.GetComponent<Text>().text = sicks_1[0].Name;
        }
        SickMenu.SetActive(true);
    }
    public void SickMenu_OFF()
    {
        if (SickMenu_.transform.childCount != 0)
        {
            for (int i = 0; i < SickMenu_.transform.childCount; i++)
            {
                Destroy(SickMenu_.transform.GetChild(i).gameObject);
            }
        }

        SickMenu.SetActive(false);
    }

    #endregion

    #region 藥水自動
    public void PotionResult()
    {
        DoctorMoney.text = "診療費  500$";
        PotionMoney = 0;
        PotiomRenown = 0;
        finishPotion.text = "";

        for (int i = 0; i < ChoseSick.potion.Length; i++)
        {
            if (i > 0)
                finishPotion.text = finishPotion.text + " 、 ";

            if (ChoseSick.potion[i].Count == 0)
                finishPotion.text = finishPotion.text + "<color=#ff0000>";
            else
            {
                DoctorMoney.text = DoctorMoney.text + "\n" + ChoseSick.potion[i].Name + "  " + ChoseSick.potion[i].buy + "$";
                PotionMoney += ChoseSick.potion[i].buy;
                PotiomRenown += 50;
            }

            finishPotion.text = finishPotion.text + ChoseSick.potion[i].Name;

            if (ChoseSick.potion[i].Count == 0)
                finishPotion.text = finishPotion.text + "</color>";
        }

        if (SceneManager.GetActiveScene().name == "港口")
        {
            DoctorMoney.text = "義診不收取費用";
        }

    }
    #endregion
}
