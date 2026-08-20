using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Flower;

public class Hospital : MonoBehaviour
{
    FlowerSystem flowerSystem;
    public GameObject Buttom, fileBorad;

    public int FurryIndex, vampireIndex, seaIndex;
    public GameObject[] costomer;
    public GameObject costomerSlime;
    [HideInInspector] public int costomerID = 99;
    Text DialogName;
    [HideInInspector] public Image DialogImage;
    string costomerName, playerName;
    public Sprite CostomerSprite, PlayerSprite;
    HospitalDialogue hospitalDialogue;
    GameObject CostomerGameObject;
    public DoctorResult Result;

    Sick[] sicks;
    List<Sick> RandomSick = new List<Sick>();
    GameCtrl gameCtrl;
    // public  GameObject dialog;
    void Start()
    {


        flowerSystem = FlowerManager.Instance.CreateFlowerSystem("Hospital", true);
        playerName = "普萊格";

        gameCtrl = gameObject.GetComponent<GameCtrl>();

        flowerSystem.SetupDialog("對話1");
        DialogName = flowerSystem.dialog_text;
        DialogImage = flowerSystem.Dialog_Image;
        //  dialog = GameObject.Find("客人(Clone)");



        flowerSystem.RegisterCommand("Lock", (List<string> _params) => { GameObject.Find("夾板").GetComponent<FileBoard>().costomer.GetComponent<HospitalDialogue>().Lock = true; });
        flowerSystem.RegisterCommand("UnLock", (List<string> _params) => { GameObject.Find("夾板").GetComponent<FileBoard>().costomer.GetComponent<HospitalDialogue>().Lock = false; });
        flowerSystem.RegisterCommand("name", (List<string> _params) => { DialogName.text = costomerName; DialogImage.sprite = CostomerSprite; });
        flowerSystem.RegisterCommand("player", (List<string> _params) =>
        {
            DialogName.text = playerName; DialogImage.sprite = PlayerSprite;
            try
            {
                if (Result.CostomerSick != 3)
                    hospitalDialogue.animator.SetInteger("int", 0);
            }
            catch
            {

            }
        });

        flowerSystem.RegisterCommand("ani", (List<string> _params) =>
        {
            try
            {
                hospitalDialogue.animator.SetInteger("int", int.Parse(_params[0]));
            }
            catch
            {

            }

        });
        sicks = Resources.LoadAll<Sick>("Sick");
    }

    public void Action()
    {
        gameCtrl.AP_Reduce(1);

    }
    public void Costomer()
    {
       // Debug.Log(costomerID);
       
        if (PlayerPrefs.GetInt("Tutorial") == 1 && SceneManager.GetActiveScene().name == "診所")
        {
            gameCtrl.Tutorial(1);
        }


        if (costomerID == 99)
        {
            if (SceneManager.GetActiveScene().name == "港口")
            {
                costomerID = Random.Range(seaIndex, costomer.Length);
            }
            else if (gameCtrl.NowTime >= 22 || gameCtrl.NowTime < 6)
            {
                costomerID = Random.Range(vampireIndex, seaIndex);
            }
            else
            {
                costomerID = Random.Range(0, vampireIndex);
            }
        }

        CostomerGameObject = Instantiate(costomer[costomerID], costomer[costomerID].transform.position, transform.rotation);

        fileBorad.GetComponent<FileBoard>().costomer = CostomerGameObject;
        hospitalDialogue = CostomerGameObject.GetComponent<HospitalDialogue>();
        costomerName = hospitalDialogue.costomerName;
        CostomerSprite = hospitalDialogue.sprite;
        Result.HumanID = hospitalDialogue.HumanID;



        RandomSick.Clear();

        foreach (Sick sicks in sicks)
        {
            if (sicks.Human == 1 || sicks.Human == Result.HumanID)
            {
                RandomSick.Add(sicks);
               // Debug.Log("新增" + sicks.Name);
            }
        }
        RandomSick.TrimExcess();

        int sickIndex = Random.Range(1, RandomSick.Count);
        Result.CostomerSick = RandomSick[sickIndex].ID;
        //Debug.Log(RandomSick[sickIndex].Name);



        //Result.CostomerSick = 5;

        #region  各疾病調整

        if (Result.CostomerSick == 4)//史萊姆
        {
           string name= CostomerGameObject.GetComponent<HospitalDialogue>().costomerName;

            Destroy(CostomerGameObject);
            CostomerGameObject = Instantiate(costomerSlime, costomerSlime.transform.position, transform.rotation);
            fileBorad.GetComponent<FileBoard>().costomer = CostomerGameObject;
            hospitalDialogue = CostomerGameObject.GetComponent<HospitalDialogue>();
            CostomerSprite = hospitalDialogue.sprite;
            hospitalDialogue.costomerName = name;
        }

        hospitalDialogue.sickID = Result.CostomerSick;
        CostomerGameObject.SetActive(true);//要先SetActive(true)才能調整動畫的key值

        if (Result.CostomerSick == 3)//顛笑
        {
            try
            {
                hospitalDialogue.animator.SetInteger("int", 3);
            }
            catch
            {

            }

        }
        else if (Result.CostomerSick == 5)//凋零
        {
            try
            {
                hospitalDialogue.animator.SetBool("flower", true);
            }
            catch
            {
                hospitalDialogue.spriteRenderer.sprite = hospitalDialogue.flower;
            }

        }
        #endregion


        Buttom.SetActive(false);
        // costomer[costomerID].SetActive(true);
        fileBorad.SetActive(true);
        costomerID = 99;
    }

}
