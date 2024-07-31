using UnityEngine;

public class AsteroidChildren : MonoBehaviour
{
    [SerializeField] private string targetTag = "Bullet";
    [SerializeField] private GameObject[] powerUpPrefabs;
    [SerializeField] private Asteroid asteroid;
    private GameObject scoreEvent;
    private int totalHp;
    private int hp;


    private void Start()
    {
        totalHp = asteroid.GetHp();
        hp = totalHp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            hp--;
            if (hp <= 0)
            {
                gameObject.SetActive(false);
                scoreEvent = GameObject.Find("Score Event");
                scoreEvent.GetComponent<ScoreEvent>().AddScore(25 * totalHp);
                if (powerUpPrefabs.Length != 0 && Random.Range(0, 4) < 2)
                {
                    Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], gameObject.transform.position, Quaternion.identity);
                }
            }
        }
    }
}