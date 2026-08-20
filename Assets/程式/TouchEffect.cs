using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffect : MonoBehaviour
{
    public GameObject Effect;
    public GameObject NowEffect;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 )
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if(NowEffect != null)
                {
                    CancelInvoke();
                    Destroy(NowEffect);
                }
                NowEffect = Instantiate(Effect, Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 5)), Quaternion.identity, gameObject.transform);
                Invoke("End", 1);
            }
        }
    }

    void End()
    {
        Destroy(NowEffect);
    }
}
