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
    [SerializeField] private AudioSource _audioFanfare;
    [SerializeField] private AudioSource _audioLose;

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
        
        Debug.Log("_game over!");
        Level.SetActive(false);
        LoseScreen.SetActive(true);
        _audioLose.Play();
        _snake.ScoreCount = 0;
    }

    public void OnSnakeReachedFinish()
    {
        if (CurrentState != State.Playing) return;

        CurrentState = State.Won;
        _snake.enabled = false;
        _head.enabled = false;
        LevelIndex++;
        Level.SetActive(false);
        WinScreen.SetActive(true);
        _audioFanfare.Play();
        _textPassedLevel.text = ($"Level {LevelIndex.ToString()} passed");
        _textScore.text = ($"Score: {_snake.ScoreCount}");


        PlayerPrefs.SetInt("SaveTailSize", _snake.Segment.Count);
    }

    public int LevelIndex
    {
        get => PlayerPrefs.GetInt("LevelIndex", 0);
        private set
        {
            PlayerPrefs.SetInt("LevelIndex", value);
            //    PlayerPrefs.SetInt(LevelIndexKey, value);
            //    PlayerPrefs.Save();
        }
    }


    private const string LevelIndexKey = "LevelIndex";

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
