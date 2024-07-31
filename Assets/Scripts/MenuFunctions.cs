using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    public void LoadScene(int index)
    {
        if(index > 0)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(index);
    }

    public void Quit()
    {
        Application.Quit();
    }
}