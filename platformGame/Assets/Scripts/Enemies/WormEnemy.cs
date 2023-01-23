using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : MonoBehaviour
{
    private float walkSpeed = .2f;

    [SerializeField] Rigidbody2D rig;

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

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.layer == 7)
            walkSpeed = -walkSpeed;
            transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
