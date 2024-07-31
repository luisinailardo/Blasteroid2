using UnityEngine;
using TMPro;

public class AmmunitionEvent : MonoBehaviour
{
    [SerializeField] private int ammunition;
    [SerializeField] private TMP_Text ammunitionText;

    public void ModifyAmmunition(int ammunitionUsed)
    {
        string tmp = "";
        ammunition += ammunitionUsed;
        for(int i = 0; i < ammunition; i++)
        {
            tmp += "| ";
        }
        ammunitionText.SetText(tmp);
    }
}
