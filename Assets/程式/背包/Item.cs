using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "New Item")]//自訂道具

public class Item : ScriptableObject
{
    public int id;
    public int Rare;
    public bool have;
    public string Name;

    public int sell;
    public int buy;

    [TextArea(1, 10)]
    public string  ItemDetails;

    public Sprite sprite;
    public Sprite BackpackSprite;
    public int Count;


    public Item[] Recipe_items;
    public int[] Recipe_count;
    public int Pro;
    [TextArea(1, 15)]
    public string [] PotionDetails;
    public Item [] Device;
}
