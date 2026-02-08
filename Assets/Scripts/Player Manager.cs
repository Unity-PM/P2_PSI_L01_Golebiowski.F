using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private Weapon Weapon;
    [SerializeField]
    private AudioSource endGameSound;

    public static Action<int> UpdateHealth;

    private void Start()
    {
        health = 200;
    }

    public void Hit(int damageTaken)
    {
        UpdateHealth?.Invoke(health);
        if (health > 1)
        {
            health -= damageTaken;
            if (health < 1)
            {
                Application.Quit();
            }
        }
        else
        {
            Application.Quit();
        }

        
    }
}
