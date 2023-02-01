using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblins : MonoBehaviour
{
    private float healt;
    private float goblinSpeed = 2.4f;
    private float maxVision = 8.3f;
    private float playerDis = .7f;

    private bool isRight = true;
    private bool isFront;

    private float timeAtk = .9f;
    private bool isGoblinAttacking;
    private float currentTimeAtk = .7f;
    private float atkDamage = 1.3f;

    private Vector2 direction;

    [SerializeField] Rigidbody2D rig;
    [SerializeField] Transform rayPos;
    [SerializeField] Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        if(isRight){
            transform.eulerAngles = new Vector2(0, 0);
            direction =  Vector2.right;
            anim.SetInteger("Transition", 0);
        }
        else{
            transform.eulerAngles = new Vector2(0, 180);
            direction = Vector2.left;
            anim.SetInteger("Transition", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        GetPlayer();
        Move();
    }

    void Move()
    {
        if(isFront){
            if(isRight){
                transform.eulerAngles = new Vector2(0, 0);
                direction =  Vector2.right;
                rig.velocity = new Vector2(goblinSpeed, rig.velocity.y);
                anim.SetInteger("Transition", 2);
            }
            else{
                transform.eulerAngles = new Vector2(0, 180);
                direction = Vector2.left;
                rig.velocity = new Vector2(-goblinSpeed, rig.velocity.y);
                anim.SetInteger("Transition", 2);
            }
        }
    }

    void GetPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayPos.position, direction, maxVision);
        if(hit.collider != null){
            if(hit.transform.CompareTag("Player")){
                isFront = true;
                isGoblinAttacking = false;
                // Return the difference btw the goblin and player
                float distance = Vector2.Distance(transform.position, hit.transform.position);

                if(distance <= playerDis){
                    isFront = false;
                    rig.velocity = Vector2.zero;
                    currentTimeAtk += Time.deltaTime;
                    anim.SetInteger("Transition", 0);

                    if(currentTimeAtk >= timeAtk){
                        hit.transform.GetComponent<playerMovement>().Damage();
                        playerMovement.instance.health = playerMovement.instance.health - atkDamage;
                        currentTimeAtk = 0;

                        anim.SetInteger("Transition", 1);
                        isGoblinAttacking = true;
                        StartCoroutine(OnGoblinAttack());
                    }
                }
            }
        }
    }

    IEnumerator OnGoblinAttack()
    {
        yield return new WaitForSeconds(0.572f);
        isGoblinAttacking = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(rayPos.position, direction * maxVision);
    }
}
