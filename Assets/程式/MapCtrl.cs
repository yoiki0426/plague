using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapCtrl : MonoBehaviour
{
    GameCtrl gameCtrl;
    
    void Start()
    {
        gameCtrl= GameObject.Find("GameCtrl").GetComponent<GameCtrl>();
    }

    public void Go_Scene(string _SceneName)
    {
        try 
        {
            gameCtrl.KeySet();
            SceneManager.LoadScene(_SceneName);
        }
        catch { }
       
    }
    public void OpenMenu(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    public void CloseMenu(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
    public void End()
    {
        Destroy(gameObject);
    }
}
