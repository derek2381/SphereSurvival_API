using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI killText;

    void Start()
    {
        UpdateScore(0);
        UpdateKills(0);
    }

    public void UpdateScore(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }

    public void UpdateKills(int newKills)
    {
        killText.text = "Kills: " + newKills;
    }
}
