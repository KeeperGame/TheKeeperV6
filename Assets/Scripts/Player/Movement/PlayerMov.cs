using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    Rigidbody PlayerRb;

    [SerializeField]
    Vector3 move;

    float JumpForce;
    float speed;

    

    Transform groundCheck;
    float groundDistance = 0.5f;
    public LayerMask groundMask;

    Animator anim;
    GameObject CrPos;
    GameObject RealBody;
    GameObject newCamPos;

    CapsuleCollider capCol;
    float ColliderYOriginal;
    float ColliderYCrouched;

    float ColliderCenterOri;
    float ColliderCenterCrouched;

    bool Crouching;

    public GameObject OriginalCamPos;

    // Start is called before the first frame update
    void Start()
    {
        CrPos = GameObject.Find("Crouch"); // We save the reference to it.
        CrPos.SetActive(false); // and we disable it.
        Crouching = false;
        RealBody = GameObject.Find("PlayerAnimated");

        newCamPos = GameObject.Find("NewCamPos");
        OriginalCamPos = GameObject.Find("OriginalCamPos");

        capCol = GetComponent<CapsuleCollider>();

        ColliderYOriginal = capCol.height;
        ColliderCenterOri = capCol.center.y;
        ColliderYCrouched = 1.33f;
        ColliderCenterCrouched = -0.363f;

        anim = GetComponentInChildren<Animator>();
        groundCheck = GameObject.Find("GroundCheck").GetComponent<Transform>();
        JumpForce = 400.0f;
        speed = 4.0f;
        PlayerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        Movement();
        Crouch();
    }

    void Movement()
    {

        move = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        transform.Translate(move * speed * Time.deltaTime);
       
        if(move != Vector3.zero)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
        
        /*
        if(Input.GetButtonDown("Jump") && GroundCheck())
        {
            PlayerRb.AddForce(Vector3.up * JumpForce);
        }
        */

    }

    void Crouch() // Funny note, this function has been empty for a week.
    {
        if((Input.GetKey(KeyCode.LeftControl) || 
            Input.GetKey(KeyCode.C) ) && !Crouching)
        {
            
            CrPos.SetActive(true);
            Camera.main.transform.position = newCamPos.transform.position;
            RealBody.SetActive(false);
            Crouching = true;
            capCol.height = ColliderYCrouched;
            capCol.center = Vector3.up * ColliderCenterCrouched;
        }

        if(( Input.GetKeyUp(KeyCode.C) || 
            Input.GetKeyUp(KeyCode.LeftControl)) && Crouching)
        {
            CrPos.SetActive(false);
            RealBody.SetActive(true);
            capCol.height = ColliderYOriginal;
            capCol.center = Vector3.up * ColliderCenterOri;
            Crouching = false;
            Camera.main.transform.position = OriginalCamPos.transform.position;
        }


    }

   bool GroundCheck()
   {
      bool onGround;
      onGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
      return onGround;
   }

}
