using UnityEngine;
using TMPro;

public class HighScoreTextModify : MonoBehaviour
{
    TextMeshProUGUI highScoreText;
    // Start is called before the first frame update
    void OnEnable()
    {
        highScoreText = GetComponent<TextMeshProUGUI>();
        //EventManager.current.onEndGameMenuAppears += UpdateHighScore;
        highScoreText.text = "High Score: "+ PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void UpdateHighScore(int score)
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text ="High Score: "+ score.ToString();
        }
    }
    
}
