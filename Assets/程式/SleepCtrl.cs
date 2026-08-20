using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepCtrl : MonoBehaviour
{
    public Slider slider;
    int NowTime, AP, AP_Lack, SleepTime_Max, AP_Add, SleepTime;
    public Text AP_Add_text, SleepTime_text;
    private void OnEnable()
    {
        SleepTime = 0;
        AP_Add = 0;
        NowTime = GameObject.Find("GameCtrl").GetComponent<GameCtrl>().NowTime;
        AP = GameObject.Find("GameCtrl").GetComponent<GameCtrl>().Ap;
        AP_Lack = 24 - AP;

        if (AP_Lack > 3)
        {
            SleepTime_Max = 6 + AP_Lack - 3;
        }
        else
        {
            SleepTime_Max = AP_Lack * 2;
        }

        slider.value = 0;
        slider.maxValue = SleepTime_Max;

        slider.onValueChanged.AddListener(UpdateText);
        AP_Add_text.text = "總共恢復" + AP_Add + "AP，睡醒後將有" + (AP + AP_Add) + "AP";
    }

    void UpdateText(float value)
    {
        if (value <= 6)
        {
            AP_Add = (int)value / 2;
        }
        else
        {
            AP_Add = 3 +(int) value - 6;
        }
        SleepTime =(int) value;

        SleepTime_text.text = SleepTime.ToString() + "小時";
        AP_Add_text.text = "總共恢復" + AP_Add + "AP，睡醒後將有" + (AP+AP_Add) + "AP";
    }
   public void Sleep()
    {
        GameObject.Find("GameCtrl").GetComponent<GameCtrl>().AP_Add(AP_Add, SleepTime);
        gameObject.SetActive(false);
    }
    public void Wait()
    {
        gameObject.SetActive(false);
    }
}
