using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : Bird {

    public override void ShowSkill()
    {
        base.ShowSkill();

        Vector3 speed = rgBD.velocity;
        speed *= -1;
        rgBD.velocity = speed;
    }

}
