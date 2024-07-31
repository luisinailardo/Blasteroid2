using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StarshipController : MonoBehaviour
{
    [SerializeField] private int hp = 5;
    [SerializeField] private int shieldHp = 0;
    [SerializeField] private float speed;
    [SerializeField] private float correctInput;
    [SerializeField] private float loopDistanceHorizontal;
    [SerializeField] private string obstacleTag = "Enemy";
    [SerializeField] private SpawnableEvent deathEvent;
    [SerializeField] private SpawnableEvent speedPowerUp;
    [SerializeField] private SpawnableEvent strengthPowerUp;
    [SerializeField] private SpawnableEvent shieldPowerUp;
    [SerializeField] private SpawnableEvent healthUp;
    [SerializeField] private SpawnableEvent healthDown;
    [SerializeField] private Vector3 target;
    [SerializeField] private GameObject vfx;
    [SerializeField] private GameObject shield;
    [SerializeField] private Material shieldMaterial;
    [SerializeField] private GameObject starshipChildren;
    private Vector3 leftLoopPoint, rightLoopPoint;
    private Vector2 moveInput;


    private void Awake()
    {
        leftLoopPoint = new Vector3(-loopDistanceHorizontal, 0, 0) + transform.position;
        rightLoopPoint = new Vector3(loopDistanceHorizontal, 0, 0) + transform.position;
    }

    private void Start()
    {
        PlayerInput input = FindObjectOfType<PlayerInput>();

        input.actions["Move"].started += OnMovePerformed;
        input.actions["Move"].performed += OnMovePerformed;
        input.actions["Move"].canceled += OnMovePerformed;

        for (int i = 0; i < hp; i++)
        {
            healthUp.Invoke();
        }
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            MoveSpaceship();
        }
    }

    private void MoveSpaceship()
    {
        transform.Translate(speed * moveInput.x * Time.deltaTime, speed * moveInput.y * Time.deltaTime, 0);

        if (transform.position.x > loopDistanceHorizontal)
            transform.position = new Vector3(leftLoopPoint.x, transform.position.y, 0);
        if (transform.position.x < -loopDistanceHorizontal)
            transform.position = new Vector3(rightLoopPoint.x, transform.position.y, 0);
        if (transform.position.y > 15)
            transform.position = new Vector3(transform.position.x, 15, 0);
        if (transform.position.y < 0)
            transform.position = new Vector3(transform.position.x, 0, 0);

        // Mathf.Lerp accetta valori da 0 a 1 per decidere che valore assegnare: 0 = -20, 0.5 = 0, 1 = 20.
        correctInput = (moveInput.x + 1) / 2;
        starshipChildren.transform.eulerAngles = new Vector3(180, Mathf.Lerp(20, -20, correctInput), 0);
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnDeath()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(obstacleTag))
        {
            if (shieldHp > 0)
            {
                GameObject tmpVFX = Instantiate(vfx, other.transform.position, Quaternion.identity);
                Destroy(tmpVFX, 2);
                other.gameObject.SetActive(false);
                shieldHp--;
                if(shieldHp <= 5)
                { 
                    shieldMaterial.SetColor("Color_305775efeafa48798f3eddeead481b6f", new Color(0, 154, 191, 0) * shieldHp / 160);
                }

                if (shieldHp <= 0)
                {
                    shield.SetActive(false);
                }
            } 
            else
            {
                GameObject tmpVFX = Instantiate(vfx, other.transform.position, Quaternion.identity);
                Destroy(tmpVFX, 2);
                hp--;
                healthDown.Invoke();
                other.gameObject.SetActive(false);
                if (hp <= 0)
                {
                    OnDeath();
                    deathEvent.Invoke();
                }
            }
        }
        else if (other.CompareTag("SpeedUp"))
        {
            speedPowerUp.Invoke();
        }
        else if (other.CompareTag("HPUp"))
        {
            strengthPowerUp.Invoke();
            healthUp.Invoke();
        }
        else if (other.CompareTag("ShieldUp"))
        {
            shieldPowerUp.Invoke();
            RenderShield();
        }
    }

    private void RenderShield()
    {
        shield.SetActive(true);
    }

    public void SetHp(int i)
    {
        hp += i;
    }

    public void SetSpeed(float i)
    {
        speed += i;
    }

    public void SetShield(int i)
    {
        shieldHp += i;
        if(shieldHp <= 5)
        {
            shieldMaterial.SetColor("Color_305775efeafa48798f3eddeead481b6f", new Color(0, 154, 191, 0) * shieldHp / 160);
        }
    }
}
