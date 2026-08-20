using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FileBoard : MonoBehaviour, IPointerClickHandler
{
    public GameObject doctorResult,costomer;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        doctorResult.SetActive(true);
        gameObject.SetActive(false);
    }

}
