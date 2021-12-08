using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //fuck singleton but yea xD
    public static EventManager current;

    private void Awake()
    {
        current = this;
    }

    //energy collecting event
    public event Action<bool> onEnergyCollection;
    public void EnergyTriggerEnter(bool isPlayer)
    {
        if (onEnergyCollection != null)
        {
            //Debug.Log("event is invoked");
            onEnergyCollection(isPlayer);
        }
    }

    //killing a monster event
    public event Action onEnemyKill;
    public void EnemyKilledEvent()
    {
        onEnemyKill?.Invoke();
    }

    //card Played event
    public event Action<int> onCardPlayed;
    public void CardPlayed(int cost)
    {
        onCardPlayed?.Invoke(cost);
    }

    //incease diff event
    public event Action onWaveComplete;
    public void WaveComplete()
    {
        onWaveComplete?.Invoke();
    }


    //cards events
    //decrease speed event
    public event Action onDecreaseSpeed;
    public void DecreaseSpeed()
    {
        onDecreaseSpeed?.Invoke();   
    }

    //increase bullet Damage
    public event Action onIncreaseBulletDamage;
    public void IncreaseBulletDamge()
    {
        onIncreaseBulletDamage?.Invoke();
    }



    //end game event
    public event Action onGameEnded;
    public void GameEnded()
    {
        onGameEnded?.Invoke();
    }

}
