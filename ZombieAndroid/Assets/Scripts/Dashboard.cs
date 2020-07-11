using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dashboard : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Gun gun;

    public int scoreMultiplier;
    public int scoreValue;

    public Image playerHealthBar;
   
    public Text ammo;
    public Text score;

    [HideInInspector]
    public string ammoInString;
    [HideInInspector]
    public string scoreInString;

    [HideInInspector]
    public int killCounter;

    void Start()
    {
        killCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthBar.fillAmount = playerHealth.playerCurrentHealth / playerHealth.playerMaxHealth;
     
        ammoInString = gun.currentAmmo.ToString();
        ammo.text = ammoInString;

        scoreValue = killCounter * scoreMultiplier;
        scoreInString = scoreValue.ToString();
        score.text = scoreInString;
    }

    public void Scoring()
    {
        killCounter++;

    }

}
