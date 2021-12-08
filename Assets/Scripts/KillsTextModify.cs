
using UnityEngine;
using TMPro;

public class KillsTextModify : MonoBehaviour
{
    TextMeshProUGUI killsText;
    int kills;

    public int Kills 
    {
        get { return kills; }
    }
    // Start is called before the first frame update
    void Start()
    {
        kills = 0;
        killsText = GetComponent<TextMeshProUGUI>();
        killsText.text = "Kills: " + kills.ToString();
        EventManager.current.onEnemyKill += EnemyKilled;
    }

  void EnemyKilled()
    {
        kills++;
        killsText.text = "Kills: " + kills.ToString();
    }

    private void OnDestroy()
    {
        EventManager.current.onEnemyKill -= EnemyKilled;
    }
}
