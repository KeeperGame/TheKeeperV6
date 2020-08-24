using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandfatherClock : MonoBehaviour
{
    public GameObject Player;
    public AudioSource AS;
    public float Volume;
    public AudioClip ClockTick;

    public GameObject Door;
    public bool OpenTheDoor;
    // Start is called before the first frame update
    void Start()
    {
        AS = Player.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AS.isPlaying == false)
        {
            AS.volume = Volume;
            AS.PlayOneShot(ClockTick);
        }

        if (OpenTheDoor == true)
        {
            Door.GetComponent<Animator>().SetBool("DoorOpen", true);
        }
        else if (OpenTheDoor == false)
        {
            Door.GetComponent<Animator>().SetBool("DoorOpen", false);
        }
        
   
 
    }
}
