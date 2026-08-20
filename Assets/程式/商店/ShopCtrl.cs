using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCtrl : MonoBehaviour
{
    public GameObject ShopMenu;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShopStart()
    {
        Instantiate(ShopMenu, GameObject.Find("Canvas").transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
    }
}
