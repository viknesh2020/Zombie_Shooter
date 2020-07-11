using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Animations;

public class ZombieHealth : MonoBehaviour
{
    [HideInInspector]
    public GameObject enemy;

    public float maxEnemyHealth;
    public float currentEnemyHealth;

    [HideInInspector]
    public bool enemyDied;
    public bool gettingHit;

    public Image enemyHealthBar;
    [HideInInspector]
    public Dashboard dashBoard;
    public Canvas healthCanvas;

    public ParticleSystem deathEffect;
    public float deathDelay=3f;

    void Start()
    {
        dashBoard = GameObject.Find("GameManager").GetComponent<Dashboard>();
        enemy = this.gameObject;
        currentEnemyHealth = maxEnemyHealth;
        enemyDied = false;
        healthCanvas.worldCamera = Camera.main;

    }

    void Update()
    {
         enemyHealthBar.fillAmount = currentEnemyHealth / maxEnemyHealth;
        if (Input.GetButtonUp("Fire1"))
        {
            gettingHit = false;
        }
    }

    public void GetDamage(float amount)
    {
        currentEnemyHealth -= amount;
        gettingHit = true;

        if (currentEnemyHealth <= 0)
        {
            enemyDied = true;
            Invoke("DestroyEnemy", deathDelay);
            dashBoard.Scoring();
        }
    }

    public void DestroyEnemy()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
}
