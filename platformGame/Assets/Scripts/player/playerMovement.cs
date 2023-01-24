using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    #region variables

    [SerializeField] private Rigidbody2D rig;

    // movementVar
    [Header("Move")]
    public float moveSpeed;
    public float acceleration;
    public float decceleration;
    public float velPower;
    private Vector2 _moveInput;

    // jumpVar
    [Header("Jump")]
    public float jumpPower;
    private bool isJumping;

    // atckVar
    [Header("Atack")]
    private float radius = 0.2f;
    private bool isAttacking;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] private Transform atkPoint;

    // healthVar
    private float health = 10;

    [Header("Aniamtion")]
    [SerializeField] private Animator anim;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // _moveInput.x recebe o comando de movimentação Horizontal
        _moveInput.x = Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Jump();
        Attack();
    }

    void Move()
    {
        // calculate the direction we want to move and the velocity we want
        float targetSpeed = _moveInput.x * moveSpeed;
        // calculate the difference between the current and the velocity we want
        float speedDif = targetSpeed - rig.velocity.x;
        // change the accelaration rate depending on the situation
        // Mathf.Abs -- return the value what we want, this case, target speed
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        // Mathf.Pow -- first number high the second number (elevado)
        // Mathf.Sign -- return the "sign" of the number, if it's positive or negative
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        rig.AddForce(movement * Vector2.right);
        if(_moveInput.x > 0 && !isJumping && !isAttacking){
            transform.eulerAngles = new Vector3(0, 0 , 0);
            anim.SetInteger("Transition", 1);
        }
        else if(_moveInput.x < 0 && !isJumping && !isAttacking){
            transform.eulerAngles = new Vector3(0, 180, 0);
            anim.SetInteger("Transition", 1);
        }
        else if(_moveInput.x == 0 && !isJumping && !isAttacking)
            anim.SetInteger("Transition", 0);
    }

    void Jump()
    {   
        if(Input.GetButtonDown("Jump") && !isJumping){
            anim.SetInteger("Transition", 2);
            rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    void Attack()
    {
        if(Input.GetButtonDown("Fire1")){
            isAttacking = true;
            anim.SetInteger("Transition", 3);
            Collider2D hit = Physics2D.OverlapCircle(atkPoint.position, radius, enemyLayer);

            if(hit != null)
                hit.GetComponent<WormEnemy>().OnHit();

            StartCoroutine(OnAttack());
        }
    }

    IEnumerator OnAttack()
    {
        yield return new WaitForSeconds(0.286f);
        isAttacking = false;
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.layer == 6)
            isJumping = false;
    }

    void Damage()
    {
        anim.SetTrigger("Hit");
        //Damage per enemy
        health--;

        if(health <= 0)
            // GameOver Screen
            anim.SetTrigger("Death");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {              
        if(collision.gameObject.layer == 8)
            Damage();
    }
}