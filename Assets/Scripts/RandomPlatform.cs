using System.Collections.Generic;
using UnityEngine;

// Activate a random delivery location 
public class RandomPlatform : MonoBehaviour
{
    public List<Transform> platforms;
    public GameObject halo;

    // Update the position of the halo that will show where the barrel will be delivered to
    public void GetPlatform()
    {
        int i = Random.Range(0, platforms.Count);
        halo.transform.position = platforms[i].position;
        halo.SetActive(true);
    }
}
