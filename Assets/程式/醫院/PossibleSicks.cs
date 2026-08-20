using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PossibleSicks : MonoBehaviour, IPointerClickHandler
{
    public Sick sick;
    public DoctorResult Result;
    void Start()
    {
        Result = GameObject.Find("¶EÂ_®Ñ").GetComponent<DoctorResult>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Result.ChoseSick = sick;
        Result.finish.text = sick.Name;
        Result.SickMenu_OFF();
        Result.PotionResult();
    }
}
