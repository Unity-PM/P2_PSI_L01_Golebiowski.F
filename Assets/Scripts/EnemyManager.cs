using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField]
    private int health;
    private void Start()
    {
        health = 100;
    }

    public void Hit(int damageTaken)
    {
        if (health > 1) {
            health -= damageTaken;
            if (health < 1) {
                Destroy(gameObject);
            }
        }
        else Destroy(gameObject);
    }

}
