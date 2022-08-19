using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody))]
public class SlashProjectile : MonoBehaviour
{
    // Migrate this into scriptableObjects.
    public float speed = 5f;
    [SerializeField] private VisualEffect _visualEffect;
    private IEnumerator coholder;

    private bool IsStopped = false;
    public void Shoot()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        // Set destroy when the ground duration fades completely.
        
        float swordDuration = _visualEffect.GetFloat("Sword Lifetime");
        float trailDuration = _visualEffect.GetFloat("Trail Lifetime");
        
        coholder = SwordTimer(swordDuration, trailDuration);
        StartCoroutine(coholder);
    }

    private void FixedUpdate()
    {
        if (IsStopped) return;
    
        RaycastHit hit;
    
        Vector3 distance = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        if (Physics.Raycast(distance, transform.TransformDirection(-Vector3.up), out hit, 2))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
        else
        {
            //transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            IsStopped = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    IEnumerator SwordTimer(float swordTime, float trailTime)
    {
        yield return new WaitForSeconds(swordTime);
        _visualEffect.Stop();
        IsStopped = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(trailTime + 0.2f);
        Destroy(this.gameObject);
    }
}
