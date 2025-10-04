using TMPro;
using UnityEngine;

public class ScoreManagerGUI : MonoBehaviour
{
    public TMP_Text guiCurrentScore;
    public TMP_Text guiHighScore;
    public GameObject player;

    private PlayerController pcScript;
    private HighScoreManager hsmScript;

    private void Start()
    {
        pcScript = player.GetComponent<PlayerController>();
        hsmScript = GetComponent<HighScoreManager>();
        pcScript.setPlayerHighScore(hsmScript.readHighScore());
        setGUIHighScore();
    }

    public void setGUICurrentScore()
    {
        guiCurrentScore.text = "Score: " + pcScript.getPlayerScore().ToString();

        if (isHighscore())
        {
            setGUIHighScore();
            hsmScript.writeHighScore();
        }
    }

    public void setGUIHighScore()
    {
        guiHighScore.text = "Highscore: " + pcScript.getPlayerHighScore().ToString();
    }

    public bool isHighscore()
    {
        if (pcScript.getPlayerHighScore() < pcScript.getPlayerScore())
        {
            //change the highscore that is connected to the PlayerController
            pcScript.setPlayerHighScore(pcScript.getPlayerScore());

            //we have a new high score, return true
            return true;
        }
        else
        {
            return false;
        }
    }
}
