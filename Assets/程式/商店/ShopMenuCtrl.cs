using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuCtrl : MonoBehaviour
{
    Item[] materials;
    public Transform Shop_transform;
    List<GameObject> ShopSlot = new List<GameObject>();
    GameCtrl gameCtrl;

    [Header("Slot相關")]
    public GameObject Slot;
    public GameObject[] slotSelect;
    public Sprite[] Rare;
    public GameObject[] RareStar;

    int nowSlot = 0;
    int i = 0;
    SlotID slotID, NowSlot;

    [Header("背包相關")]
    public GameObject[] page;
    public Text Name, ItemDetails, count, price;
    public Image image;
    int NowPage = 0;
    int FirstMaterialSlot;

    [Header("確認選單相關")]
    public GameObject confirmMenu;
    public Slider slider;
    public Text Main, BuyCount, BuyMoney;

    void Start()
    {

        materials = Resources.LoadAll<Item>("material");
        foreach (Item item in materials)
        {
            if (item.id == 1 || item.id == 2 || item.id == 4 || item.id == 17 || item.id == 20)
            {
                GameObject slot = Instantiate(Slot, Shop_transform);
                ShopSlot.Add(slot);

                SlotID slotID = slot.GetComponent<SlotID>();
                slotID.ID = i;
                slotID.id = item.id;
                slotID.sprite = item.sprite;
                slotID.Name = item.Name;
                slotID.ItemDetails = item.ItemDetails;
                slotID.count = item.Count;
                slotID.price = item.buy;
                slotID.Rare = item.Rare;
                slotID.transform.GetChild(0).GetComponent<Image>().sprite = Rare[slotID.Rare - 1];
                slotID.transform.GetChild(1).GetComponent<Image>().sprite = slotID.sprite;
                Instantiate(RareStar[slotID.Rare - 1], slot.transform);


                if (i == 0)
                {
                    NowSlot = slotID;
                    Name.text = slotID.Name;
                    ItemDetails.text = slotID.ItemDetails;
                    count.text = "所持數量:" + slotID.count;
                    image.sprite = slotID.sprite;
                    price.text = "售價:" + slotID.price;
                    Instantiate(slotSelect[slotID.Rare - 1], slot.transform);
                }

                i++;
                slot.SetActive(true);
            }
        }

        gameCtrl = GameObject.Find("GameCtrl").GetComponent<GameCtrl>();
        slider.onValueChanged.AddListener(UpdateText);
    }

    public void TouchSlot(int x, int Rare)
    {
        Destroy(ShopSlot[nowSlot].transform.GetChild(3).gameObject);
        Instantiate(slotSelect[Rare - 1], ShopSlot[x].transform);

        slotID = ShopSlot[x].GetComponent<SlotID>();
        NowSlot = slotID;
        Name.text = slotID.Name;
        ItemDetails.text = slotID.ItemDetails;
        count.text = "所持數量:" + slotID.count;
        price.text = "售價:" + slotID.price;
        image.sprite = slotID.sprite;
        nowSlot = x;
    }

    public void confirmMenu_Start()
    {
        if (gameCtrl.Money / NowSlot.price >= 99)
            slider.maxValue = 99;
        else
            slider.maxValue = gameCtrl.Money / NowSlot.price;

        slider.value = 1;


        BuyCount.text = "購買數量:1";
        int sum = gameCtrl.Money - NowSlot.price;
        BuyMoney.text = "總共花費 " + NowSlot.price + " 元，剩餘" + sum;
        Main.text = "確定購買 " + NowSlot.Name + " 嗎?";

        confirmMenu.SetActive(true);
    }
    public void confirmMenu_End()
    {
        confirmMenu.SetActive(false);
    }
    void UpdateText(float value)
    {
        int sum = gameCtrl.Money - (int)value * NowSlot.price;
        BuyCount.text = "購買數量:" + (int)value;
        BuyMoney.text = "總共花費 " + (int)value * NowSlot.price + " 元，剩餘" + sum + "元";
    }

    public void Buy()
    {
        foreach (Item item in materials)
        {
            if (item.id == NowSlot.id)
            {
                Debug.LogWarning(",,,,");
                item.Count += (int)slider.value;
                if (!item.have)
                    item.have = true;
                break;
            }
        }
        gameCtrl.Money -= NowSlot.price * (int)slider.value;
        gameCtrl.Money_text.text = gameCtrl.Money.ToString();
        PlayerPrefs.SetInt("Money", gameCtrl.Money);

        Destroy(gameObject);
    }

    public void ShopStop()
    {
        Destroy(gameObject);
    }
}
