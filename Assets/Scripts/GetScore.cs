using UnityEngine;

// saves variables that we will use in the scene where the scores will be displayed.
public class GetScore : MonoBehaviour
{
    public float playtime;
    public int scoreCount = 0;
    public int barrelsCount = 0;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
