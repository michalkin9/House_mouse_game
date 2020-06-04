using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovment : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    float gravity = 9.81f;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        velocity.x = x * speed;
        velocity.z = z * speed;

        if (!controller.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;

        }

        velocity = transform.TransformDirection(velocity);


        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime );

    }
}
