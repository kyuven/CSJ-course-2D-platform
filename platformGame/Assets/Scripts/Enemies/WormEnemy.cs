using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : MonoBehaviour
{
    private float walkSpeed = .2f;
    private int health = 5;

    [SerializeField] Rigidbody2D rig;
    [SerializeField] Animator anim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rig.velocity = new Vector2(walkSpeed, rig.velocity.y);
    }

    public void OnHit()
    {
        anim.SetTrigger("Hit");
        health--;

        if(health <= 0){
            anim.SetTrigger("Death");
            Destroy(gameObject, 1f);
            walkSpeed = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.layer == 7)
            walkSpeed = -walkSpeed;
            transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
