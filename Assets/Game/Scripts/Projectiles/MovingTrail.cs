using System.Collections;
using System.Collections.Generic;
using Boss;
using UnityEngine;

public class MovingTrail : Bullet
{
    public override void InstantiateBullet(Vector3 center)
    {
        SnapToGround();
        transform.forward = FindObjectOfType<BossController>().GetBossParent().transform.forward;
    }
}
