using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{

    public Text Score;
    public Text Level;
    public Game Game;
    void Start()
    {
        Score.text = "SCORE: 0";
        Level.text = "LEVEL: 1";
    }
    private void OnEnable()
    {
        Game.EventUpdateScore += UpdateScoreText;
        Game.EventChangeLevel += ChangeLevel;
    }
    private void OnDisable()
    {
        Game.EventUpdateScore -= UpdateScoreText;
        Game.EventChangeLevel -= ChangeLevel;
    }
    void Update()
    {
        
    }

    public void UpdateScoreText(int value)
    {
        Score.text = $"SCORE: {value.ToString()}" ;
    }
    public void ChangeLevel(int value)
    {
        Level.text=$"LEVEL: {value.ToString()}";
    }
}
