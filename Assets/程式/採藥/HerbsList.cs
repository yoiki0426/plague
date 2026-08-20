using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbsList : MonoBehaviour
{
    public Item[] PassbleHerbs1;
    public Item[] PassbleHerbs2;
    public Item[] PassbleHerbs3;
    public CollectionGame collectionGame;

    public void GameStart()
    {
        collectionGame.PassbleHerbs1 = PassbleHerbs1;
        collectionGame.PassbleHerbs2 = PassbleHerbs2;
        collectionGame.PassbleHerbs3 = PassbleHerbs3;
        collectionGame.gameObject.SetActive(true);
    }
}
