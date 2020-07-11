using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Properties : MonoBehaviour
    {
        public bool isRanged;
        public bool isMelee;
        public bool isThrowable;
        
        public GameObject rangedWeapon;
        public GameObject meleeWeapon;
        public GameObject throwableWeapon;

        [HideInInspector]
        public float rangedDamage;
        public float meleeDamage;
        public float throwableDamage;

        public float range;
        public float currentAmmo;
        public float maxAmmo;

        public ParticleSystem[] effectsList;
        public Animator weaponAnimator;

        public void InitiateWeaponSystems()
        {

            for(int i = 0; i<effectsList.Length; i++)
            {
                if (rangedWeapon.transform.childCount > 0 && isRanged)
                {
                    effectsList[i] = rangedWeapon.GetComponentInChildren<ParticleSystem>();
                }
                else if(isRanged)
                {
                    effectsList[i] = rangedWeapon.GetComponent<ParticleSystem>();
                }
            }

            if(isRanged || isThrowable)
            {
                currentAmmo = maxAmmo;
            }
  
        }
    
    }
    
    public class Damage : MonoBehaviour
    {
        public Properties properties;


        public void Fire()
        {


            properties.currentAmmo--;




        }

    }

   

}

