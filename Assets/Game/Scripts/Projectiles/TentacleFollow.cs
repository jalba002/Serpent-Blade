using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody))]
public class TentacleFollow : Bullet
{
    public float speed = 8f;
    private Rigidbody rb;

    private IEnumerator coroutine;
    public override void InstantiateBullet(Vector3 center)
    {
        var a = FindObjectOfType<CharacterController>();
        Vector3 playerPos = a.transform.position;
        playerPos.y = transform.position.y;
        Vector3 direction = playerPos - transform.position;
        transform.forward = direction.normalized;
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        var vfx = GetComponentInChildren<VisualEffect>();
        float duration = vfx.GetFloat("Duration");
        float tentacleDuration = vfx.GetFloat("Tentacle Duration");
        float destroyTime = duration + tentacleDuration;
        coroutine = CastDamage(center, duration, destroyTime);
        StartCoroutine(coroutine);
    }

    IEnumerator CastDamage(Vector3 center, float delay, float destroyTime)
    {
        yield return new WaitForSeconds(delay);
        DealDamageArea(center, 0.5f);
        rb.velocity = Vector3.zero;
        GetComponentInChildren<VisualEffect>().Stop();
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }
    
    private void Update()
    {
        // Follow player?
        
    }

    protected override void CheckOutOfGround()
    {
        base.CheckOutOfGround();
    }

}
