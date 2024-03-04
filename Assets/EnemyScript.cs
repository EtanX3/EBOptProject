using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int size = 3;
    public GameManagerScript gameManager;
    private void Start()
    {
        //Scale based on size
        transform.localScale = .5f * size * Vector3.one;

        //Add movement, making sure bigger asteroids are slower
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Random.value, Random.value).normalized;
        float spawnSpeed = Random.Range(4f - size, 5f - size);
        rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);

        //assign gamemanager
        gameManager.asteroidCount++;
    }
    public void Damage()
    {
        gameManager.asteroidCount--;

        if(size > 1)
        {
            for(int i = 0; i <2; i++)
            {
                EnemyScript newAsteroid = Instantiate(this, transform.position, Quaternion.identity);
                newAsteroid.size = size - 1;
                newAsteroid.gameManager = gameManager;
            }
        }
        Destroy(gameObject);
    }
}
