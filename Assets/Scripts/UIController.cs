using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

// control the UI of the first scene
public class UIController : MonoBehaviour
{
    public Text timerTxt;
    public float playtime;
    private float initialPlaytime;

    public Text scoreTxt;
    private int scoreCount = 0;

    public Text barrelsTxt;
    private int barrelsCount = 0;

    public Text showScoreTxt;

    private GetScore getScore;



    private void Awake()
    {
        getScore = FindObjectOfType<GetScore>();
        initialPlaytime = playtime;
    }

    private void Update()
    {
        // Update the playtime 
        playtime -= Time.deltaTime;
        timerTxt.text = string.Format("{0}:{1:00}", Mathf.FloorToInt(playtime / 60), Mathf.FloorToInt(playtime % 60));

        // When the time is up we change scene 
        if (playtime < 0)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }

    // Update the number of barrels delivered 
    public void AddBarrels()
    {
        barrelsCount += 1;
        barrelsTxt.text = barrelsCount.ToString();
    }

    // Update the score 
    public void AddScore(int newScore)
    {
        scoreCount += newScore;
        scoreTxt.text = scoreCount.ToString();
    }

    // Print the score
    public void ShowScore(int score)
    {
        showScoreTxt.gameObject.SetActive(true);
        showScoreTxt.text = "+ " + score.ToString();
        Invoke("HideText", 1f);
    }

    private void HideText()
    {
        showScoreTxt.gameObject.SetActive(false);
    }

    // When changing scenes, the variables are saved 
    private void OnDestroy()
    {
        getScore.playtime = initialPlaytime - playtime;
        getScore.scoreCount = scoreCount;
        getScore.barrelsCount = barrelsCount;
    }

   
}
