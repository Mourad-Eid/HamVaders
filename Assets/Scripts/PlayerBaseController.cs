using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerBaseController : MonoBehaviour
{
    //touch input
    Touch touch;
    //movement
    Rigidbody2D rb;
    [SerializeField] int speed=2;
    Vector3 touchPos, whereToGo;
    float previousDistanceToTouchPos, currentDistanceToTouchPos;
    bool isMoving;
    bool facingRight=true;
    //holding and putting tower
    [SerializeField] Transform holdingPos;
    [SerializeField] Transform puttingPos;
    bool isHoldingTower=false;
    bool isReadyToPutTower = false;
    bool towerIsInAir = false;
    bool isReadyToLiftTower = false;
    GameObject theTower;
    TowerBase towerScript;

    //animation
    Animator anim;

    //incease speed card effect
    int energy;
    Timer incSpeedTimer;
    [SerializeField] int buffedSpeed=4;
    [SerializeField] int regularSpeed = 2;
    [SerializeField] int incSpeedDuration=4;

    //detecting wall
    [SerializeField] LayerMask whatIsWall;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //speed card effect
        energy = 0;
        speed = regularSpeed;
        incSpeedTimer = gameObject.AddComponent<Timer>();
        incSpeedTimer.Duration = incSpeedDuration;
        EventManager.current.onEnergyCollection += addEnergy;
        EventManager.current.onCardPlayed += HandleOtherCardPlayed;
    }

    // Update is called once per frame
    void Update()
    {
        if (incSpeedTimer.Running)
        {
            speed = buffedSpeed;
        }
        else
        {
            speed = regularSpeed;
        }
       
        //motion
         if (isMoving)
         {
             currentDistanceToTouchPos = (touchPos - transform.position).magnitude;
         }
         if (Input.touchCount > 0)
         {
             touch = Input.GetTouch(0);
            if (!IsPointerOverUIObject())
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                    {
                        return;
                    }

                    previousDistanceToTouchPos = 0;
                    currentDistanceToTouchPos = 0;
                    isMoving = true;
                    anim.SetBool("isRunning", true);
                    if (isHoldingTower)
                    {
                        isReadyToPutTower = true;
                        if (isMoving)
                        {
                            anim.SetBool("isRunningWithTower", true);
                        }
                    }
                    touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    touchPos.z = 0f;
                    whereToGo = (touchPos - transform.position).normalized;
                    if ((transform.position.x > touchPos.x && facingRight) || (transform.position.x < touchPos.x && !facingRight))
                    {
                        Flip();
                    }
                    rb.velocity = new Vector2(whereToGo.x * speed, whereToGo.y * speed);
                }
            }
         }
         if (currentDistanceToTouchPos > previousDistanceToTouchPos)
         {
             isMoving = false;
             anim.SetBool("isRunning", false);
            anim.SetBool("isRunningWithTower", false);
            rb.velocity = Vector2.zero;
             if (isReadyToPutTower && theTower !=null)
             {
                theTower.transform.parent = null;
                isHoldingTower = false;
                anim.SetBool("isHoldingTower", false);
                if (towerScript != null)
                {
                    towerScript.IsHeld = false;
                }
                if (towerIsInAir && isReadyToPutTower)
                {
                    theTower.transform.position = Vector3.MoveTowards(theTower.transform.position, puttingPos.position, 3 * Time.deltaTime);
                    if (theTower.transform.position == puttingPos.position)
                    {
                        isReadyToPutTower = false;
                        towerIsInAir = false;
                    }
                }
            }
         }
         if (isMoving)
         {
             previousDistanceToTouchPos = (touchPos - transform.position).magnitude;
         }

        if (!towerIsInAir && theTower != null && isReadyToLiftTower)
        {
            theTower.transform.position = Vector3.MoveTowards(theTower.transform.position, holdingPos.position, 2 * Time.deltaTime);
            if (theTower.transform.position == holdingPos.position)
            {
                towerIsInAir = true;
                isReadyToLiftTower = false;
            }
        }       
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tower")
        {
            if (towerScript == null)
            {
                towerScript = collision.GetComponentInChildren<TowerBase>();
            }
            isHoldingTower = true;
            towerScript.IsHeld = true;
            anim.SetBool("isHoldingTower", true);
            rb.velocity = Vector2.zero;

            theTower = collision.gameObject;
            theTower.transform.parent = this.transform;
            //theTower.transform.position = puttingPos.position;
            towerIsInAir = false;
            isReadyToLiftTower = true;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }


    void addEnergy(bool isPlayer)
    {
        if(isPlayer)
            energy++;
    }

    public void HandleCardClick(int cost)
    {
        if (energy >= cost)
        {
            energy -= cost;
            EventManager.current.CardPlayed(cost);
            incSpeedTimer.Run();
        }

    }

    void HandleOtherCardPlayed(int cost)
    {
        if(energy>=cost)
            energy -= cost;
    }
    private void OnDestroy()
    {
        EventManager.current.onEnergyCollection -= addEnergy;
        EventManager.current.onCardPlayed -= HandleOtherCardPlayed;
    }
}
