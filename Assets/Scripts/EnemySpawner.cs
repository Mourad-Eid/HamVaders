using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //pooling
    [SerializeField] int pooledAmount = 10;
    [SerializeField] GameObject enemyPrefab1;
    List<GameObject> enemies;
    //spawning positions test
    float min = 0.2f;
    float max = 0.8f;

    //spawning Time
    Timer timeBetSpawns;
    [SerializeField] float timeBetSpawnsDuration;

    //clamping position using wall
    [SerializeField] GameObject wall;
    [SerializeField] GameObject cityEnterance;

    //cards handling
    int energy;

    //menus prefabas and texts
    //exit menu
    [SerializeField] GameObject exitGameMenu;
    [SerializeField] GameObject scoreText;
    [SerializeField] GameObject scoreInExitMenu;
    [SerializeField] GameObject highScoreInExitMenu;
    // Start is called before the first frame update
    void Start()
    {
        //spawning walls
        Vector3 spawnLeftWall= Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 1));
        GameObject leftWall = Instantiate(wall, spawnLeftWall, Quaternion.identity);
        Vector3 spawnRighttWall = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 1));
        GameObject rightWall = Instantiate(wall, spawnRighttWall, Quaternion.identity);
        rightWall.transform.rotation= Quaternion.Euler(0, 180, 0);

        //spawning city entrance
        Vector3 spawnCityEntrance = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.15f, 1));
        GameObject cityGate = Instantiate(cityEnterance, spawnCityEntrance, Quaternion.identity);
        //enemy spawning
        timeBetSpawns = gameObject.AddComponent<Timer>();
        timeBetSpawns.Duration = timeBetSpawnsDuration;
        //booling enimies
        enemies = new List<GameObject>(10);
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(enemyPrefab1);
            obj.SetActive(false);
            enemies.Add(obj);
        }

        //managing cards
        energy = 0;
        EventManager.current.onEnergyCollection += addEnergy;
        EventManager.current.onCardPlayed += HandleOtherCardPlayed;

        //diffculty
        EventManager.current.onWaveComplete += HandleIncreaseDifficulty;

        //end game management 
        EventManager.current.onGameEnded += HandleOnGameEnded;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeBetSpawns.Running)
        {
            spawnEnemies();
        }
        
    }

    void spawnEnemies()
    {
        //sekeleton for spawning enemies
        for (int i = 0; i < enemies.Count; i++)
            if (!enemies[i].activeInHierarchy)
            {
                Vector3 spawnPlace = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(min, max), 1, 1));
                enemies[i].transform.position = spawnPlace;
                enemies[i].SetActive(true);
                timeBetSpawns.Run();
                break;
            }
    }
    void addEnergy(bool isPlayer)
    {
        if (isPlayer)
            energy++;
       // Debug.Log(energy);
    }

    

    void HandleOtherCardPlayed(int cost)
    {
        if(energy>=cost)
            energy -= cost;       
    }

    //event handlers
    //decrease speed handler(button event)
    public void HandleDecreseSpeedCardClick(int cost)
    {

        if (energy >= cost)
        {
            energy -= cost;
            //Debug.Log("energy after card is " + energy);
            EventManager.current.CardPlayed(cost);
            EventManager.current.DecreaseSpeed();
        }
    }

    //increase bullet speed
    public void HandleIncreaseBulletDamageClick(int cost)
    {
        if (energy >= cost)
        {
            energy -= cost;
            EventManager.current.CardPlayed(cost);
            EventManager.current.IncreaseBulletDamge();
        }
    }
    //icrease difficulty handler( listner to wave text)
    void HandleIncreaseDifficulty()
    {
        for (int i = 0; i < pooledAmount; i++)
        {
            enemies[i].GetComponent<EnemyBase>().Speed += 0.3f;
            timeBetSpawnsDuration -= 0.5f;
        }
    }

    //end game
    void HandleOnGameEnded()
    {
        exitGameMenu.SetActive(true);
        int score = scoreText.GetComponent<KillsTextModify>().Kills;
        scoreInExitMenu.GetComponent<ScoreTextModify>()?.UpdateScore(score);
        highScoreInExitMenu.GetComponent<HighScoreTextModify>()?.UpdateHighScore(score);
        Time.timeScale = 0;      
    }

   
    //return enemy speed to normal
    float returnSpeedToNormal(float currentSpeed, float decreasedSpeed)
    {
        return currentSpeed + decreasedSpeed;       
    }
    private void OnDestroy()
    {
        EventManager.current.onEnergyCollection -= addEnergy;
        EventManager.current.onCardPlayed -= HandleOtherCardPlayed;
        EventManager.current.onWaveComplete -= HandleIncreaseDifficulty;
        EventManager.current.onGameEnded -= HandleOnGameEnded;
    }
}
