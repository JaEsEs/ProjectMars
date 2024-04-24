using UnityEngine;

// Rotate a object
public class Rotate : MonoBehaviour
{

    private void Update()
    {
        transform.Rotate(Vector3.up * .5f);
    }
}
