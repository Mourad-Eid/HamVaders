using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    [SerializeField] int speed;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down *speed* Time.deltaTime);
    }
}
