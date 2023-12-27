using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird {
    // 黑色小鸟技能是爆炸.


    public List<Pig> enemyLists = new List<Pig>();



    // 大的包围盒是为了框选黑色小鸟周围的敌人
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            enemyLists.Add(collider.gameObject.GetComponent<Pig>());
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            enemyLists.Remove(collider.gameObject.GetComponent<Pig>());
        }

    }


    public override void ShowSkill()
    {
        base.ShowSkill();


        if (enemyLists.Count > 0 && enemyLists != null)
        {
            for (int i = 0; i < enemyLists.Count; ++i)
            {
                enemyLists[i].Dead();
            }
        }

        Clear();
    }

    void Clear()
    {
        rgBD.velocity = Vector3.zero;

        Instantiate(goBoom, transform.position, Quaternion.identity);

        hurtSpRender.enabled = false;

        GetComponent<CircleCollider2D>().enabled = false;

        myTrail.ClearTrail();
    }

    protected override void Next()
    {
        GameMgr.Instance.BirdLists.Remove(this);

        Destroy(gameObject);

        GameMgr.Instance.NextBird();
    }
}
