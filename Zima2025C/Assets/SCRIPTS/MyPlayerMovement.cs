using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerMovement : MonoBehaviour
{
    public Animator animator;

    public float MovementSpeed = 1.5f; //Wartoœci pocz¹tkowe - s¹ u¿ywane gdy dodajemy komponent do GameObject'u. PóŸniej mo¿emy te wartoœci edytowaæ...
    public float RotationSpeed = 1f; //... w Inspektorze Unity
    public float JumpSpeed = 4f;
    public float Gravity = -9.81f;

    public bool IsGrounded = false;
    public Vector3 MoveDirection = Vector3.zero;

    private CharacterController cc;
    private float yVeclocity = 0;
    private float jumpDelay = 0;

    public float walkSpeed = 1.5f;
    public float runSpeed = 3f;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        jumpDelay += Time.deltaTime;
        IsGrounded = cc.isGrounded;

        transform.Rotate(0, Input.GetAxis("Mouse X") * RotationSpeed, 0);

        var move = Input.GetAxis("Vertical") * transform.forward + 
            Input.GetAxis("Horizontal") * transform.right;

        MovementSpeed = Input.GetButton("Run") ? runSpeed : walkSpeed;

        MoveDirection = MovementSpeed * Vector3.ClampMagnitude(move, 1f);

        if (cc.isGrounded && jumpDelay > 0.02f)
        {
            yVeclocity = Gravity;
            if (Input.GetButtonDown("Jump"))
            {
                yVeclocity = JumpSpeed;
                jumpDelay = 0;
            }
        }

        Vector3 localVelocity = transform.InverseTransformDirection(cc.velocity);
        animator.SetFloat("MoveForward", localVelocity.z);
        animator.SetFloat("MoveRight", localVelocity.x);
        MoveDirection.y = yVeclocity;
    }

    void FixedUpdate()
    {
        yVeclocity += Gravity * Time.deltaTime;
        MoveDirection.y = yVeclocity;
        cc.Move(Time.deltaTime * MoveDirection);
    }
}