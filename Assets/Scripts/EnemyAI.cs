using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField]
    private Transform player;
    [SerializeField]
    private float rotationSpeed = 5f;
    [SerializeField]
    private float fireRate = 0.5f;

    private float nextFireTime = 0f;


    public Action ShootHandler;
    public Action ReloadHandler;

    LayerMask playerLayerMask;

    private void Awake()
    {
        playerLayerMask = LayerMask.GetMask("Player", "Wall");
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, playerLayerMask))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (Time.time < nextFireTime) return;
                nextFireTime = Time.time + fireRate;
                ShootHandler?.Invoke();
            }

        }

        Vector3 direction = player.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
