using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{

    public float MaxSpeed = 8;
    public float MinSpeed = 4;
    public Sprite HurtSp;

    private SpriteRenderer spRender;

    public GameObject goBoom;

    public GameObject goScore;

    public bool isPig = false; // 添加标记是为了让猪和木头使用同一份脚本.


    public AudioClip hurtAudio;
    public AudioClip deadAudio;
    public AudioClip birdCollisionAudio;


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioPlay(birdCollisionAudio, collision.transform.position);

            collision.transform.GetComponent<Bird>().Hurt();
        }

        if (collision.relativeVelocity.magnitude >= MaxSpeed)
        {
            Dead();
        }
        else if (collision.relativeVelocity.magnitude > MinSpeed && collision.relativeVelocity.magnitude < MaxSpeed)
        {
            spRender.sprite = HurtSp;

            AudioPlay(hurtAudio, transform.position);
        }


    }

    void Awake()
    {
        spRender = GetComponent<SpriteRenderer>();

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Dead()
    {
        if (isPig)
        {
            GameMgr.Instance.PigLists.Remove(this);
        }

        Destroy(gameObject);

        AudioPlay(deadAudio, transform.position);

        Instantiate(goBoom, transform.position, Quaternion.identity);

        var go = Instantiate(goScore, transform.position + new Vector3(0, 0.5f, 0) , Quaternion.identity);
        Destroy(go, 1.5f);
    }

    public void AudioPlay(AudioClip clip, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos);
    }
}
