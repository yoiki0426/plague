using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Flower;

public class SceneStartDialogue : MonoBehaviour
{
    FlowerSystem flowerSystem;
    public Sprite [] sprite;
    public string []  Name;
    Text DialogName;
    Image DialogImage;

    bool Lock = false;
    string SceneName;

    public GameObject Button;
    void Start()
    {
        flowerSystem = FlowerManager.Instance.CreateFlowerSystem("SceneStartDialogue", true);
        flowerSystem.SetupDialog("對話1");

        DialogName = flowerSystem.dialog_text;
        DialogImage = flowerSystem.Dialog_Image;

        flowerSystem.RegisterCommand("name", (List<string> _params) =>{ DialogName.text = Name[int.Parse(_params[0])] ; DialogImage.sprite = sprite[int.Parse(_params[0])]; });

        flowerSystem.RegisterCommand("Button", (List<string> _params) => { Button.SetActive(true); });
        

        SceneName = SceneManager.GetActiveScene().name;

      

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            flowerSystem.Next();
        }
    }

    public void StartDialogue()
    {
        Button.SetActive(false);
        switch (SceneName)
        {
            case "山洞":
                flowerSystem.ReadTextFromResource("場景對話/山洞");
                break;
            case "港口":
                flowerSystem.ReadTextFromResource("場景對話/港口");
                break;
            case "獵人小屋":
                flowerSystem.ReadTextFromResource("場景對話/獵人小屋");
                break;
            case "酒館":
                flowerSystem.ReadTextFromResource("場景對話/酒館");
                break;
            case "貨艙":
                flowerSystem.ReadTextFromResource("場景對話/貨艙");
                break;
            case "後院":
                flowerSystem.ReadTextFromResource("場景對話/後院");
                break;
            case "墓地":
                flowerSystem.ReadTextFromResource("場景對話/墓地");
                break;
            case "教堂":
                flowerSystem.ReadTextFromResource("場景對話/教堂");
                break;

        }
    }
}
