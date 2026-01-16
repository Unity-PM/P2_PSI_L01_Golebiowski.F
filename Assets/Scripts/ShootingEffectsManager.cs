using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;

public class ShootingEffectsManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem muzzleFlashParticles;
    [SerializeField]
    private GameObject muzzleFlashLight;
    [SerializeField]
    private GameObject noAmmoSound;
    [SerializeField]
    private GameObject shootingSound;
    [SerializeField]
    private GameObject hitSound;
    [SerializeField]
    private Weapon Weapon;

    [Header("Ustawienia")]
    public bool isPlayerWeapon = true;
    void Start()
    {
        if (isPlayerWeapon)
        {
            Weapon.ShootEvent += OnShoot;
            Weapon.Hit += Hit;
            muzzleFlashLight.GetComponent<Light>().enabled = false;
        }
        else
        {
            Weapon.ShootEvent += OnShoot;
            muzzleFlashLight.GetComponent<Light>().enabled = false;
        }
    }

    private void OnShoot(int ammo)
    {
        if(ammo > 0)
        {
            shootingSound.GetComponent<AudioSource>().volume = 0.5f;
            shootingSound.GetComponent<AudioSource>().Play();
            muzzleFlashParticles.Play();
            muzzleFlashLight.GetComponent<Light>().enabled = true;
            StartCoroutine(DisableMuzzleFlashLight());
        }
        else
        {
            noAmmoSound.GetComponent<AudioSource>().Play();
        }
        
    }
    private void Hit()
    {
        shootingSound.GetComponent<AudioSource>().volume = 0.2f;
        hitSound.GetComponent<AudioSource>().Play();
    }
    IEnumerator DisableMuzzleFlashLight()
    {
        yield return new WaitForSeconds(0.05f);
        muzzleFlashLight.GetComponent<Light>().enabled = false;
    }
}
