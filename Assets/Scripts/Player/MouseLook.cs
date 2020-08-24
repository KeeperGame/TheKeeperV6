using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Transform PlayerTrans;
    public float m_sensitivity = 3.0f;

    float xRotation;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        xRotation = 0.0f;        
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * m_sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * m_sensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        PlayerTrans.Rotate(Vector3.up * mouseX);        

    }

 
}
