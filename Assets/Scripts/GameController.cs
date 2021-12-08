using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GameController : MonoBehaviour
{

    //calculate the width of the screen to spawn the 3 roads
    private Camera mainCamera;
    private Vector2 screenBounds;
    private float screenHeight;
    //testing tower object to spawn
    [SerializeField] GameObject tower;
    // Start is called before the first frame update
    void Start()
    {
        //calculating world cordinates screen height
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        screenHeight = screenBounds.y;
        float thirdofScreenSize = screenHeight / 3;
        float newPos = 0;
        for (int i = 0; i< 3; i++)
        {
            newPos+= thirdofScreenSize;
            GameObject toto=GameObject.Instantiate(tower);
            toto.transform.position= new Vector3(0, newPos, 0); ;
        }
      
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
