using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private string targetTag = "Enemy";
    [SerializeField] private GameObject vfx;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        if (audioSource == null)
        {
            GameObject audioSourceObject = GameObject.Find("SFX Audio Source");
            if (audioSourceObject != null)
            {
                audioSource = audioSourceObject.GetComponent<AudioSource>();
            }
        }
    }

    private void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Destroy(Instantiate(vfx, transform.position, Quaternion.identity), 2);
            audioSource.PlayOneShot(audioClip);
            gameObject.SetActive(false);
        }
    }
}
