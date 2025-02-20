using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 7f;
    public float moveSpeed = 0f;
    public float rotateSpeed = 60f;
    public float gravity = -9.81f; // 중력 값
    float damping = 5f;
    private Vector3 velocity;
    public bool isMoving = false;
    PlayerInput input;
    public Vector3 dir;
    bool isJump = false;
    bool isStop = false;
    //private Rigidbody rb;
    private CharacterController cc;
    private Animator animator;

    private int hashPosX = Animator.StringToHash("PosX");
    private int hashPosY = Animator.StringToHash("PosY");
    private int hashSpeed = Animator.StringToHash("Speed");
    private int hashStopJump = Animator.StringToHash("StopJump");
    private int hashMoveJump = Animator.StringToHash("MoveJump");
    private int hashAttack = Animator.StringToHash("Attack");

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (isStop==false)
        {
            isMoving = (input.posX == 0 && input.posY == 0) ? false : true;
            moveSpeed = isMoving ? (input.isRun ? runSpeed : walkSpeed) : 0f;
            dir = new Vector3(input.posX, 0f, input.posY);
            Rotate();
            Move();
            animator.SetFloat(hashPosX, input.posX);
            animator.SetFloat(hashPosY, input.posY);
            animator.SetFloat(hashSpeed, moveSpeed);
        }
    }

    private void Rotate()
    {
        //Quaternion targetRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y * input.rotate, 0);
        transform.rotation *= Quaternion.Euler(0, input.rotate * rotateSpeed * Time.deltaTime, 0);
    }
    private void Move()
    {
        Vector3 moveDirection = (input.posX * transform.right +input.posY * transform.forward).normalized;
        //rb.MovePosition(rb.position + moveDistance);
        if (cc.isGrounded)
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
        cc.Move(moveDirection * Time.deltaTime * moveSpeed + velocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (cc.isGrounded == false || isJump == true) return;
        isJump = true;
        if (!isMoving)
        {
            animator.SetTrigger(hashStopJump);
            isStop = true;
        }
        else
        {
            animator.SetTrigger(hashMoveJump);
        }
    }

    public void ResetMoveJump()
    {
        isJump = false;
    }

    public void Attack()
    {
        if (isStop == false)
        {
            isStop = true;
            animator.SetTrigger(hashAttack);
        }
    }

    public void AllowMove() //공격 끝나면 isAttack false로 만드는 메서드
    {
        isStop = false;
        if (isJump == true) isJump = false;
    }
}
