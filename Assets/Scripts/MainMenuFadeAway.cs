using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuFadeAway : MonoBehaviour
{
    [SerializeField] private Image titleFrame;
    [SerializeField] private Image buttonFrame;
    private float tmpColor;

    private void Start()
    {
        StartFadeIn();
    }

    public void StartFadeAway()
    {
        Time.timeScale = 1;
        tmpColor = 1;
        StartCoroutine(FadeAway());
    }

    public void StartFadeIn()
    {
        Time.timeScale = 1;
        tmpColor = 0;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeAway()
    {
        tmpColor -= 0.05f; 
        titleFrame.color = new Color(titleFrame.color.r, titleFrame.color.g, titleFrame.color.b, tmpColor);
        buttonFrame.color = new Color(buttonFrame.color.r, buttonFrame.color.g, buttonFrame.color.b, tmpColor);
        yield return new WaitForSeconds(0.03f);
        if(tmpColor >= 0)
        {
            StartCoroutine(FadeAway());
        }
    }

    IEnumerator FadeIn()
    {
        tmpColor += 0.05f;
        titleFrame.color = new Color(titleFrame.color.r, titleFrame.color.g, titleFrame.color.b, tmpColor);
        buttonFrame.color = new Color(buttonFrame.color.r, buttonFrame.color.g, buttonFrame.color.b, tmpColor);
        yield return new WaitForSeconds(0.03f);
        if(tmpColor <= 1)
        {
            StartCoroutine(FadeIn());
        }
    }
}
