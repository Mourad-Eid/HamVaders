using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    private void Start()
    {
        Vector3 spawnPlace = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), Random.Range(0.3f, 0.7f), 1));
        gameObject.transform.position = spawnPlace;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EventManager.current.EnergyTriggerEnter(true);
        }
        else if (collision.tag == "Enemy")
        {
            EventManager.current.EnergyTriggerEnter(false);
        }
    }
}
