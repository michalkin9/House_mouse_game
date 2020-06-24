using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    bool isCollied;
    bool isPickedObject;
    bool isFinishedLevel;
    public float Health = 0.5f;
    public Image HealthFillBar;
    public GameObject CompletePanel , GameOverPanel;
    public GameObject TrapedPanel;

    private GameObject SelectedObject;
    public Material NormalMaterial, PickedMaterial;
    // Start is called before the first frame update
    void Start()
    {
        HealthFillBar.fillAmount = Health;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isCollied)
        {
            SelectedObject.transform.SetParent(gameObject.transform);
            SelectedObject.GetComponent<MeshRenderer>().material = PickedMaterial;
            isCollied = false;
            isPickedObject = true;
        }
        else if (Input.GetKeyDown(KeyCode.Return)  && isPickedObject)
        {
            SelectedObject.transform.SetParent(null);
            SelectedObject.GetComponent<MeshRenderer>().material = NormalMaterial;
            isPickedObject = false;
        }
    }
    private void FixedUpdate()  //FixedUpdate is drawn before physics calculations.
    {
        if (Health > 0 && !isFinishedLevel)
        {
            Health = Health - 0.0001f;
            CheckHealth();
        }

        //HelperArrow.transform.LookAt(LevelObjects[PlayerPrefs.GetInt("SelectedLevel") - 1].transform.GetChild(1).transform);
    }
    void CheckHealth()
    {
        if (Health > 0)
        {
            HealthFillBar.fillAmount = Health;
        }
        else
        {
            GameOver();
            isFinishedLevel = true;
        }
    }
    private void GameOver()
    {
        GameOverPanel.SetActive(true);
        Invoke("Restart", 2f);
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "floor")
        {
            Debug.Log("collied with floor");

        }

        if (collision.gameObject.CompareTag("food")) //collision.gameObject.name == "foodToEat"
        {
            Health = Health + .35f; //add the energy
            if (Health >= 1.0f && SceneManager.GetActiveScene().buildIndex != 3)
            {
                CompletePanel.SetActive(true);
                Invoke("NextLevel", 2f);
            }
            Debug.Log("collied with 2 food");
            Destroy(collision.gameObject); //dextroies the food on collision
        }
        if (collision.gameObject.CompareTag("trap"))
        {
            Destroy(collision.gameObject); 
            TrapedPanel.SetActive(true);
            Invoke("Restart", 2f);
        }
        if (collision.gameObject.CompareTag("moveable") && !isPickedObject)
        {
            isCollied = true;
            SelectedObject = collision.gameObject;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("moveable") && !isPickedObject)
        {
            isCollied = false;
            SelectedObject = null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("OnTable"))
        {
            CompletePanel.SetActive(true);
            Invoke("Home", 2f);
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
