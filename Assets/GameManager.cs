using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject button;
    public GameObject lost;
    public Ghost ghost;
    public Board board;
    public Piece piece;

    private int highScore;
    public int score;
    public int level;
    public float dropSpeed;
    private int tetrominosTotal;
    private int linesCleared;


    private float startTime;
    private int methodCallCounter;


    public int[] tetrominos;
    public TextMeshProUGUI[] tetrominoText;
    public TextMeshProUGUI[] stats;




    private void Start()
    {
        startTime = Time.realtimeSinceStartup;

        button.SetActive(true);
        lost.SetActive(false);
        board.enabled = false;
        ghost.enabled = false;
        piece.enabled = false;

        dropSpeed = piece.stepDelay;
        highScore = PlayerPrefs.GetInt("hiscore");
        stats[0].text = highScore.ToString();

    }
    private void Update()
    {
        if (highScore < score)
        {
            highScore = score;
            PlayerPrefs.SetInt("hiscore", highScore);

        }


        if (Time.realtimeSinceStartup - startTime >= 0.1f)
        {
            startTime = Time.realtimeSinceStartup;


            if (methodCallCounter == 4)
            {
                Tetris();
            }
            else if (methodCallCounter == 3)
            {
                Triple();
            }
            else if (methodCallCounter == 2)
            {
                Double();
            }
            else if (methodCallCounter == 1)
            {
                Single();
            }

            methodCallCounter = 0;
        }


        int lastLevel = level;
        linesCleared = board.linesCleared;
        level = Mathf.RoundToInt(board.linesCleared / 10+1);
        
        if(level > lastLevel)
        {
            piece.stepDelay -= 0.10f;
            lastLevel = level;
            
        }
        dropSpeed = piece.stepDelay;
        UpdateTetrominoStats();
        

    }




    public void StartGame()
    {
        board.enabled = true;
        ghost.enabled = true;
        piece.enabled = true;
        lost.SetActive(false);
        button.SetActive(false);
        board.tilemap.ClearAllTiles();
        ClearTetrominoStats();
        board.linesCleared = 0;
        tetrominosTotal = 0;
        piece.stepDelay = 0.9f;
        score = 0;
    }

    public void GameLost()
    {
        lost.SetActive(true);
        button.SetActive(true);
        board.enabled = false;
        ghost.enabled = false;
        piece.enabled = false;

        

    }

    

    public void NewPieceSpawned(string tetromino)
    {
        switch (tetromino)
        {
            case "I":
                tetrominos[0] += 1;
                break;

            case "J":
                tetrominos[1] += 1;
                break;

            case "L":
                tetrominos[2] += 1;
                break;

            case "O":
                tetrominos[3] += 1;
                break;

            case "S":
                tetrominos[4] += 1;
                break;

            case "T":
                tetrominos[5] += 1;
                break;

            case "Z":
                tetrominos[6] += 1;
                break;

                
        }

        tetrominosTotal += 1;
    }

    public void UpdateTetrominoStats()
    {
        for (int i = 0; i < tetrominos.Length; i++)
        {
            tetrominoText[i].text = tetrominos[i].ToString();
        }
        stats[0].text = highScore.ToString();
        stats[1].text = score.ToString();
        stats[2].text = level.ToString();
        stats[3].text = linesCleared.ToString();
        stats[4].text = dropSpeed.ToString("0.00");
        stats[5].text = tetrominosTotal.ToString();
    }

    public void ClearTetrominoStats()
    {
        for (int i = 0; i < tetrominos.Length; i++)
        {
            tetrominos[i] = 0;
        }
    }


    public void Single()
    {
        score += 100 * level;
    }

    public void Double()
    {
        score += 300 * level;
    }
    public void Triple()
    {
        score += 500 * level;
    }

    public void Tetris()
    {
        score += 800 * level;
    }

    public void MethodToTrack()
    {
        methodCallCounter++;
    }

    public void ATITB()
    {
        Application.OpenURL("https://atitb.com");
    }

}
