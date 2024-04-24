using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

// Display the score achieved by the player 
public class DisplayScore : MonoBehaviour
{
    public Text barrelsTxt;
    public Text scoreTxt;
    public Text playtimeTxt;
    public Text totalScoreTxt;

    private GetScore getScore;

    private void Awake()
    {
        getScore = FindObjectOfType<GetScore>();
    }

    private void Start()
    {
        barrelsTxt.text = getScore.barrelsCount.ToString();
        scoreTxt.text = getScore.scoreCount.ToString();

        float playtime = getScore.playtime;
        playtimeTxt.text = string.Format("{0}:{1:00}", Mathf.FloorToInt(playtime / 60), Mathf.FloorToInt(playtime % 60));

        int totalScore = (int)(getScore.barrelsCount + getScore.scoreCount + playtime);
        totalScoreTxt.text = totalScore.ToString();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
