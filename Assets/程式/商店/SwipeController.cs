using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour,IEndDragHandler
{
    [Header("主設定")]
    public int MaxPage;
    int NowPage;
    Vector3 targetPos;
    public Vector3 pageStep;
    public RectTransform levelPageRect;

    public float tweenTime;
    public LeanTweenType tweenType;

    float dragThreshould;

    [Header("下一頁設定")]

    public Sprite[] sprite;
    public Image[] image; //0上一頁;1下一頁

    private void Start()
    {
        NowPage = 1;
        targetPos = levelPageRect.localPosition;
        dragThreshould = Screen.width / 15;
    }
    public void NextPage()
    {
        if (NowPage < MaxPage)
        {
            NowPage++;
            targetPos += pageStep;
            MovePage();
        }
        else
        {
            NowPage = 1;
            targetPos -= pageStep * (MaxPage - 1);
            MovePage();
        }
    }
    public void PreviousPage()
    {
        if (NowPage > 1)
        {
            NowPage--;
            targetPos -= pageStep;
            MovePage();
        }
        else
        {
            NowPage = MaxPage;
            targetPos += pageStep*(MaxPage-1);
            MovePage();
        }
    }
    void MovePage()
    {
        levelPageRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);

        if (NowPage == 1)
            image[0].sprite = sprite[MaxPage - 1];
        else
            image[0].sprite = sprite[NowPage - 2];

        if (NowPage == MaxPage)
            image[1].sprite = sprite[0];
        else
            image[1].sprite = sprite[NowPage];
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      if(Mathf.Abs(eventData.position.x-eventData.pressPosition.x)> dragThreshould)
        {
            if (eventData.position.x > eventData.pressPosition.x)
                PreviousPage();
            else
                NextPage();
        }
      else
        {
            MovePage();
        }
    }
}
