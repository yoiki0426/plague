using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProGame : MonoBehaviour, IPointerClickHandler
{
    [Header("基礎設定")]
    public Slider slider;
    int NowIndex = 0;
    public float Speed;

    public float frameInterval;
    float frameCounter = 0;
    bool stop = false;
    bool add;

    [Header("初始隨機設定")]
    int nowRandom;
    public Image sliderimage;
    public Sprite[] sprites;
    public int[] prefect; // +8
    public int[] great; //  +37

    [Header("結算設定")]
    public GameObject EndGameObject;
    public Item item;
    public Image EndImage;
    public Sprite[] EndSprite;
    public Text EndText;
    public Text EndPro;
    bool end = false;

    private void OnEnable()
    {
        NowIndex = 0;
        nowRandom = Random.Range(0, sprites.Length);
        sliderimage.sprite = sprites[nowRandom];
        slider.value = NowIndex;
        InvokeRepeating("NowIndex_Add", 0f, Speed);
    }

    private void Update()
    {
        if (!stop)

        { /*// 每帧增加计数器
            frameCounter++;

            // 如果达到设定的帧数间隔，调用 NowIndex_Add 并重置计数器
            if (frameCounter >= frameInterval)
            {
                NowIndex_Add();
                frameCounter = 0;
            }*/

            // 累加時間
               frameCounter += Time.deltaTime;

                // 如果時間達到設定的間隔
                if (frameCounter >= frameInterval)
                {
                    frameCounter -= frameInterval; // 重置计时器，但保留多餘的時間
                    NowIndex_Add();         // 增加計數器
                }
           


        }
        else if(stop&&!end)
        {
            end = true;
            Invoke("End", 0.3f);
        }
    }

    void NowIndex_Add()
    {
        if (NowIndex == 100)
            add = false;
        if (NowIndex == 0)
            add = true;

        if (add)
            NowIndex++;
        else
            NowIndex--;

        slider.value = NowIndex;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        stop = true;
        CancelInvoke();
    }

    public void End()
    {
        if (NowIndex >= prefect[nowRandom] && NowIndex <= prefect[nowRandom] + 9)
        {
            item.Pro += 3;
            EndPro.text = "熟練度增加 3 \n目前熟練度有 " + item.Pro;
            EndText.text = "完美";
            EndImage.sprite = EndSprite[0];
        }
        else if (NowIndex >= great[nowRandom] && NowIndex <= great[nowRandom] + 38)
        {
            item.Pro += 1;
            EndPro.text = "熟練度增加 1 \n目前熟練度有 " + item.Pro;
            EndText.text = "不錯";
            EndImage.sprite = EndSprite[1];
        }
        else
        {
            EndPro.text = "熟練度增加 0 \n目前熟練度有 " + item.Pro;
            EndText.text = "良好";
            EndImage.sprite = EndSprite[2];
        }

        item.Count++;
        if (item.Count > 0)
            item.have = true;
        EndGameObject.SetActive(true);
    }
}
