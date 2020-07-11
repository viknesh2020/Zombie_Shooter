using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage;
    public float distance;
    public float impactForce;

    public float maxAmmo;
    public float currentAmmo;

    public Camera playerCam;
      
    public int enemyLayer;
    public AudioSource gunAudios;

    public ParticleSystem hitEffect;
   
    // Use this for initialization
    void Start()
    {
        currentAmmo = maxAmmo;
        enemyLayer = 1<<8;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
        {
            Shoot();
            Debug.Log("Ammo reducing...");
        }
               
    }

    public void Shoot()
    {
        RaycastHit hit;
        currentAmmo--;
        gunAudios.Play();
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, distance, enemyLayer))
        {
            Instantiate(hitEffect, hit.point, Quaternion.identity);
            hitEffect.Play();
            //ZombieHealth zombieHealth = hit.transform.GetComponent<ZombieHealth>();
            GameObject zombieHealth = hit.transform.root.gameObject;
            if (zombieHealth.GetComponent<ZombieHealth>() != null)
            {
                zombieHealth.GetComponent<ZombieHealth>().GetDamage(damage);
                
            }
        } else if (currentAmmo <= 0)
        {
            currentAmmo = 0;
        }             
    }
}
