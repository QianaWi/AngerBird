using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bird : MonoBehaviour {

    private bool isClicked = false;
    public float maxDis = 1.5f;

    [HideInInspector]
    public SpringJoint2D springJoint;
    protected Rigidbody2D rgBD;

    public LineRenderer rightLine;
    public Transform rightPos;

    public LineRenderer leftLine;
    public Transform leftPos;

    public GameObject goBoom;

    protected TestMyTrail myTrail;

    [HideInInspector]
    public bool canMove = false;//解决小鸟没销毁之前可以被点击的bug
    [HideInInspector]
    public float cameraSmooth = 2; //相机跟随的移动速度倍率.


    public AudioClip selectAudio;
    public AudioClip flyAudio;

    private bool isFlying = false;
    public bool isReleased = false;//飞出去鼠标释放.

    public Sprite hurtSp;
    protected SpriteRenderer hurtSpRender;


    private void OnMouseDown()
    {
        if (canMove)
        {
            AudioPlay(selectAudio);

            isClicked = true;
            rgBD.isKinematic = true;
        }
    }


    private void OnMouseUp()
    {
        if (canMove)
        {
            isClicked = false;
            rgBD.isKinematic = false;
            Invoke("Fly", 0.1f);

            //禁用划线组件.
            rightLine.enabled = false;
            leftLine.enabled = false;

            canMove = false;
        }
    }

    void Awake()
    {
        springJoint = GetComponent<SpringJoint2D>();
        rgBD = GetComponent<Rigidbody2D>();

        myTrail = GetComponent<TestMyTrail>();

        hurtSpRender = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // 判断是否点击UI界面.
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }


        if (isClicked)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 原点参考不一样，所以需要设置下才能看到小鸟
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);

            //下面进行位置限定.
            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)
            {
                var pos = (transform.position - rightPos.position).normalized;
                pos *= maxDis;
                transform.position = pos + rightPos.position;
            }

            Line();
        }

        // 相机跟随.
        var posX = transform.position.x;
        var cameraPos = Camera.main.transform.position;
        Camera.main.transform.position = Vector3.Lerp(cameraPos, new Vector3(Mathf.Clamp(posX, 0, 12), cameraPos.y, cameraPos.z), cameraSmooth*Time.deltaTime);

        if (isFlying)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowSkill();
            }
        }
    }

    void Fly()
    {
        isReleased = true;
        isFlying = true;
        AudioPlay(flyAudio);

        myTrail.StartTrail();

        springJoint.enabled = false;
        Invoke("Next", 3);
    }

    void Line()
    {
        rightLine.enabled = true;
        leftLine.enabled = true;

        rightLine.SetPosition(0, rightPos.position);
        rightLine.SetPosition(1, transform.position);

        leftLine.SetPosition(0, leftPos.position);
        leftLine.SetPosition(1, transform.position);

    }

    // 下一只小鸟飞出.
    protected virtual void Next()
    {
        GameMgr.Instance.BirdLists.Remove(this);
        Destroy(gameObject);
        Instantiate(goBoom, transform.position, Quaternion.identity);
        GameMgr.Instance.NextBird();
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        isFlying = false;


        myTrail.ClearTrail();
    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    // 释放技能.
    public virtual void ShowSkill()
    {
        isFlying = false;


    }

    public void Hurt()
    {
        hurtSpRender.sprite = hurtSp;
    }
}
