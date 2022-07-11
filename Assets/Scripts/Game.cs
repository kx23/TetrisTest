using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Dictionary<int, int> pointsForRows = new Dictionary<int, int>() 
    { 
        { 1, 100 }, 
        { 2, 300 }, 
        { 3, 700 }, 
        { 4, 1500 } 
    }; //словарь {кол-во уничтоженных рядов} - {кол-во полученных очков} 
    private Dictionary<int,float> levelSpeeds = new Dictionary<int, float>()
    {
        { 1, 1 },
        { 2, 0.8f },
        { 3, 0.725f },
        { 4, 0.650f },
        { 5, 0.600f },
        { 6, 0.500f },
        { 7, 0.4f },
        { 8, 0.2f },
        { 9, 0.1f },
        { 10, 0.05f }

    }; // словарь {уровень игры} - {скорость падения}*/

    public event Action<int> EventUpdateScore;
    public event Action<int> EventChangeLevel;
    public event Action EventGameOver;


    public Playfield Playfield;
    public Spawner Spawner;
    public GameOverView GameOverView;
    private Tetramino CurrTetramino;


    private float lastFall = 0; //время последнего тика
    private int score=0;
    private int linesCount =0;
    private const int linesToNextLevel = 20; //кол-во уничтоженных рядов для перехода на следующий уровень
    private int level; // текущий уровень
    private float speed;
    private float maxSpeed=0.05f; // максимальная скорость спуска фигур (при нажатии кнопки вниз)



    private void OnEnable()
    {
        Playfield.EventDeleteRows += AddPointsForRows;
        Playfield.EventDeleteRows += AddLinesCount;
        
    }
    private void OnDisable()
    {
        Playfield.EventDeleteRows -= AddPointsForRows;
        Playfield.EventDeleteRows -= AddLinesCount;
    }
    private void Start()
    {
        level = 1;
        speed = levelSpeeds[level];
        EventGameOver += ShowGameOverView;
        maxSpeed = levelSpeeds[levelSpeeds.Count];
        CurrTetramino = Spawner.SpawnNewTetramino();
        
        
    }

    private void Update()
    {
       
        if (Time.time - lastFall >= speed)
        {
            CurrTetramino.transform.position += new Vector3(0, -1, 0);

            if (CheckIsValidPosition())
            {
                UpdateGrid();
            }
            else
            {
                CurrTetramino.transform.position += new Vector3(0, 1, 0);

                Playfield.DeleteFullRows();
                CurrTetramino = Spawner.SpawnNewTetramino();
                if (!CheckIsValidPosition())
                {
                    CallEventGameOver();
                    this.enabled = false;
                    
                }
                UpdateGrid();
                lastFall = 0;
            }

            lastFall = Time.time;
        }
    }
    // проверка позиции фигуры
    private bool CheckIsValidPosition()
    {
        foreach (Transform child in CurrTetramino.transform)
        {
            Vector2 v = Playfield.RoundVec2(child.position);

            if (!Playfield.CheckInsideGrid(v)) // внитри ли рамки фигура
                return false;

            if (Playfield.Grid[(int)v.x, (int)v.y] != null &&
                Playfield.Grid[(int)v.x, (int)v.y].parent != CurrTetramino.transform) // не заезжает ли фигура на другую
                return false;
        }
        return true;

    }

    // Обновление информации в сетке
    private void UpdateGrid()
    {
        for (int y = 0; y < Playfield.GridHeigth; ++y)
            for (int x = 0; x < Playfield.GridWigth; ++x)
                if (Playfield.Grid[x, y] != null)
                    if (Playfield.Grid[x, y].parent == CurrTetramino.transform)
                        Playfield.Grid[x, y] = null;


        foreach (Transform child in CurrTetramino.transform)
        {
            Vector2 v = Playfield.RoundVec2(child.position);
            Playfield.Grid[(int)v.x, (int)v.y] = child;
        }

    }

    private void AddPointsForRows(int rowsCount)
    {
        score = score + pointsForRows[rowsCount];
        CallEventUpdateScore(score);
    }
    private void AddLinesCount(int value)
    {
        linesCount += value;
        if (linesToNextLevel*level <= linesCount &&level < levelSpeeds.Count)
        {
            level++;
            CallEventChangeLevel(level);
            speed = levelSpeeds[level];
        }

    }
    
    public void CallEventUpdateScore(int value)
    {
        EventUpdateScore?.Invoke(value);
    }
    public void CallEventChangeLevel(int value)
    {
        EventChangeLevel?.Invoke(value);
    }
    public void CallEventGameOver()
    {
        EventGameOver?.Invoke();
    }

    public void TryMoveTetraminoLeft()
    {
        CurrTetramino.transform.position += new Vector3(-1, 0, 0);
        if (CheckIsValidPosition())
        {
            UpdateGrid();
        }
        else
        {
            CurrTetramino.transform.position += new Vector3(1, 0, 0);
        }
            
    }

    public void TryMoveTetraminoRight()
    {
        CurrTetramino.transform.position += new Vector3(1, 0, 0);
        if (CheckIsValidPosition())
        {
            UpdateGrid();
        }
        else
        {
            CurrTetramino.transform.position += new Vector3(-1, 0, 0);
        }
            
    }

    public void TryRotateTetramino()
    {
        CurrTetramino.transform.Rotate(0, 0, -90);

        if (CheckIsValidPosition())
            UpdateGrid();
        else
            CurrTetramino.transform.Rotate(0, 0, 90);
    }

    public void SpeedUp() => speed = maxSpeed;
    public void BackToNormalSpeed() => speed = levelSpeeds[level];

    public void RestartScene() => SceneManager.LoadScene("GameScene");
    public void GoToMenu()=> SceneManager.LoadScene("MenuScene");

    private void ShowGameOverView()
    {
        GameOverView.Score.text = $"SCORE: {score.ToString()}";
        GameOverView.gameObject.SetActive(true);
    }
}
