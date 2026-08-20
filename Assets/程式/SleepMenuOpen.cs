using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepMenuOpen : MonoBehaviour
{
   public  GameObject SleepMemu;
   public void OpenSleepMemu()
    {
        if(!SleepMemu.activeSelf)
        {
            SleepMemu.SetActive(true);
        }
    }
}
