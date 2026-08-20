using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Flower;

public class HospitalDialogue : MonoBehaviour, IPointerClickHandler
{
    FlowerSystem flowerSystem;
    public bool Lock = false;
    public int sickID;
    public Sprite sprite, flower;
    public int HumanID;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public string costomerName;
    void Start()
    {
        flowerSystem = FlowerManager.Instance.GetFlowerSystem("Hospital");


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            flowerSystem.Next();
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Lock)
        {
            switch (costomerName)
            {
                #region 安娜薇爾
                case "安娜薇爾":
                    switch (sickID)
                    {
                        case 1:
                            flowerSystem.ReadTextFromResource("文本/安娜薇爾/外傷1");
                            break;
                        case 2:
                            flowerSystem.ReadTextFromResource("文本/安娜薇爾/食物中毒1");
                            break;
                        case 3:
                            flowerSystem.ReadTextFromResource("文本/安娜薇爾/顛笑症1");
                            break;
                        case 4:
                            flowerSystem.ReadTextFromResource("文本/安娜薇爾/史萊姆1");
                            break;
                        case 5:
                            flowerSystem.ReadTextFromResource("文本/安娜薇爾/凋零症1");
                            break;
                        case 6:
                            flowerSystem.ReadTextFromResource("文本/安娜薇爾/燒傷1");
                            break;
                    }
                    break;
                #endregion

                #region 蘿希婭
                case "蘿希婭":
                    switch (sickID)
                    {
                        case 1:
                            flowerSystem.ReadTextFromResource("文本/蘿希婭/外傷1");
                            break;
                        case 2:
                            flowerSystem.ReadTextFromResource("文本/蘿希婭/食物中毒1");
                            break;
                        case 3:
                            flowerSystem.ReadTextFromResource("文本/蘿希婭/顛笑症1");
                            break;
                        case 4:
                            flowerSystem.ReadTextFromResource("文本/蘿希婭/史萊姆1");
                            break;
                        case 5:
                            flowerSystem.ReadTextFromResource("文本/蘿希婭/凋零症1");
                            break;
                        case 6:
                            flowerSystem.ReadTextFromResource("文本/蘿希婭/燒傷1");
                            break;
                    }
                    break;
                #endregion

                #region 奧菲
                case "奧菲":
                    switch (sickID)
                    {
                        case 1:
                            flowerSystem.ReadTextFromResource("文本/奧菲/外傷1");
                            break;
                        case 2:
                            flowerSystem.ReadTextFromResource("文本/奧菲/食物中毒1");
                            break;
                        case 3:
                            flowerSystem.ReadTextFromResource("文本/奧菲/顛笑症1");
                            break;
                        case 4:
                            flowerSystem.ReadTextFromResource("文本/奧菲/史萊姆1");
                            break;
                        case 5:
                            flowerSystem.ReadTextFromResource("文本/奧菲/凋零症1");
                            break;
                        case 6:
                            flowerSystem.ReadTextFromResource("文本/奧菲/燒傷1");
                            break;
                    }
                    break;
                #endregion

                #region 夕花
                case "夕花":
                    switch (sickID)
                    {
                        case 1:
                            flowerSystem.ReadTextFromResource("文本/夕花/外傷1");
                            break;
                        case 2:
                            flowerSystem.ReadTextFromResource("文本/夕花/食物中毒1");
                            break;
                        case 3:
                            flowerSystem.ReadTextFromResource("文本/夕花/顛笑症1");
                            break;
                        case 4:
                            flowerSystem.ReadTextFromResource("文本/夕花/史萊姆1");
                            break;
                        case 5:
                            flowerSystem.ReadTextFromResource("文本/夕花/凋零症1");
                            break;
                        case 6:
                            flowerSystem.ReadTextFromResource("文本/夕花/燒傷1");
                            break;
                    }
                    break;
                #endregion

                #region 洛普
                case "洛普":
                    switch (sickID)
                    {
                        case 1:
                            flowerSystem.ReadTextFromResource("文本/洛普/外傷1");
                            break;
                        case 2:
                            flowerSystem.ReadTextFromResource("文本/洛普/食物中毒1");
                            break;
                        case 3:
                            flowerSystem.ReadTextFromResource("文本/洛普/顛笑症1");
                            break;
                        case 4:
                            flowerSystem.ReadTextFromResource("文本/洛普/史萊姆1");
                            break;
                        case 5:
                            flowerSystem.ReadTextFromResource("文本/洛普/凋零症1");
                            break;
                        case 7:
                            flowerSystem.ReadTextFromResource("文本/洛普/脆化症1");
                            break;
                    }
                    break;
                #endregion

                #region 希爾
                case "希爾":
                    switch (sickID)
                    {
                        case 1:
                            flowerSystem.ReadTextFromResource("文本/希爾/外傷1");
                            break;
                        case 2:
                            flowerSystem.ReadTextFromResource("文本/希爾/食物中毒1");
                            break;
                        case 3:
                            flowerSystem.ReadTextFromResource("文本/希爾/顛笑症1");
                            break;
                        case 4:
                            flowerSystem.ReadTextFromResource("文本/希爾/史萊姆1");
                            break;
                        case 5:
                            flowerSystem.ReadTextFromResource("文本/希爾/凋零症1");
                            break;
                        case 8:
                            flowerSystem.ReadTextFromResource("文本/希爾/靈魂附著1");
                            break;
                    }
                    break;
                #endregion

                #region 彌湛斯
                case "彌湛斯":
                    switch (sickID)
                    {
                        case 1:
                            flowerSystem.ReadTextFromResource("文本/彌湛斯/外傷1");
                            break;
                        case 2:
                            flowerSystem.ReadTextFromResource("文本/彌湛斯/食物中毒1");
                            break;
                        case 3:
                            flowerSystem.ReadTextFromResource("文本/彌湛斯/顛笑症1");
                            break;
                        case 4:
                            flowerSystem.ReadTextFromResource("文本/彌湛斯/史萊姆1");
                            break;
                        case 5:
                            flowerSystem.ReadTextFromResource("文本/彌湛斯/凋零症1");
                            break;
                    }
                    break;
                #endregion

                #region 共通
                default:
                    switch (sickID)
                    {
                        case 1:
                            flowerSystem.ReadTextFromResource("文本/共通/外傷1");
                            break;
                        case 2:
                            flowerSystem.ReadTextFromResource("文本/共通/食物中毒1");
                            break;
                        case 3:
                            flowerSystem.ReadTextFromResource("文本/共通/顛笑症1");
                            break;
                        case 4:
                            flowerSystem.ReadTextFromResource("文本/共通/史萊姆1");
                            break;
                        case 5:
                            flowerSystem.ReadTextFromResource("文本/共通/凋零症1");
                            break;
                        case 6:
                            flowerSystem.ReadTextFromResource("文本/共通/燒傷1");
                            break;
                        case 7:
                            flowerSystem.ReadTextFromResource("文本/共通/脆化症1");
                            break;
                        case 8:
                            flowerSystem.ReadTextFromResource("文本/共通/靈魂附著1");
                            break;
                    }
                    break;
                    #endregion
            }
        }
    }
}
