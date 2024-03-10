using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public GameObject player;
    public Canvas canvas;

    public int asteroidCount;
    private int level = 0;
    public GameObject asteroidPrefab;
    public Sprite[] sprites;

    public int poolSize = 10;
    public List<EnemyScript> enemyPool = new List<EnemyScript>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitializePool();
    }

    private void Start()
    {
        player.SetActive(true);
        canvas.enabled = false;
    }

    private void Update()
    {
        if(player.GetComponent<PlayerScript>().dead)
        {
            player.SetActive(false);
            canvas.enabled = true;
        }

        if (asteroidCount == 0)
        {
            level++;
            int numAsteroids = 2 + (2 * level);
            for ( int i = 0; i < numAsteroids; i++)
            {
                print("getting enemy");
                GetEnemy();
            }
        }
    }
    /*public void SpawnAsteroid()
    {
        //Spawn on edges of screen
        float offset = Random.Range(0f, 1f);
        Vector2 viewportSpawnPosition = Vector2.zero;

        //choose which edge of screen to spawn on
        int edge = Random.Range(0, 4);
        if(edge == 0)
        { viewportSpawnPosition = new Vector2(offset, 0); }
        else if(edge == 1)
        { viewportSpawnPosition = new Vector2(offset, 1); }
        else if (edge == 2)
        { viewportSpawnPosition = new Vector2(0, offset); }
        else if (edge == 4)
        { viewportSpawnPosition = new Vector2(1, offset); }

        //Create Asteroid
        Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
        GameObject asteroid = Instantiate(asteroidPrefab, worldSpawnPosition, Quaternion.identity);
        EnemyScript enemyScript = asteroid.GetComponent<EnemyScript>();
        enemyScript.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        enemyScript.size = Random.Range(2, 4);
        enemyScript.gameManager = this;
    }*/


    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void InitializePool()
    {
        asteroidCount = 0;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemyObject = Instantiate(asteroidPrefab);
            EnemyScript enemyScript = enemyObject.GetComponent<EnemyScript>();
            enemyPool.Add(enemyScript);
            enemyObject.SetActive(false);
        }
    }

    public EnemyScript GetEnemy()
    {
        print("Get Enemy");
        asteroidCount++;

        foreach (EnemyScript enemy in enemyPool)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemy.gameObject.SetActive(true);
                SetUpEnemy(enemy);
                return enemy;
            }
        }

        GameObject enemyObject = Instantiate(asteroidPrefab);
        EnemyScript enemyScript = enemyObject.GetComponent<EnemyScript>();
        enemyPool.Add(enemyScript);
        SetUpEnemy(enemyScript);
        return enemyScript;
    }

    public void SetUpEnemy(EnemyScript enemy)
    {
        enemy.gameObject.SetActive(true);

        //Spawn on edges of screen
        float offset = Random.Range(0f, 1f);
        Vector2 viewportSpawnPosition = Vector2.zero;

        //choose which edge of screen to spawn on
        int edge = Random.Range(0, 4);
        if (edge == 0)
        { viewportSpawnPosition = new Vector2(offset, 0); }
        else if (edge == 1)
        { viewportSpawnPosition = new Vector2(offset, 1); }
        else if (edge == 2)
        { viewportSpawnPosition = new Vector2(0, offset); }
        else if (edge == 4)
        { viewportSpawnPosition = new Vector2(1, offset); }

        Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
        enemy.gameObject.transform.position = worldSpawnPosition;
        enemy.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        enemy.size = Random.Range(2, 4);
        enemy.StartUp();
        //enemy.gameManager = this;
    }
}
