using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    GameObject Player;
    GameObject AnimatedPlayer;

    bool OpeningDoor;
    bool tmp;

    Animator anim;

    [SerializeField]
    float DoorAngleClosed;

    [SerializeField]
    float DoorAngleOpen;

    [SerializeField]
    Camera p_Cam;

    public bool isLocked;


    private void Awake()
    {
        // Door stuff (default values)
        isLocked = true;
        DoorAngleOpen = -90.0f;
        DoorAngleClosed = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
       
        OpeningDoor = false;
        tmp = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        AnimatedPlayer = GameObject.FindGameObjectWithTag("PlayerAnimated");
        anim = Player.GetComponentInChildren<Animator>();
       
        p_Cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        // Animation Removed.

        //if (OpeningDoor)
        //    MovePlayerToDoor();

        if (!isLocked)
        {
            JustOpen();
        }

    }

    public void Open()
    {
        if (!isLocked)
        {
            ChoosingAngle();
            OpeningDoor = !OpeningDoor;
        }
    }

    void ChoosingAngle()
    {
        Vector3 doorToPlayer = Player.transform.position - transform.position;
        float dotProduct = Vector3.Dot(transform.forward, doorToPlayer);

        //Debug.Log("dot product: " + dotProduct);

        if (dotProduct > 0) // in front of the door
        {
            DoorAngleOpen = -90.0f;
        }
        else // behind the door.
        {
            DoorAngleOpen = 90.0f;
        }
    }

    void DoorTriggerOpen()
    {        
        Quaternion DoorRot = Quaternion.Euler(0.0f, DoorAngleOpen, 0.0f);

        transform.localRotation = Quaternion.Lerp(transform.localRotation, DoorRot, 2.0f * Time.deltaTime);

    }

    void DoorTriggerClose()
    {
        Quaternion DoorClosed = Quaternion.Euler(0.0f, DoorAngleClosed, 0.0f);

        transform.localRotation = Quaternion.Lerp(transform.localRotation, DoorClosed, 2.0f * Time.deltaTime);

    }

    void JustOpen()
    {
        if(OpeningDoor)
        {
            DoorTriggerOpen();
        }
        else
        {
            DoorTriggerClose();
        }
    }

    void MovePlayerToDoor()
    {
        float Distance = Vector3.Distance(Player.transform.position, transform.position);

        if (tmp != true)
        {
            if (Distance > 1.286f) // If the Player is far away from the Door (for the animation to connect)
            {
                // Move him a little bit forward.
                Player.transform.position = Vector3.MoveTowards(Player.transform.position, this.transform.position, Time.deltaTime * 5.0f);
            }
            else
            {
                tmp = true;
            }
        }
        else
        {
            // Once the Door and animation can connect:
            Player.GetComponent<PlayerMov>().enabled = false;
            p_Cam.GetComponent<MouseLook>().enabled = false;
            anim.SetBool("OpeningDoor", true);
            Camera.main.transform.parent = AnimatedPlayer.transform;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.4f && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Opening_Door"))
        {
            DoorTriggerOpen();
        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Opening_Door"))
        {
            DoorTriggerClose();
        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("Opening_Door"))
        {
            OpeningDoor = false;
            tmp = false;
            Player.transform.position = AnimatedPlayer.transform.position;
            Player.GetComponent<PlayerMov>().enabled = true;
            p_Cam.GetComponent<MouseLook>().enabled = true;
            anim.SetBool("OpeningDoor", false);
            Camera.main.transform.parent = Player.transform;
            Camera.main.transform.position = Player.GetComponent<PlayerMov>().OriginalCamPos.transform.position;
            // Added this lane above because the position was wrong when the parents of the Camera Main were changed.
        }


    }

}
