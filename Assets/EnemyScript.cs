using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int size = 3;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void StartUp()
    {
        rb.velocity = Vector3.zero;
        //Scale based on size
        transform.localScale = .5f * size * Vector3.one;

        //Add movement, making sure bigger asteroids are slower
        Vector2 direction = Random.insideUnitCircle.normalized;
        float spawnSpeed = Random.Range(4f - size, 5f - size);
        rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);
        print("enabled");
    }

    public void Damage()
    {
        GameManagerScript.instance.asteroidCount--;

        if (size > 1)
        {
            for(int i = 0; i < 2; i++)
            {
                EnemyScript newAsteroid = GameManagerScript.instance.GetEnemy();
                newAsteroid.transform.position = transform.position;
                newAsteroid.size = size - 1;
                if (newAsteroid.size > 0)
                {
                    newAsteroid.StartUp();
                    newAsteroid.gameObject.SetActive(true);
                }

            }
        }

        gameObject.SetActive(false);
    }
}