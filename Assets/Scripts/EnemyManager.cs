using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField]
    private int health;
    private void Start()
    {
        health = 10;
        Weapon.Hit += Hit;
    }

    private void Hit()
    {
        if (health > 1) health--;
        else Destroy(gameObject);
    }

}
