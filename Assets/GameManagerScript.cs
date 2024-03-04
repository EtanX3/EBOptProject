using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject player;
    public Canvas canvas;

    public int asteroidCount;
    private int level = 0;
    [SerializeField] private EnemyScript asteroidPrefab;

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

        if(asteroidCount == 0)
        {
            level++;
            int numAsteroids = 2 + (2 * level);
            for ( int i = 0; i < numAsteroids; i++)
            {
                SpawnAsteroid();
            }
        }
    }
    public void SpawnAsteroid()
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
        EnemyScript asteroid = Instantiate(asteroidPrefab, worldSpawnPosition, Quaternion.identity);
        asteroid.gameManager = this;
    }


    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
