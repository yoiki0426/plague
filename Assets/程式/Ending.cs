using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flower;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    FlowerSystem flowerSystem;
    int EndID;
    string StorySence;


    // Start is called before the first frame update
    void Start()
    {
        EndID = 2;
        StorySence = "診所";
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        float Bgm = PlayerPrefs.GetFloat("BGM");
        audioSource.volume = Bgm;

        if (PlayerPrefs.HasKey("End"))
            EndID = PlayerPrefs.GetInt("End");
        if (PlayerPrefs.HasKey("StorySence"))
            StorySence = PlayerPrefs.GetString("StorySence");

        flowerSystem = FlowerManager.Instance.CreateFlowerSystem("Ending", true);
        flowerSystem.SetupDialog("結局對話");

        flowerSystem.RegisterCommand("Scene", (List<string> _params) => 
        {
            if (EndID != 3)
                SceneManager.LoadScene(0); 
            else
                SceneManager.LoadScene(StorySence);
        });
        flowerSystem.RegisterCommand("endding", (List<string> _params) =>
        {
                gameObject.GetComponent<SettingMenu>().ClearKey();
                PlayerPrefs.SetFloat("BGM", Bgm);
        });

        switch (EndID)
        {
            case 1:
                flowerSystem.ReadTextFromResource("結局/普通結局");
                break;
            case 2:
                flowerSystem.ReadTextFromResource("結局/結局一代名醫"); 
                break;
            case 3:
                flowerSystem.ReadTextFromResource("結局/復仇前導");
                break;
            case 31:
                flowerSystem.ReadTextFromResource("結局/毒殺");
                break;
            case 32:
                flowerSystem.ReadTextFromResource("結局/縱火");
                break;
            case 4:
                flowerSystem.ReadTextFromResource("結局/結局過勞死");
                break;
            case 5:
                flowerSystem.ReadTextFromResource("結局/結局破產");
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            flowerSystem.Next();
        }

    }
}
