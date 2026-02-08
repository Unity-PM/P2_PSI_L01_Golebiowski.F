using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField]
    private int health;
    [SerializeField]
    private Weapon Weapon;
    [SerializeField]
    private ParticleSystem DeathParticle;
    [SerializeField]
    private GameObject DeathSound;

    public static Action Death;

    private void Start()
    {
        health = 100;
    }

    public void Hit(int damageTaken)
    {
        if (health > 1)
        {
            health -= damageTaken;
            if (health < 1)
            {
                Destroy(gameObject);

                DeathSound.transform.position = transform.position;
                DeathSound.GetComponent<AudioSource>().Play();
                
                DeathParticle.transform.position = transform.position;
                DeathParticle.Play();
                Death.Invoke();
            }
        }
        else
        {
            Destroy(gameObject);

            DeathSound.transform.position = transform.position;
            DeathSound.GetComponent<AudioSource>().Play();

            DeathParticle.transform.position = transform.position;
            DeathParticle.Play();
            Death.Invoke();
        }
        
    }

}
