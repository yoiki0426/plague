using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbCtrl : MonoBehaviour
{
    public GameObject collectionGame;
    public GameObject[] Menu;
    public GameObject MenuTotal;
    GameObject NowMeuu;
    HerbsList herbsList;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMenu(int index)
    {
        MenuTotal.SetActive(true);
        NowMeuu = Menu[index];
        NowMeuu.SetActive(true);
        herbsList = NowMeuu.GetComponent<HerbsList>();
    }
    public void CloseMenu()
    {
        NowMeuu.SetActive(false);
        MenuTotal.SetActive(false);
    }
    public void GameStar(int StageID)
    {
        NowMeuu.SetActive(false);
        MenuTotal.SetActive(false);
      GameObject gameObject=  Instantiate(collectionGame, GameObject.Find("Canvas").transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        herbsList.collectionGame = gameObject.GetComponent<CollectionGame>();
        


    }
}
