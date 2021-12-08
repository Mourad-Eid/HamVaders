
using UnityEngine;
using TMPro;

public class ScoreTextModify : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void OnEnable()
    {

        scoreText = GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    public void UpdateScore(int score)
    {
       // Debug.Log("score function responded");
        scoreText.text = "Score: " + score.ToString();
    }
    
}
