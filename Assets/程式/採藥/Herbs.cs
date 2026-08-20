using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Herbs : MonoBehaviour , IPointerClickHandler
{

    public CollectionGame collectionGame;
    public bool StunHerb;
    Animator animator;
   
    void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("EndKusa", collectionGame.HerbContinued_Time);
       
    }

    // Update is called once per frame
    void Update()
    {

    }
  
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!collectionGame.stun)
        {
            CancelInvoke();

            if (StunHerb)
            {
                animator = gameObject.GetComponent<Animator>();
                animator.SetBool("get", true);
                collectionGame.stun = true;
                collectionGame.Stun_red.SetActive(true);
                Invoke("RemoveStun", collectionGame.Stun_time);
            }
            else
            {
                collectionGame.score++;
                animator.SetBool("Get", true);

                if(collectionGame.SpriteIndex==0)
                    collectionGame.SpriteIndex = 1;
                else
                    collectionGame.SpriteIndex = 0;

                collectionGame.PlayerImage.sprite = collectionGame.Player[collectionGame.SpriteIndex];
                Invoke("EndKusa", 0.2f);
            }
        }

    }
        void EndKusa()
    {
        Destroy(gameObject);
    }

    void RemoveStun()
    {
        collectionGame.Stun_red.SetActive(false);
        collectionGame.stun = false;
        Destroy(gameObject);
    }
}
