using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour {

	public List<Bird> BirdLists;
    public List<Pig> PigLists;

    public static GameMgr Instance;

    private Vector3 originPos; // 小鸟初始化位置.

    public GameObject win;
    public GameObject lose;
    public GameObject[] Stars;

    private int starsNum; // 玩家获得星星的数量.

    private int totalLevelNum = 10;// 关卡数量.

    void Awake()
    {
        Instance = this;

        if (BirdLists.Count > 0)
        {
            originPos = BirdLists[0].transform.position;

        }
    }

    // Use this for initialization
    void Start () {

        Initialized();

    }
	
	// Update is called once per frame
	void Update () {
		
	}


	private void Initialized()
    {
        for (int i = 0; i < BirdLists.Count; ++i)
        {
            if (i==0)
            {
                BirdLists[i].transform.position = originPos;
                BirdLists[i].enabled = true;
                BirdLists[i].springJoint.enabled = true;
                BirdLists[i].canMove = true;
            }
            else
            {
                BirdLists[i].enabled = false;
                BirdLists[i].springJoint.enabled = false;
                BirdLists[i].canMove = false;

            }
        }
    }


    public void NextBird()
    {
        if (PigLists.Count > 0)
        {
            if (BirdLists.Count > 0)
            {
                // 下只小鸟.
                Initialized();
            }
            else
            {
                // 输了
                lose.SetActive(true);
            }
        }
        else
        {
            // 胜利啦
            win.SetActive(true);
        }
    }

    public void ShowStars()
    {
        StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        for (; starsNum < BirdLists.Count + 1; ++starsNum)
        {
            if (starsNum >= Stars.Length)
            {
                break;
            }

            yield return new WaitForSeconds(0.2f);
            Stars[starsNum].SetActive(true);
        }
    }

    public void Replay()
    {
        SaveData();

        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        SaveData();

        SceneManager.LoadScene(1);
    }

    public void SaveData()
    {
        if (starsNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("curLevel")))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("curLevel"), starsNum);
        }

        int totalStarsNum = 0;
        for (int i = 1; i <= totalLevelNum; i++)
        {
            totalStarsNum += PlayerPrefs.GetInt("level" + i.ToString());
        }

        PlayerPrefs.SetInt("totalStars", totalStarsNum);

    }
}
