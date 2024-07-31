using UnityEngine;

public class AsteroidChildren : MonoBehaviour
{
    [SerializeField] private string targetTag = "Bullet";
    [SerializeField] private GameObject[] powerUpPrefabs;
    [SerializeField] private Asteroid asteroid;
    [SerializeField] private SpawnableEvent killEvent;
    private int hp;


    private void Start()
    {
        hp = asteroid.GetHp();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            hp--;
            if (hp <= 0)
            {
                gameObject.SetActive(false);
                killEvent.Invoke();
                if (powerUpPrefabs.Length != 0 && Random.Range(0, 4) < 2)
                {
                    Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], gameObject.transform.position, Quaternion.identity);
                }
            }
        }
    }
}