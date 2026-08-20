using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Certificate : MonoBehaviour
{
    public Image playerImage;
    public GameObject[] Sex;
    public Text Name, Renown;
    [HideInInspector] public GameCtrl gameCtrl;

    void Start()
    {
        Renown.text = gameCtrl.Renown.ToString();
    }

    public void EndThis()
    {
        Destroy(gameObject);
    }
}
