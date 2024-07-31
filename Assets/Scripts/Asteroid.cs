using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Vector2 fallSpeedRange;
    [SerializeField] private Vector2 sizeRange;
    [SerializeField] private float tumble;
    [SerializeField] private int hp = 1;
    [SerializeField] private float difficultyLevel;
    private float fallSpeed;

    public void Init()
    {
        float size = Random.Range(sizeRange.x, sizeRange.y);
        fallSpeed = -Random.Range(fallSpeedRange.x, fallSpeedRange.y);
        transform.localScale = new Vector3(size, size, size);
        hp = (int)((size * 2) + difficultyLevel);
        if (hp >= 10)
        {
            hp = 10;
        }
    }

    private void Start()
    {
        GetComponentInChildren<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }

    private void Update()
    {
        transform.Translate(0, fallSpeed * Time.deltaTime, 0);
    }

    public void SetDifficultyLevel(float i)
    {
        difficultyLevel += i;
    }

    public int GetHp()
    {
        return hp;
    }
}