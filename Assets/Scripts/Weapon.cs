using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private int projectileVelocity;
    [SerializeField]
    private GameObject FirePoint;
    [SerializeField]
    private GameObject Camera;
    [SerializeField]
    private KeyCode shoot = (KeyCode.Mouse0);
    [SerializeField]
    private KeyCode reload = (KeyCode.R);
    [SerializeField]
    private int ammunition;
    [SerializeField]
    private int magAmmunition;
    [SerializeField]
    private int maxMagAmmunition;

    public static Action<int, int> AmmoUpdate;
    public static Action Hit;

    LayerMask enemyLayerMask;

    private LineRenderer line;

    void Awake()
    {
        enemyLayerMask = LayerMask.GetMask("Enemy");

        line = gameObject.AddComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = 0.02f;
        line.endWidth = 0.02f;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = Color.yellow;
        line.endColor = Color.yellow;
    }

    private void Start()
    {
        ammunition = 150;
        maxMagAmmunition = 16;
        magAmmunition = maxMagAmmunition;
    }
    void Update()
    {
        if (Input.GetKeyDown(shoot)) Shoot();
        if (Input.GetKeyDown(reload)) Reload();
    }

    void Shoot()
    {
        if(magAmmunition > 0)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);

            // Oddanie strza³u w Enemy Layer
            if (Physics.Raycast(Camera.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, enemyLayerMask))
            { 
                // Linia w Game View
                line.SetPosition(0, ray.origin);
                line.SetPosition(1, hit.point);

                Hit?.Invoke();
            }
           
            ammunition--;
            magAmmunition--;
            AmmoUpdate?.Invoke(magAmmunition, maxMagAmmunition);
        }
        
    }

    void Reload()
    {
        if (ammunition - maxMagAmmunition < maxMagAmmunition + magAmmunition)
        {
            ammunition = ammunition - (maxMagAmmunition - magAmmunition);
            magAmmunition = ammunition;
            maxMagAmmunition = ammunition;
        } else {
            ammunition = ammunition - (maxMagAmmunition - magAmmunition);
            magAmmunition = maxMagAmmunition;   
        }
        
        
        AmmoUpdate?.Invoke(magAmmunition, maxMagAmmunition);
        
    }

}
