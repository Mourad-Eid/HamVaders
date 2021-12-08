using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySpawner : MonoBehaviour
{
    //spawning
    [SerializeField] GameObject energyPrefab;
    // Start is called before the first frame update
    void Start()
    {        
        EventManager.current.onEnergyCollection += EnergyCollectedSpawnerHander;

    }

    void EnergyCollectedSpawnerHander (bool isPlayer)
    {
        energyPrefab.SetActive(false);
        Vector3 spawnPlace = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.3f, 0.7f), Random.Range(0.3f, 0.7f), 1));
        energyPrefab.transform.position = spawnPlace;
        if (!energyPrefab.activeSelf)
        {            
            energyPrefab.SetActive(true);
        }
    }
    private void OnDestroy()
    {
        EventManager.current.onEnergyCollection -= EnergyCollectedSpawnerHander;
    }
}
