using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : Bullet
{
    public override void InstantiateBullet(Vector3 center)
    {
        base.InstantiateBullet(center);
        // Spawn once
        // Find the ground with raycasts.
        // Place itself there.
        // Destroy self after time.
        if(Physics.Raycast(center, Vector3.down, out RaycastHit hit, 50f))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + 0.1f, transform.position.z);
        }
        else
        {
            transform.position = center;
        }

        if (spawnPrefab)
        {
            var pb = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
        // Get the time it lasts and Destroy it after.
    }
}
