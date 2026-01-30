using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private Weapon Weapon;
    private void Start()
    {
        health = 200;
    }

    public void Hit(int damageTaken)
    {
        if (health > 1)
        {
            health -= damageTaken;
            if (health < 1)
            {
                // Destroy(gameObject);
            }
        }
        // else Destroy(gameObject);
    }
}
