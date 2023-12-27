using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour
{

    public int starsNum = 0;
    private bool canSelect = false;

    public GameObject goLock;
    public GameObject goStars;

    public GameObject goPanel;
    public GameObject goMap;

    public Text starsText;

    public int startLevelNum = 1;
    public int endLevelNum = 3;

    // Use this for initialization
    void Start()
    {
        //PlayerPrefs.DeleteAll();

        // unity提供的键值对存储.
        if (PlayerPrefs.GetInt("totalStarsNum", 0) >= starsNum)
        {

            canSelect = true;
        }

        if (canSelect)
        {
            goLock.SetActive(false);
            goStars.SetActive(true);


            //todo text显示.
            int curTotalNum = 0;
            for (int i = starsNum; i <= endLevelNum; ++i)
            {
                curTotalNum += PlayerPrefs.GetInt("level" + i.ToString(), 0);
            }
            starsText.text = curTotalNum.ToString()+ "/30";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSelected()
    {
        if (canSelect)
        {
            goPanel.SetActive(true);
            goMap.SetActive(false);
        }
    }

    public void OnPanelSelected()
    {
        goPanel.SetActive(false);
        goMap.SetActive(true);
    }
}
