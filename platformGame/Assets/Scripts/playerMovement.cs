using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    #region variables

    private Rigidbody2D rig;

    // movementVar
    [Header("Move")]
    public float moveSpeed;
    public float acceleration;
    public float decceleration;
    public float velPower;
    private Vector2 _moveInput;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _moveInput.x = Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
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
    }
}