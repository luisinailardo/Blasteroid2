using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private int asteroidNumber = 20;
    [SerializeField] private float asteroidKillHeight = -14.0f;
    [SerializeField] private Asteroid[] asteroidPrefab;
    [SerializeField] private Vector2 spawnIntervalRange = new Vector2(0.25f, 1.0f);
    [SerializeField] private Vector3 spawnAreaHalfExtent = new Vector3(10.0f, 0.5f, 0f);
    [SerializeField] private List<Asteroid> asteroidPool;
    [SerializeField] private List<Asteroid> activeAsteroids;
    private Queue<Asteroid> inactiveAsteroids;
    private float difficultyLevel = 0;


    private float nextSpawnTime;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, spawnAreaHalfExtent * 2);
    }

    private void Start()
    {
        StartCoroutine(IncreaseDifficulty());
        inactiveAsteroids = new Queue<Asteroid>();

        for (int i = 0; i < asteroidNumber; i++)
        {
            Asteroid asteroid = Instantiate(asteroidPrefab[Random.Range(0, asteroidPrefab.Length)]);
            asteroid.gameObject.SetActive(false);
            asteroidPool.Add(asteroid);
            inactiveAsteroids.Enqueue(asteroid);
        }
    }

    private IEnumerator IncreaseDifficulty()
    {
        difficultyLevel += 0.5f;
        yield return new WaitForSeconds(30);
        StartCoroutine(IncreaseDifficulty());
    }

    private Vector3 GetRandomSpawnPoint()
    {
        return new Vector3(Random.Range(-spawnAreaHalfExtent.x, spawnAreaHalfExtent.x),
                           Random.Range(-spawnAreaHalfExtent.y, spawnAreaHalfExtent.y),
                           Random.Range(-spawnAreaHalfExtent.z, spawnAreaHalfExtent.z));
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            if (inactiveAsteroids.TryDequeue(out Asteroid asteroid))
            {
                asteroid.gameObject.SetActive(true);
                asteroid.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                asteroid.SetDifficultyLevel(difficultyLevel);
                activeAsteroids.Add(asteroid);
                asteroid.transform.position = GetRandomSpawnPoint() + transform.position;
                asteroid.Init();
            }
            nextSpawnTime = Time.time + Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
        }

        List<Asteroid> toRemove = new List<Asteroid>();

        foreach (Asteroid asteroid in activeAsteroids)
        {
            if (asteroid.transform.position.y <= asteroidKillHeight || asteroid.gameObject.activeSelf == false)
            {
                asteroid.gameObject.SetActive(false);
                inactiveAsteroids.Enqueue(asteroid);
                toRemove.Add(asteroid);
            }
        }

        foreach (Asteroid removeObject in toRemove)
        {
            activeAsteroids.Remove(removeObject);
        }
    }

    public List<Asteroid> GetActiveAsteroids()
    {
        return activeAsteroids;
    }

    public Queue<Asteroid> GetInactiveAsteroids()
    {
        return inactiveAsteroids;
    }
}
