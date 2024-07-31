using UnityEngine;

public class PowerUpEvent : MonoBehaviour
{
    [SerializeField] private float destroyDelay;
    [SerializeField] private GameObject destructionVFX;
    private bool pickedUp;

    private void Start()
    {
        Destroy(gameObject, destroyDelay);
    }

    public void HpUpgrade(int i = 1)
    {
        GameObject.Find("StarshipContainer").GetComponent<StarshipController>().SetHp(i);
    }

    public void SpeedUpgrade(float i = 0.25f)
    {
        GameObject.Find("StarshipContainer").GetComponent<StarshipController>().SetSpeed(i);
    }

    public void ShieldUpgrade(int i = 1)
    {
        GameObject.Find("StarshipContainer").GetComponent<StarshipController>().SetShield(i);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickedUp = true;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (!pickedUp)
        {
            Destroy(Instantiate(destructionVFX, transform.position, Quaternion.identity), 2);
        }
    }
}