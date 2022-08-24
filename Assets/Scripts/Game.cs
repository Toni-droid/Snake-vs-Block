using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Snake _snake;
    [SerializeField] private SnakeHead _head;
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject LoseScreen;
    [SerializeField] private GameObject Level;
    [SerializeField] private Text _textPassedLevel;
    [SerializeField] private Text _textScore;
    [SerializeField] private AudioSource _audioSource;
    
    public enum State
    {
        Playing,
        Won,
        Lose,
    }

    public State CurrentState { get; private set; }

    public void OnSnakeDied()
    {
        if (CurrentState != State.Playing) return;
        CurrentState = State.Lose;
        _audioSource.Stop();
        //_head.enabled = false;
        Debug.Log("Game over!");
        Level.SetActive(false);
        LoseScreen.SetActive(true);
        _snake.ScoreCount = 0;

    }

    public void OnSnakeReachedFinish()
    {
        if (CurrentState != State.Playing) return;

        CurrentState = State.Won;
        _snake.enabled = false;
        _head.enabled = false;
        Debug.Log("WON!");
        LevelIndex++;
        Level.SetActive(false);
        WinScreen.SetActive(true);
        _textPassedLevel.text = ($"Level {LevelIndex.ToString()} passed");
        _textScore.text = ($"Score: {_snake.ScoreCount}");
        _snake.TailSize();
    }

    public int LevelIndex
    {
        get => PlayerPrefs.GetInt("LevelIndex", 0);
        private set
        {
            PlayerPrefs.SetInt(LevelIndexKey, value);
            PlayerPrefs.Save();
        }
    }


    private const string LevelIndexKey = "LevelIndex";

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
