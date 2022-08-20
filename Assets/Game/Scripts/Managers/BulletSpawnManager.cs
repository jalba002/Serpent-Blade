using System;
using System.Collections;
using System.Collections.Generic;
using Boss;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public GameObject _debugParticlePrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnBullet(UnityEngine.Object prefab, Vector3 position)
    {
        Bullet a = Instantiate(prefab as GameObject, position, Quaternion.identity).GetComponent<Bullet>();
        a.InstantiateBullet(position);
    }

    public void OnAttack()
    {
        Handspawn();
    }
    
    void Handspawn()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, 50f);
        SpawnBullet(_debugParticlePrefab, hit.point);               
    }
     
}
