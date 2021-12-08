using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayScenePlacement : MonoBehaviour
{
    [SerializeField] GameObject road;
    // Start is called before the first frame update
    void Start()
    {
        //instantiating road
        float left = 0.1f;
        float right = 0.9f;
        Vector3 leftPlace = Camera.main.ViewportToWorldPoint(new Vector3(left, 0.5f, 1));
        GameObject instantiatedRoad=GameObject.Instantiate(road);
        instantiatedRoad.transform.position = leftPlace;
        Vector3 rightPlace = Camera.main.ViewportToWorldPoint(new Vector3(right, 0.5f, 1));
        GameObject instantiatedRoad2 = GameObject.Instantiate(road);
        instantiatedRoad2.transform.position = rightPlace;
    }


}
