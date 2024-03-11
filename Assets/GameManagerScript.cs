using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;
    public GameObject player;
    public Canvas canvas;

    public int asteroidCount;
    private int level = 0;
    [SerializeField] private GameObject asteroidPrefab;
    public Sprite[] sprites;
    [SerializeField] private ParticleSystem explodeParticle;
    bool exploded;

    private enum Edge
    {
        Top,
        Bottom,
        Left,
        Right
    }

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

        player.SetActive(true);
        canvas.enabled = false;
    }

    private void Update()
    {
        if (player.GetComponent<PlayerScript>().dead)
        {
            player.SetActive(false);
            canvas.enabled = true;
            if(!exploded)
            {
                explodeParticle.transform.position = player.transform.position;
                explodeParticle.Play();
                exploded = true;
            }
        }

        if (asteroidCount == 0)
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
        // Choose which edge of the screen to spawn on
        Edge edge = (Edge)Random.Range(0, 4);
        Vector2 viewportSpawnPosition = Vector2.zero;

        switch (edge)
        {
            case Edge.Top:
                viewportSpawnPosition = new Vector2(Random.value, 1);
                break;
            case Edge.Bottom:
                viewportSpawnPosition = new Vector2(Random.value, 0);
                break;
            case Edge.Left:
                viewportSpawnPosition = new Vector2(0, Random.value);
                break;
            case Edge.Right:
                viewportSpawnPosition = new Vector2(1, Random.value);
                break;
        }

        // Create Asteroid
        Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
        EnemyScript asteroid = Instantiate(asteroidPrefab, worldSpawnPosition, Quaternion.identity).GetComponent<EnemyScript>();
        asteroid.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        asteroid.size = Random.Range(2, 4);
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
