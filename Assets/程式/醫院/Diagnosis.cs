using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Diagnosis : MonoBehaviour, IPointerClickHandler
{
    DoctorResult doctorResult;
    public int ID;
    public Sprite Check_True, Check_False;
    public Image CheckBox;
   public  bool NowCheck;


    private void Start()
    {
        doctorResult = GameObject.Find("¶EÂ_®Ñ").GetComponent<DoctorResult>();
    }

    private void OnEnable()
    {

        if (!NowCheck)
        {
            CheckBox.sprite = Check_False;
        }
    }
    void Update()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        NowCheck = !NowCheck;

        if (NowCheck)
        {
            CheckBox.sprite = Check_True;
            doctorResult.result.Add(ID);
        }

        else
        {
            CheckBox.sprite = Check_False;
            doctorResult.result.Remove(ID);
        }



    }
}
