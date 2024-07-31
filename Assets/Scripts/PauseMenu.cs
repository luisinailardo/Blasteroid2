using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameplayGUI;
    [SerializeField] private bool isDead;

    private void Start()
    {
        PlayerInput input = FindObjectOfType<PlayerInput>();
        input.actions["Menu"].started += OnMenuPerformed;
        pauseMenu.SetActive(false);
    }

    private void OnMenuPerformed(InputAction.CallbackContext context)
    {
        ChangeStatePauseMenu();
    }

    public void ChangeStatePauseMenu()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            gameplayGUI.SetActive(true);
            Time.timeScale = 1;
        }
        else if (!isDead)
        {
            pauseMenu.SetActive(true);
            gameplayGUI.SetActive(false);
            Time.timeScale = 0;
        }
    }

    public void SetIsDead()
    {
        isDead = true;
    }
}