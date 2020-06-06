using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovment : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public float jumpHeight = 3f;
    float gravity = -9.81f;
    Vector3 velocity;
    bool isGrounded = true; 


    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    


    // Update is called once per frame
    void Update()
    {

        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); - מחזיר false לבדוק מה הלוז

        if(isGrounded && velocity.y < 0)
        {
            Debug.Log("isGRounded true");
            velocity.y = -2f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded ) // לא עובד עם && isGrounded לבדוק למה
            //להחליט אם עדיף ככה או שהקפיצות יהיו חלקות כמו קודם
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Debug.Log("now you are jumping");
            isGrounded = false;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "floor")
         {
          Debug.Log("collied with floor");
        isGrounded = true;

        }

        if (collision.gameObject.name == "foodToEat")
        {
            Debug.Log(collision.gameObject.name);
            Debug.Log("collied with 2 food");
            Destroy(collision.gameObject);
       }
    }




}

//     float x = Input.GetAxis("Horizontal");
//float z = Input.GetAxis("Vertical");

//velocity.x = x* speed;
//velocity.z = z* speed;

   //     if (!controller.isGrounded)
   //     {
   //         velocity.y -= gravity* Time.deltaTime;

   //     }

   //     velocity = transform.TransformDirection(velocity);
//

   //     Vector3 move = transform.right * x + transform.forward * z;
//controller.Move(move* speed * Time.deltaTime );
