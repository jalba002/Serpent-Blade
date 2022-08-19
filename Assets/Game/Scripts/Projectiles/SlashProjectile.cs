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
    public void Shoot()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        // Set destroy when the ground duration fades completely.
        
        float swordDuration = _visualEffect.GetFloat("Sword Lifetime");
        float trailDuration = _visualEffect.GetFloat("Trail Lifetime");
        
        coholder = SwordTimer(swordDuration, trailDuration);
        StartCoroutine(coholder);
    }

    IEnumerator SwordTimer(float swordTime, float trailTime)
    {
        yield return new WaitForSeconds(swordTime);
        _visualEffect.Stop();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(trailTime + 0.2f);
        Destroy(this.gameObject);
    }
}
