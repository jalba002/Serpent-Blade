using System;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileShooterManager : MonoBehaviour
{
    public GameObject projectile;

    private bool readyToShoot = false;
    
    private Vector3 startingPoint = Vector3.zero;
    private Vector3 destinationPoint = Vector3.zero;
    
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootProjectile();            
        }
    }

    private void ShootProjectile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, 500);

        if (!readyToShoot)
        {
            startingPoint = hit.point;
            readyToShoot = true;
        }
        else
        {
            destinationPoint = hit.point;
            SpawnProjectile();
            readyToShoot = false;
        }
    }

    private void SpawnProjectile()
    {
        // Instantiat eprojectile
        var projectileObject = Instantiate(projectile, startingPoint, Quaternion.identity) as GameObject;
        
        RotateToDestination(projectileObject, true);
        projectileObject.GetComponent<SlashProjectile>().Shoot();

        startingPoint = Vector3.zero;
        destinationPoint = Vector3.zero;
    }

    private void RotateToDestination(GameObject obj, bool onlyY)
    {
        var direction = (destinationPoint - startingPoint).normalized;
        var rotation = Quaternion.LookRotation(direction);

        if (onlyY)
        {
            rotation.x = 0f;

            rotation.z = 0f;
        }

        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }

    private void OnDrawGizmos()
    {
        if(startingPoint != Vector3.zero)
            Gizmos.DrawSphere(startingPoint, 0.5f);
        if(destinationPoint != Vector3.zero)
            Gizmos.DrawSphere(destinationPoint, 0.5f);
    }
}
