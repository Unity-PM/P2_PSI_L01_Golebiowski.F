using System;
using UnityEngine;
using UnityEngine.Timeline;

public class Weapon : MonoBehaviour
{
    [Header("Statystyki Broni")]
    [SerializeField]
    private float damage;

    private float damageTaken;
    private float distance;

    [Header("Amunicja")]
    [SerializeField]
    private int currentAmmo;
    [SerializeField]
    private int maxAmmoInMag;
    [SerializeField]
    private int reserveAmmo;

    [Header("Komponenty")]
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private PlayerWeaponManager player;
    [SerializeField]
    private EnemyAI enemy;

    [Header("Ustawienia")]
    public bool isPlayerWeapon = true;


    public Action<int, int> AmmoUpdate;
    public Action<int> ShootEvent;
    public Action Hit;

    LayerMask enemyLayerMask;

    private LineRenderer line;

    void Awake()
    {
        enemyLayerMask = LayerMask.GetMask("Enemy", "Wall", "Player");

        line = gameObject.AddComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = 0.02f;
        line.endWidth = 0.02f;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = Color.yellow;
        line.endColor = Color.yellow;
        line.enabled = false;
    }

    private void Start()
    {
        currentAmmo = maxAmmoInMag;

        if (isPlayerWeapon)
        {
            player.ShootHandler += Shoot;
            player.ReloadHandler += Reload;
            AmmoUpdate?.Invoke(currentAmmo, maxAmmoInMag);
        }
        else
        {
            enemy.ShootHandler += Shoot;
            enemy.ReloadHandler += Reload;
        }
    }

    void Shoot()
    {
        ShootEvent?.Invoke(currentAmmo);
        if (currentAmmo <= 0)
        {
            return;
        }
        currentAmmo--;

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        // Oddanie strza³u w Enemy Layer
        if (Physics.Raycast(playerCamera.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, enemyLayerMask))
        {
                
            // Linia w Game View
            line.SetPosition(0, ray.origin);
            line.SetPosition(1, hit.point);

            // Obliczanie obra¿enia
            distance = hit.distance;
            if(distance <= 5)
            {
                damageTaken = damage;
            } else if(distance > 5 && distance <=15)
            {
                damageTaken = damage - distance * 0.5f;
            } else if(distance > 15)
            {
                if(damage * 1.4f - distance <= 0)
                {
                    damageTaken = 1;
                }
                else
                {
                    damageTaken = damage * 1.4f - distance;
                }   
            }
            Debug.Log("Odleg³oœæ: " + distance + " Obra¿enia: " + (int)damageTaken);


            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<EnemyManager>().Hit((int)damageTaken);
            } 
            Hit?.Invoke();
        }
        if (reserveAmmo == 0)
        {
            AmmoUpdate?.Invoke(currentAmmo, currentAmmo);
        } else
        {
            AmmoUpdate?.Invoke(currentAmmo, maxAmmoInMag);
        }
            
        
    }

    void Reload()
    {
    // Jeœli magazynek pe³ny lub brak rezerwy -> nie prze³adowujemy
    if (currentAmmo == maxAmmoInMag || reserveAmmo <= 0) return;

    // Obliczamy ile brakuje do pe³na
    int bulletsNeeded = maxAmmoInMag - currentAmmo;

    // Sprawdzamy czy mamy tyle w rezerwie
    if (reserveAmmo >= bulletsNeeded)
    {
        reserveAmmo -= bulletsNeeded;
        currentAmmo += bulletsNeeded;
        AmmoUpdate?.Invoke(currentAmmo, maxAmmoInMag);
    }
    else
    {
        // £adujemy resztkê z rezerwy
        currentAmmo += reserveAmmo;
        reserveAmmo = 0;
        AmmoUpdate?.Invoke(currentAmmo, currentAmmo);
    }
    }

}
