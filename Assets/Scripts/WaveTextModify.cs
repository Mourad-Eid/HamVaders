using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaveTextModify : MonoBehaviour
{
    TextMeshProUGUI wavesText;
    int waves;
    int kills;
    // Start is called before the first frame update
    void Start()
    {
        waves = 0;
        kills = 0;
        wavesText = GetComponent<TextMeshProUGUI>();
        wavesText.text = "Waves: " + waves.ToString();
        EventManager.current.onEnemyKill += WaveFinished;
    }

    void WaveFinished()
    {
        kills++;
        if(kills%10 == 0)
        {
            waves++;
            wavesText.text = "Waves: " + waves.ToString();
            EventManager.current.WaveComplete();
        }
    }

    private void OnDestroy()
    {
        EventManager.current.onEnemyKill -= WaveFinished;
    }
}
