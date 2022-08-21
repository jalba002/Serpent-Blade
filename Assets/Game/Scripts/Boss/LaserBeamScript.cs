using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect))]
public class LaserBeamScript : MonoBehaviour
{
    private VisualEffect _visualEffect;
    public float maxDistance = 10f;
    public float rayScale = 1f;
    public LayerMask laserRay;

    public GameObject finalImpactBall;
    private GameObject instantiatedBall;

    private void Awake()
    {
        _visualEffect = GetComponent<VisualEffect>();
        _visualEffect.enabled = false;
        
    }

    private void Start()
    {
        instantiatedBall = Instantiate(finalImpactBall, transform.position, Quaternion.identity);
        instantiatedBall.SetActive(false);
    }

    public void Update()
    {
        RaycastForLengthAndEndball();
    }

    private void RaycastForLengthAndEndball()
    {
        var length = _visualEffect.GetFloat("LaserSize");
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance, laserRay))
        {
            _visualEffect.SetFloat("LaserSize", hit.distance * rayScale);
            instantiatedBall.transform.position = hit.point;
        }
    }

    public void Shoot()
    {
        _visualEffect.enabled = true;
        _visualEffect.SendEvent("Laser");
        //_visualEffect.Play();
        instantiatedBall.SetActive(true);
        // Start coroutine to end it.
        // With the duration and stuff.
    }

    public void Stop()
    {
        _visualEffect.Stop();
        _visualEffect.enabled = false;
        instantiatedBall.SetActive(false);
    }

    public void Showcase()
    {
        _visualEffect.enabled = true;
        _visualEffect.SendEvent("Showcase");
    }
    
}
