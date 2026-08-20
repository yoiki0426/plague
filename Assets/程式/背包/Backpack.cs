using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : MonoBehaviour
{
    public Transform Potion, material, device;
    List<GameObject> deviceSlot = new List<GameObject>();
    List<GameObject> PotionSlot = new List<GameObject>();
    List<GameObject> materialSlot = new List<GameObject>();

    Item[] materials;
    Item[] Potions;
    Item[] devices;

    int nowSlot = 0;
    int i = 0;

    [Header("Slot闽")]
    public GameObject Slot;
    public GameObject[] slotSelect;
    public Sprite[] Rare;
    public GameObject[] RareStar;

    [Header("璉闽")]
    public GameObject[] page;
    public Text Name, ItemDetails, count;
    public Image image;
    int NowPage = 0;
    int FirstMaterialSlot;
    void Start()
    {
        #region 媚旧
        i = 0;
        materials = Resources.LoadAll<Item>("material");
        foreach (Item item in materials)
        {
            if (item.have)
            {
                GameObject slot = Instantiate(Slot, material);
                materialSlot.Add(slot);

                SlotID slotID = slot.GetComponent<SlotID>();
                slotID.ID = i;
                slotID.sprite = item.sprite;
                slotID.Name = item.Name;
                slotID.ItemDetails = item.ItemDetails;
                slotID.count = item.Count;
                slotID.Rare = item.Rare;
                slotID.transform.GetChild(0).GetComponent<Image>().sprite = Rare[slotID.Rare - 1];
                slotID.transform.GetChild(1).GetComponent<Image>().sprite = slotID.sprite;
                Instantiate(RareStar[slotID.Rare - 1], slot.transform);


                if (i == 0)
                {
                    FirstMaterialSlot = slotID.Rare - 1;
                    Name.text = slotID.Name;
                    ItemDetails.text = slotID.ItemDetails;
                    count.text = "┮计秖:" + slotID.count;
                    image.sprite = slotID.sprite;

                    Instantiate(slotSelect[slotID.Rare - 1], slot.transform);
                }

                i++;
                slot.SetActive(true);
            }
        }
        #endregion

        #region 媚旧
        Potions = Resources.LoadAll<Item>("Potion");
        i = 0;
        foreach (Item item in Potions)
        {
            if (item.have)
            {
                GameObject slot = Instantiate(Slot, Potion);
                PotionSlot.Add(slot);

                SlotID slotID = slot.GetComponent<SlotID>();
                slotID.sprite = item.sprite;
                slotID.Name = item.Name;
                slotID.ItemDetails = item.ItemDetails;
                slotID.count = item.Count;
                slotID.transform.GetChild(1).GetComponent<Image>().sprite = slotID.sprite;

                slotID.ID = i;

                i++;
                slot.SetActive(true);
            }
        }
        #endregion

        #region 竟旧
        devices = Resources.LoadAll<Item>("device");
        i = 0;
        foreach (Item item in devices)
        {
            if (item.have)
            {
                GameObject slot = Instantiate(Slot, device);
                deviceSlot.Add(slot);

                SlotID slotID = slot.GetComponent<SlotID>();
                if (item.BackpackSprite != null)
                    slotID.sprite = item.BackpackSprite;
                else
                    slotID.sprite = item.sprite;
                slotID.Name = item.Name;
                slotID.ItemDetails = item.ItemDetails;
                slotID.count = item.Count;
                slotID.transform.GetChild(1).GetComponent<Image>().sprite = slotID.sprite;

                slotID.ID = i;

                i++;
                slot.SetActive(true);
            }
        }
        #endregion

        gameObject.SetActive(true);
    }

    public void TouchSlot(int x, int Rare)
    {
        SlotID slotID;
        switch (NowPage)
        {
            case 0:
                Destroy(materialSlot[nowSlot].transform.GetChild(3).gameObject);
                Instantiate(slotSelect[Rare - 1], materialSlot[x].transform);

                slotID = materialSlot[x].GetComponent<SlotID>();
                Name.text = slotID.Name;
                ItemDetails.text = slotID.ItemDetails;
                count.text = "┮计秖:" + slotID.count;
                image.sprite = slotID.sprite;
                nowSlot = x;
                break;

            case 1:
                Destroy(PotionSlot[nowSlot].transform.GetChild(2).gameObject);
                Instantiate(slotSelect[2], PotionSlot[x].transform);

                slotID = PotionSlot[x].GetComponent<SlotID>();
                Name.text = slotID.Name;
                ItemDetails.text = slotID.ItemDetails;
                count.text = "┮计秖:" + slotID.count;
                image.sprite = slotID.sprite;
                nowSlot = x;
                break;

            case 2:
                Destroy(deviceSlot[nowSlot].transform.GetChild(2).gameObject);
                Instantiate(slotSelect[2], deviceSlot[x].transform);

                slotID = deviceSlot[x].GetComponent<SlotID>();
                Name.text = slotID.Name;
                ItemDetails.text = slotID.ItemDetails;
                count.text = "┮计秖:" + slotID.count;
                image.sprite = slotID.sprite;
                nowSlot = x;
                break;
        }
    }

    public void BackpackClose()
    {
        Destroy(gameObject);
    }
    public void ToPage1()
    {
        if (NowPage != 0)
        {
            if (NowPage == 1 && PotionSlot.Count > 0)
                Destroy(PotionSlot[nowSlot].transform.GetChild(2).gameObject);
            else if (deviceSlot.Count > 0)
                Destroy(deviceSlot[nowSlot].transform.GetChild(2).gameObject);

            nowSlot = 0;
            page[NowPage].SetActive(false);
            page[0].SetActive(true);

            if (material.childCount > 0)
            {
                Instantiate(slotSelect[FirstMaterialSlot], materialSlot[0].transform);
                SlotID slotID = material.GetChild(0).GetComponent<SlotID>();
                Name.text = slotID.Name;
                ItemDetails.text = slotID.ItemDetails;
                count.text = "┮计秖:" + slotID.count;
                image.sprite = slotID.sprite;
            }
            else
            {
                Name.text = "";
                ItemDetails.text = "";
                count.text = "┮计秖:" + "0";
                image.sprite = null;
            }

            NowPage = 0;
        }
    }
    public void ToPage2()
    {
        if (NowPage != 1)
        {
            if (NowPage == 0 && materialSlot.Count > 0)
                Destroy(materialSlot[nowSlot].transform.GetChild(3).gameObject);
            else if (deviceSlot.Count > 0)
                Destroy(deviceSlot[nowSlot].transform.GetChild(2).gameObject);

            nowSlot = 0;
            page[NowPage].SetActive(false);
            page[1].SetActive(true);

            if (Potion.childCount > 0)
            {
                Instantiate(slotSelect[2], PotionSlot[0].transform);
                SlotID slotID = Potion.GetChild(0).GetComponent<SlotID>();
                Name.text = slotID.Name;
                ItemDetails.text = slotID.ItemDetails;
                count.text = "┮计秖:" + slotID.count;
                image.sprite = slotID.sprite;
            }
            else
            {
                Name.text = "";
                ItemDetails.text = "";
                count.text = "┮计秖:" + "0";
                image.sprite = null;
            }

            NowPage = 1;
        }
    }
    public void ToPage3()
    {
        if (NowPage != 2)
        {
            if (NowPage == 0 && materialSlot.Count > 0)
                Destroy(materialSlot[nowSlot].transform.GetChild(3).gameObject);
            else if (PotionSlot.Count > 0)
                Destroy(PotionSlot[nowSlot].transform.GetChild(2).gameObject);

            nowSlot = 0;

            page[NowPage].SetActive(false);
            page[2].SetActive(true);

            if (device.childCount > 0)
            {
                Instantiate(slotSelect[2], deviceSlot[0].transform);
                SlotID slotID = device.GetChild(0).GetComponent<SlotID>();
                Name.text = slotID.Name;
                ItemDetails.text = slotID.ItemDetails;
                count.text = "┮计秖:" + slotID.count;
                image.sprite = slotID.sprite;
            }
            else
            {
                Name.text = "";
                ItemDetails.text = "";
                count.text = "┮计秖:" + "0";
                image.sprite = null;
            }


            NowPage = 2;
        }
    }

}
