using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotID : MonoBehaviour, IPointerClickHandler
{
    public int ID;
    public int id;
    public string Name;
    public int Rare;

    [TextArea(1, 10)]
    public string ItemDetails;

    public Sprite sprite;
    public int count;
    public int price;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameObject.Find("背包(Clone)"))
            GameObject.Find("背包(Clone)").GetComponent<Backpack>().TouchSlot(ID, Rare);
        else if (GameObject.Find("商店(Clone)"))
            GameObject.Find("商店(Clone)").GetComponent<ShopMenuCtrl>().TouchSlot(ID, Rare);

    }

}