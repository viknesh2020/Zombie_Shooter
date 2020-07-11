using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject player;

    public float playerMaxHealth;
    public float playerCurrentHealth;

    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDamage(float damageAmount)
    {
        playerCurrentHealth -= damageAmount;
        if (playerCurrentHealth <= 0)
        {
            //GAME OVER !
        }
    }
}
