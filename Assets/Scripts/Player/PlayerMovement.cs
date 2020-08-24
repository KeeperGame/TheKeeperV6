using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController PlayerController;
    public float speed;

    [SerializeField]
    Vector3 Vel = Vector3.zero;

    [SerializeField]
    Vector3 move;

    public float gravity;
    float jumpH = 1.5f;
    float airAcc = 4.0f;

    Transform groundCheck;
    float groundDistance = 0.5f;
    public LayerMask groundMask;

    bool onGround;

    private void Awake()
    {
        groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
        gravity = -9.81f;
        speed = 2.0f;
    }


    // Start is called before the first frame update
    void Start()
    {
        PlayerController = GetComponent<CharacterController>();       
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();       
        Movement();

    }

    void Gravity()
    {
        // CharacterController isGrounded variable is now being used.

        onGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (onGround && Vel.y < 0)
        {
           Vel.y = -2.0f;
        }        
    }

    void Movement()
    {

        if (onGround)
        {
            Debug.Log("Touching the ground.");
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            move = transform.right * x + transform.forward * z;

          
            PlayerController.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump"))
            {
                Vel.y = Mathf.Sqrt(jumpH * -2f * gravity);
                
                PlayerController.Move(Vel * Time.deltaTime);
            }
            else
            {
                Vel = Vector3.zero;
            }
        }

        if (!onGround)
        {
            Debug.Log("IN THE AIR...");
            Vel.y += gravity * Time.deltaTime;
            PlayerController.Move((Vel + move) * Time.deltaTime); // Time ^ 2
        }      

    }


}
