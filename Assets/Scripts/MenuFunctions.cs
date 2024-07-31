using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.UI;

public class MenuFunctions : MonoBehaviour
{
    public AudioSource clickSound;
    private bool clicked;
    public void LoadScene(int index)
    {
        if (index > 0)
        {
            Time.timeScale = 1;
        }
        if(!clicked) 
        {
            clicked = true;
            StartCoroutine(DelayLoadScene(index));
        }
    }

    public void Quit()
    {
        StartCoroutine(DelayQuit());
    }

    IEnumerator DelayLoadScene(int index) // Il tasto deve richiamare DelayLoadScene (non più LoadScene) per fare in modo che si senta il sound effect del tasto.
    {
        clickSound.Play();
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(index);
    }


    IEnumerator DelayQuit()
    {
        clickSound.Play();
        yield return new WaitForSeconds(0.25f);
        Application.Quit();
    }

    
}