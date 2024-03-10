using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int size = 3;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //Scale based on size
        transform.localScale = .5f * size * Vector3.one;

        //Add movement, making sure bigger asteroids are slower
        Vector2 direction = new Vector2(Random.value, Random.value).normalized;
        float spawnSpeed = Random.Range(4f - size, 5f - size);
        rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);

        //assign gamemanager
        GameManagerScript.instance.asteroidCount++;
    }

    public void Damage()
    {
        GameManagerScript.instance.asteroidCount--;

        if(size > 1)
        {
            for(int i = 0; i <2; i++)
            {
                EnemyScript newAsteroid = Instantiate(this, transform.position, Quaternion.identity);
                newAsteroid.size = size - 1;
            }
        }
        Destroy(gameObject);
    }
}
