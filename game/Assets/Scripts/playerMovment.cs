﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerMovment : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public float jumpHeight = 3f;
    float gravity = -9.81f;
    Vector3 velocity;
    bool isGrounded;
    bool isCollied;
    bool isPickedObject;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float Health = 1;
    public Image FillBar;
    public GameObject CompletePanel;
    public GameObject TrapedPanel;

    private GameObject SelectedObject;
    public Material NormalMaterial, PickedMaterial;
    // Update is called once per frame
    void Update()
    {

       isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            //  Debug.Log("isGRounded true");
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
        if (Input.GetKeyDown(KeyCode.Return) && isCollied)
        {
            SelectedObject.transform.SetParent(gameObject.transform);
            SelectedObject.GetComponent<MeshRenderer>().material = PickedMaterial;
            isCollied = false;
            isPickedObject = true;
        }else if (Input.GetKeyDown(KeyCode.Return) && SelectedObject != null && isPickedObject)
        {
            Debug.Log("........");
            SelectedObject.transform.SetParent(null);
            SelectedObject.GetComponent<MeshRenderer>().material = NormalMaterial;
            isPickedObject = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "floor")
         {
          Debug.Log("collied with floor");
        isGrounded = true;

        }

        if (collision.gameObject.CompareTag("food")) //collision.gameObject.name == "foodToEat"
        {
            FillBar.fillAmount = FillBar.fillAmount + .35f;
            if(FillBar.fillAmount == 1)
            {
                CompletePanel.SetActive(true);
                Invoke("NextLevel", 2f);
            }
            Debug.Log("collied with 2 food");
            Destroy(collision.gameObject);
            Health++;
        }
        if (collision.gameObject.CompareTag("trap"))
        {
            Destroy(collision.gameObject);
            TrapedPanel.SetActive(true);
            Invoke("Restart", 2f);
        }
        if (collision.gameObject.CompareTag("moveable"))
        {
            isCollied = true;
            SelectedObject = collision.gameObject;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("moveable"))
        {
            isCollied = false;
            SelectedObject = null;
        }
    }
    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }


}

