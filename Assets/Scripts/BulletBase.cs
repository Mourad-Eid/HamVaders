using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    EnemyBase target;
    TowerBase tower;

    //increase damage event
    int energy;
    int damage;
    float increaseDamageBuffTimer = 5;
    // Start is called before the first frame update
    void Start()
    {
        damage = 1;
        energy = 0;
        EventManager.current.onIncreaseBulletDamage += IncreaseDamage;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
    }

    public void Initialize(TowerBase tower)
    {
        this.tower = tower;
        this.target = tower.CurrentTarget;

    }    
    public void MoveToTarget()
    {
        if (target != null && target.gameObject.activeSelf)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, bulletSpeed*Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyBase>().TakeDamage(damage);
            gameObject.SetActive(false);          
        }
    }

    //increase damage Handlers;
    void IncreaseDamage()
    {
        damage ++;
        Invoke("ReturnDamageToNormal", increaseDamageBuffTimer);
    }

    void ReturnDamageToNormal()
    {
        damage--;
    }

    private void OnDestroy()
    {
        EventManager.current.onIncreaseBulletDamage += IncreaseDamage;
    }
}
