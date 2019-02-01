using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Quaternion lookLeft;
    private Quaternion lookRight;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        Cursor.visible = false;

        Time.timeScale = 1;

        lookRight = transform.rotation;
        lookLeft = lookRight * Quaternion.Euler(0, 180, 0);

        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        var localSpeed = speed;
        if (controller.isGrounded)
        {
            localSpeed = localSpeed / 2;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }


        var horizontalAxis = Input.GetAxis("Horizontal");
        moveDirection = new Vector3(-horizontalAxis, 0, 0);

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = lookLeft;
            moveDirection = transform.TransformDirection(-moveDirection);
            moveDirection *= localSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = lookRight;
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= localSpeed;
        }


        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}