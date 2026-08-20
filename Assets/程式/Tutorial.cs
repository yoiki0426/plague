using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Image image;
    public Sprite[] page;
    int page_index;
    public GameObject[] Buttom; //0 = 上一頁,1 = 下一頁
    public Text NowPage;
    void Start()
    {
        page_index = 0;
        image.sprite = page[page_index];
        NowPage.text = "Page " + (page_index + 1) + "/" + page.Length;
    }

    // Update is called once per frame
    void Update()
    {

    }
    #region 上一頁下一頁
    public void NextPage()
    {
        page_index++;
        image.sprite= page[page_index];
       

        if (page_index == page.Length - 1)
            Buttom[1].SetActive(false);

        if (page_index == 1)
            Buttom[0].SetActive(true);

        NowPage.text = "Page " + (page_index + 1) + "/" + page.Length;
    }
    public void PreviousPage()
    {
        page_index--;
        image.sprite = page[page_index];

        if (page_index == page.Length - 2)
            Buttom[1].SetActive(true);

        if (page_index == 0)
            Buttom[0].SetActive(false);

        NowPage.text = "Page " + (page_index + 1) + "/" + page.Length;
    }
    public void End()
    {
       Destroy(gameObject);
    }
    #endregion
}
