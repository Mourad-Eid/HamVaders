using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnergyTextModify : MonoBehaviour
{
    TextMeshProUGUI energyText;
    int energyValue;
    // Start is called before the first frame update
    void Start()
    {
        energyValue = 0;
        energyText = GetComponent<TextMeshProUGUI>();
        energyText.text = energyValue.ToString();
        EventManager.current.onEnergyCollection += EnergyCollected;
        EventManager.current.onCardPlayed += HandleOtherCardPlayed;
    }

    void EnergyCollected(bool isPlayer)
    {
        if (isPlayer)
        {
            energyValue++;
            energyText.text = energyValue.ToString();
        }
    }

    void HandleOtherCardPlayed(int cost)
    {
        if (energyValue >= cost)
        {
            energyValue -= cost;
            energyText.text = energyValue.ToString();
        }
    }
    private void OnDestroy()
    {
        EventManager.current.onEnergyCollection -= EnergyCollected;
        EventManager.current.onCardPlayed -= HandleOtherCardPlayed;
    }
}
