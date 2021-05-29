using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform viewPoint;
    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private float verticalRotStore;
    [SerializeField] private Vector2 mouserInput;

    [SerializeField] private float moveSpeed = 5f, runspeed = 8f;
    private float activeMoveSpeed;
    
    private Vector3 moveDir, movement;

    [SerializeField] private CharacterController _controller;

    private Camera _cam;

    [SerializeField] float jumpforce = 12f;

    [SerializeField] private Transform groundCheckPoint;
    private bool isGrounded;
    [SerializeField] private LayerMask groundMaskLayer;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _cam = Camera.main;

    }

    void Update()
    {
        float mousex = Input.GetAxisRaw("Mouse X");
        float mousey = Input.GetAxisRaw("Mouse Y");
        
        mouserInput = new Vector2(mousex, mousey) * mouseSensitivity;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouserInput.x, transform.rotation.eulerAngles.z);

        verticalRotStore -= mouserInput.y;
        verticalRotStore = Mathf.Clamp(verticalRotStore, -60f, 60f); 

        viewPoint.rotation = Quaternion.Euler(verticalRotStore,  viewPoint.rotation.eulerAngles.y, viewPoint.rotation.eulerAngles.z);


        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            activeMoveSpeed = runspeed;
        }
        else
        {
            activeMoveSpeed = moveSpeed;
        }

        float yval = movement.y;
        movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized * activeMoveSpeed;
        
        if (!_controller.isGrounded)
        {
            movement.y = yval; 
        }

        isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, .25f, groundMaskLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            movement.y = jumpforce;
        }
        
        movement.y += Physics.gravity.y * Time.deltaTime;
        
         _controller.Move( movement * Time.deltaTime);

         if (Input.GetKeyDown(KeyCode.Escape))
         {
             Cursor.lockState = CursorLockMode.None;
         } else if (Cursor.lockState == CursorLockMode.None)
         {
             if (Input.GetMouseButtonDown(0))
             {
                 Cursor.lockState = CursorLockMode.Locked;
             }
         }
    }

    private void LateUpdate()
    {
        _cam.transform.position = viewPoint.position;
        _cam.transform.rotation = viewPoint.rotation;
    }
}
