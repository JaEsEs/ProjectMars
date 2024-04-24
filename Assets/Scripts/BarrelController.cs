using UnityEngine;

// Control the behaviour of the barrel 
public class BarrelController : MonoBehaviour
{
    private GameObject halo;
    private GameObject barrels;
    private UIController ui;

    private Rigidbody rb;


    private void Awake()
    {
        halo = GameObject.Find("Halo");
        ui = FindObjectOfType<UIController>();
        barrels = GameObject.Find("Static Barrels").transform.Find("Barrels").gameObject; ;
      

        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (rb.velocity == Vector3.zero)
        {
            //Update the UI
            ui.AddScore(GetScore());
            
            // activate the static barrel so that it can be picked up and delivered 
            barrels.SetActive(true);
            // disable the script when the barrel has been delivered 
            enabled = false;
        }
    }

    // Calculates the score based on the distance the barrel has fallen from its delivery point. 
    private int GetScore()
    {
        // the maximum score is 100 
        float distance = Vector3.Distance(transform.position, halo.transform.position) * 10;
        int score = 100 - (int)distance;

        // Only when the barrel falls close are the scores added up 
        if (score < 60)
        {
            ui.ShowScore(0);
            return 0;
        }

        ui.AddBarrels();
        ui.ShowScore(score);
        return score;
    }

}
