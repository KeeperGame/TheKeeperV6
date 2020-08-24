using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CreateRay();
        Debug.DrawRay(transform.position, transform.forward, Color.red);
    }

    void CreateRay()
    {
        Ray myRay = new Ray(transform.position, transform.forward);

        RaycastHit hit;

        if(Physics.Raycast(myRay, out hit, 2.0f))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.collider.CompareTag("Door"))
                {
                    Debug.Log("We hit the door");
                    hit.collider.GetComponent<OpenDoor>().Open();

                }
            }
        }


    }

}
