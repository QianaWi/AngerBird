using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour {

    private Animator anim;
    public GameObject button;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);

    }

    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Pause()
    {
        // 1.播放动画
        anim.SetBool("isPause", true);
        button.SetActive(false);
        // 2.暂停
        // 放在动画播完事件中处理

        if (GameMgr.Instance.BirdLists.Count > 0)
        {
            if (GameMgr.Instance.BirdLists[0].isReleased == false)
            {
                // 没有飞出.
                GameMgr.Instance.BirdLists[0].canMove = false;
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        anim.SetBool("isPause", false);

        if (GameMgr.Instance.BirdLists.Count > 0)
        {
            if (GameMgr.Instance.BirdLists[0].isReleased == false)
            {
                // 没有飞出.
                GameMgr.Instance.BirdLists[0].canMove = true;
            }
        }

    }

    public void PauseAnimEnd()
    {
        Time.timeScale = 0;
    }

    public void ResumeAnimEnd()
    {
        button.SetActive(true);
    }
}
