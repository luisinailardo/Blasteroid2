using UnityEngine;
using TMPro;

public class HealthEvent : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private TMP_Text healthText;

    public void ModifyHealth(int healthUsed)
    {
        health += healthUsed;
        string tmp = health.ToString();
        healthText.SetText(tmp);
    }
}