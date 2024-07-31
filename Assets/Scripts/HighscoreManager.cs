using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class HighscoreManager : MonoBehaviour
{
    [SerializeField] private ScoreEvent totalScore;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private GameObject inputText;
    [SerializeField] private GameObject deathText;
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("HighscoreEntryContainer");
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscore highscore = JsonUtility.FromJson<Highscore>(jsonString);

        if (highscore != null && highscore.HighscoreEntryList.Count > 0)
        {
            for (int i = 0; i < highscore.HighscoreEntryList.Count; i++)
            {
                for (int j = i + 1; j < highscore.HighscoreEntryList.Count; j++)
                {
                    if (highscore.HighscoreEntryList[j].score > highscore.HighscoreEntryList[i].score)
                    {
                        (highscore.HighscoreEntryList[i], highscore.HighscoreEntryList[j]) = (highscore.HighscoreEntryList[j], highscore.HighscoreEntryList[i]);
                    }
                }
            }

            highscoreEntryTransformList = new List<Transform>();
            if (highscore.HighscoreEntryList.Count < 10)
            {
                foreach (HighscoreEntry highscoreEntry in highscore.HighscoreEntryList)
                {
                    CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    CreateHighscoreEntryTransform(highscore.HighscoreEntryList[i], entryContainer,
                        highscoreEntryTransformList);
                }
            }
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container,
        List<Transform> transformsList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        float templateHeight = 60f;
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformsList.Count);
        entryTransform.gameObject.SetActive(true);

        string rankString = (transformsList.Count + 1) + "°";
        entryTransform.Find("Position Text").GetComponent<TMP_Text>().text = rankString;
        int score = highscoreEntry.score;
        entryTransform.Find("Score Text").GetComponent<TMP_Text>().text = score.ToString();
        string playerName = highscoreEntry.playerName;
        entryTransform.Find("Name Text").GetComponent<TMP_Text>().text = playerName;

        entryTransform.Find("Background").gameObject.SetActive((transformsList.Count + 1) % 2 == 1);
        transformsList.Add(entryTransform);
    }

    private void AddHighscoreEntry(int score, string playerName)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, playerName = playerName };
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscore highscore = jsonString != "" ? JsonUtility.FromJson<Highscore>(jsonString) : new Highscore();

        if (highscore.HighscoreEntryList == null)
        {
            highscore.HighscoreEntryList = new List<HighscoreEntry>();
        }

        highscore.HighscoreEntryList.Add(highscoreEntry);
        string json = JsonUtility.ToJson(highscore);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        inputText.gameObject.SetActive(false);
        deathText.gameObject.SetActive(true);
    }

    public void AddScore()
    {
        if (nameText.text.Length > 1)
        {
            AddHighscoreEntry(totalScore.GetScore(), nameText.text);
        }
    }

    private class Highscore
    {
        public List<HighscoreEntry> HighscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string playerName;
    }
}