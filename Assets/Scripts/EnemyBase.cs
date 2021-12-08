using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //movement
    [SerializeField] float speed;
    Rigidbody2D rb;

    //health
    int health;
    [SerializeField] int maxHealth;
    [SerializeField] EnemyHealthBar healthBar;
    bool isInRange;
    int damage = 1;
    [SerializeField] int regularDamage = 1;
    [SerializeField] int buffDamage = 2;
    
    //decrease speed card
    int energy;
    float subtractedSpeed;
    float decreaseSpeedTime=5;
    //properties
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public bool IsInRange
    {
        get {return isInRange;}
        set { isInRange = value; }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isInRange = false;
        //card event
        energy = 0;
        damage = regularDamage;
     
        EventManager.current.onDecreaseSpeed += DecreaseEnemySpeed;
    }

    private void OnEnable()
    {
        health = maxHealth;      
    }
    private void OnDisable()
    {
        isInRange = false;
        
    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, -speed);
        healthBar.setHealthBar(health, maxHealth);
    }


    public void TakeDamage(int damage)
    {
        health-=damage;
        if (health <= 0)
        {
            EventManager.current.EnemyKilledEvent();
            //isInRange = false;
            gameObject.SetActive(false);
        }
    }

    
    //decrease speed handler
    void DecreaseEnemySpeed()
    {        
        subtractedSpeed = speed / 2;
        speed -= subtractedSpeed;
        Invoke("ReturnSpeedToNormal", decreaseSpeedTime);
    }
    void ReturnSpeedToNormal()
    {
        speed += subtractedSpeed;
    }
    private void OnDestroy()
    {
        EventManager.current.onDecreaseSpeed -= DecreaseEnemySpeed;
    }
}
