using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnManager : MonoBehaviour
{
    private static BulletSpawnManager _instance;

    public static BulletSpawnManager Instance
    {
        get => _instance;
        set
        {
            if (_instance == null)
                _instance = value;
            else
            {
                Destroy(value);
            }
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnBullet(UnityEngine.Object prefab, Vector3 position)
    {
        Bullet a = Instantiate(prefab as GameObject, position, Quaternion.identity).GetComponent<Bullet>();
        a.InstantiateBullet(position);
    }
}
