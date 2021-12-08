using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    //switching and targeting enemies
    EnemyBase currentTarget;
    [SerializeField] Queue<EnemyBase> targets = new Queue<EnemyBase>();
    //shooting and cooling down
    Timer shootCoolDown;
    [SerializeField] float coolDownTime;
    [SerializeField] GameObject bulletPrefab;
    GameObject bullet;

    //showing range
    SpriteRenderer rangePic;
    bool isHeld = false;

    //shot bullet y-offest
    [SerializeField] Vector3 offset;
    #region Properties
    public EnemyBase CurrentTarget
    {
        get { return currentTarget; }
    }

    public bool IsHeld
    {
        get { return isHeld; }
        set { isHeld = value; }
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rangePic = GetComponent<SpriteRenderer>();
        shootCoolDown = gameObject.AddComponent<Timer>();
        shootCoolDown.Duration = coolDownTime;
        bullet = Instantiate(bulletPrefab);
        bullet.SetActive(false);
        //EventManager.current.onEnemyKill += EnemyKilledInRangedHandle;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (IsHeld)
        {
            rangePic.enabled = true;
        }
        else
        {
            rangePic.enabled = false;
            if(!shootCoolDown.Running)
                Attack();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !isHeld)
        {
            targets.Enqueue(collision.GetComponent<EnemyBase>());
            currentTarget = collision.GetComponent<EnemyBase>();
            currentTarget.IsInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            currentTarget = null;
        }
    }


    void Attack()
    {
        //Debug.Log(currentTarget.name);
        //if target is out of range or dead change target
        if (currentTarget == null && targets.Count > 0)
        {
            currentTarget = targets.Dequeue();
        }
        if (currentTarget != null && currentTarget.gameObject.activeSelf && currentTarget.IsInRange)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        if (!bullet.activeSelf)
        {
            bullet.transform.position = this.transform.position + offset;
            bullet.SetActive(true);
            bullet.GetComponent<BulletBase>().Initialize(this);
            shootCoolDown.Run();
        }
    }

}
