using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class InstantDamage : Bullet
{
    public override void InstantiateBullet(Vector3 center)
    {
        base.InstantiateBullet(center);
        // Get the area of course, and the duration.
        var visualeffect = GetComponentInChildren<VisualEffect>();
        float duration = visualeffect.GetFloat("Duration");
        float area = visualeffect.GetFloat("Size");
        
        DealDamageArea(center, area*2f);
        Destroy(this.gameObject, duration*2f);
    }
}
