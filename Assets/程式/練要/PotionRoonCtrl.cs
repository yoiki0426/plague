using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionRoonCtrl : MonoBehaviour
{
    public GameObject MakePotionMenu;
    GameObject potion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PotionStart()
    {
        potion= Instantiate(MakePotionMenu, MakePotionMenu.transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
    }
  
}
