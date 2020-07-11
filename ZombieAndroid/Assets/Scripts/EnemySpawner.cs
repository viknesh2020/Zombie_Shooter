using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] zombies;
    public float lowerAreaLimitX;
    public float higherAreaLimitX;
    public float lowerAreaLimitZ;
    public float higherAreaLimitZ;

    private float randomAreaValueX;
    private float randomAreaValueZ;

    private Vector3 zombieSpawnPos;
    private int zombieCount;

    public GameObject floor;

    void Start()
    {
               
        zombieCount = zombies.Length;
       
        for(int i=1; i<5; i++)
        {
            int randomZombie = Random.Range(0, zombieCount);

            for (int j=0; j<=randomZombie; j++){

                 randomAreaValueX = Random.Range(lowerAreaLimitX, higherAreaLimitX);
                 randomAreaValueZ = Random.Range(lowerAreaLimitZ, higherAreaLimitZ);
                 zombieSpawnPos = new Vector3(randomAreaValueX, 6, randomAreaValueZ);

                Instantiate(zombies[j], zombieSpawnPos, Quaternion.identity);
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
