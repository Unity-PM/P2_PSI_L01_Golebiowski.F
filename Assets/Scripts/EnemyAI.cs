using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField]
    private Transform playerHitBox;
    [SerializeField]
    private Weapon Weapon;
    [SerializeField]
    private Camera Camera;
    [SerializeField]
    private float rotationSpeed = 5f;
    [SerializeField]
    private float fireRate = 0.5f;

    private float nextFireTime = 0f;


    public Action ShootHandler;
    public Action ReloadHandler;

    LayerMask LayerMask;

    private void Awake()
    {
        LayerMask = LayerMask.GetMask("Player", "Wall");

    }

    private void FixedUpdate()
    {
        Shoot();
        //if(Weapon.getAmmo < 1)
        //{

        //}
    }

    private void Shoot()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.transform.position, transform.forward);

        if (Physics.Raycast(Camera.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, LayerMask))
        {

            if (hit.collider.CompareTag("Player"))
            {
                if (Time.time < nextFireTime) return;
                nextFireTime = Time.time + fireRate;
                ShootHandler?.Invoke();
            }

        }

        // smooth obroty
        Vector3 direction = playerHitBox.position - transform.position;

        Vector3 cameraDirection = playerHitBox.position - Camera.transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        Quaternion cameraLookRotation = Quaternion.LookRotation(cameraDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        Camera.transform.rotation = Quaternion.Slerp(Camera.transform.rotation, cameraLookRotation, Time.deltaTime * rotationSpeed);
    }
}
