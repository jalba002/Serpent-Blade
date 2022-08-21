using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletList", menuName = "Data/Bullets/Database")]
public class BulletList : ScriptableObject
{
    [SerializeField]
    protected List<Bullet> bullets;

    public Bullet GetBulletByName(string name)
    {
        var foundbullet = bullets.Find(x => String.Equals(x.name, name, StringComparison.CurrentCultureIgnoreCase));
        return foundbullet;
    }
}
