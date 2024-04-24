using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Controls the behaviour of the turret 
public class TurretController : MonoBehaviour
{
    private string state = "Passive";

    public Transform turret;
    public LineRenderer laser;
    public Text countdowntxt;
       
    private PlayerController playerController;
    private float distance;

    private float countdown = 3f;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (state == "Passive")
        {
            countdowntxt.gameObject.SetActive(false);
            countdown = 3f;
            countdowntxt.text = countdown.ToString();
            
            laser.gameObject.SetActive(false);
            turret.transform.Rotate(Vector3.up * .5f);
        } 
        else if (state == "Alert")
        {
            // A countdown begins to leave the city 
            countdowntxt.gameObject.SetActive(true);
            countdown -= Time.deltaTime;
            countdowntxt.text = countdown.ToString("0");

            laser.gameObject.SetActive(true);
            laser.SetPosition(1, new Vector3(0, 0, distance - .5f));        
        }

        // if we spend too much time near the city, the game will be over. 
        if (countdown < 0)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // If we enter the city space the turret will go on alert. 
        if (other.CompareTag("Player"))
        {
            turret.transform.LookAt(playerController.transform);
            state = "Alert";
            distance = Vector3.Distance(transform.position, other.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If we leave the city space the turret will enter a passive state.
        if (other.CompareTag("Player"))
        {
            state = "Passive";
        }
    }
}
