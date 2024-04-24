using UnityEngine;
using UnityEngine.UI;

// Control the behaviour of the player
public class PlayerController : MonoBehaviour
{
    public string state = "onGround";

    public float speed = 1f;
    private readonly float maxSpeed = 15f;
    public Slider speedSlider;

    // control the direction of the nave
    private float steering;
    public float steeringAmount = 120f;
    public float pitchAmount = 20f;
    public float rollAmount = 60f;

    private Rigidbody rb;

    private bool haveBarrel = false;
    public Transform barrelSpawnPos;
    public GameObject barrelPrefab;

    private RandomPlatform randomP;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        randomP = FindObjectOfType<RandomPlatform>();
    }

    private void Update()
    {
        // Control acceleration 
        if (Input.GetButton("Fire1"))
            speedSlider.value += 0.005f;
        else if (Input.GetButton("Fire2"))
            speedSlider.value -= 0.005f;
        // Control the state of the nave
        if (transform.position.y > 1)
            state = "onAir";
        else if (transform.position.y <= 0.2f)
            state = "onGround";
    }
        
    private void FixedUpdate()
    {
        Movement();
        SteeringControl();
        GravityControl();

        if (Input.GetKey(KeyCode.Space) && haveBarrel == true)
        {
            DropBarrels();
        }
    }

    #region nave's physical behavior
    // Control the movement of the nave 
    private void Movement()
    {
        speed += speedSlider.value;
        speed = Mathf.Clamp(speed, 0, speedSlider.value * maxSpeed);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    // Control the direction of the nave 
    private void SteeringControl()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        steering += horizontalInput * steeringAmount * Time.deltaTime;

        float pitch = 0; 
        float roll = 0;

        // On the ground we will only be able to rotate on the vertical axis of the ship. 
        if (speed > 6 && state == "onGround")
        {
            pitch = Mathf.Lerp(0, pitchAmount, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);
        }

        // In the air will be able to rotate freely 
        if (state == "onAir")
        {
            roll = Mathf.Lerp(0, rollAmount, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);
            pitch = Mathf.Lerp(0, pitchAmount, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);
        }

        // apply the rotation
        transform.localRotation = Quaternion.Euler(pitch, steering, roll);
    }

    // If the speed is too slow the nave will fall. 
    private void GravityControl()
    {
        if (speed <= 5f && state == "onAir")
        {
            rb.useGravity = true;
        }
        else
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.isKinematic = false;
        }
    }

    // When we are near the barrels we will automatically pick them up. 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            other.gameObject.SetActive(false);
            haveBarrel = true;
            randomP.GetPlatform();
        }
    }
    #endregion

    // creates a barrel and drops it at the nave's speed 
    private void DropBarrels()
    {
        haveBarrel = false;

        GameObject barrel = Instantiate(barrelPrefab, barrelSpawnPos.position, Quaternion.identity);
        barrel.GetComponent<Rigidbody>().AddForce(transform.forward * speed * Time.deltaTime, ForceMode.Force);

        randomP.halo.SetActive(false);
    }
}


