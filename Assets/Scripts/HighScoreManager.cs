using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public GameObject player;
    PlayerController pcScript;

    ScoreManagerGUI scoreManagerScript;

    private void Start()
    {
        //I have to set the reference to the player to do this
        //because this script is not attatched to the same object
        //as the player
        pcScript = player.GetComponent<PlayerController>();

        //I can do this because this script is attatched to the
        //same object at the ScoreManagerGUI script
        scoreManagerScript = GetComponent<ScoreManagerGUI>();
    }

    public void writeHighScore()
    {
        //we can write simple things to a standard text file by using PlayerPrefs
        PlayerPrefs.SetInt("Highscore1", pcScript.getPlayerHighScore());
    }

    public int readHighScore()
    {
        int highscorefromfile = PlayerPrefs.GetInt("Highscore1", 0);

        return highscorefromfile;
    }
}
