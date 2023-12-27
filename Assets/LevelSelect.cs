using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {

	private bool canSelect = false;

    public Sprite spLevelBG;
    private Image image;

    public GameObject[] goStars;

    void Awake()
    {
        image = GetComponent<Image>();
    }

	void Start()
    {
        if (transform.parent.GetChild(0).name == gameObject.name)
        {
            canSelect = true;
        }
        else
        {// 判断当前关卡是否能选择.
            int beforeNum = int.Parse(gameObject.name) - 1;
            if (PlayerPrefs.GetInt("level" + beforeNum.ToString()) > 0)
            {
                canSelect = true;
            }
        }


        if (canSelect)
        {
            image.overrideSprite = spLevelBG;
            transform.Find("num").gameObject.SetActive(true);

            // 获取先做关卡对应的名字，然后获取对应存储的星星个数.
            int count = PlayerPrefs.GetInt("level" + gameObject.name);
            if (count > 0)
            {
                for (int i = 0; i < goStars.Length && i < count; ++i)
                {
                    goStars[i].SetActive(true);
                }
            }
        }
    }

    public void OnSelect()
    {
        if (canSelect)
        {
            PlayerPrefs.SetString("curLevel", "level" + gameObject.name);

            SceneManager.LoadScene(2);
        }
    }

}
