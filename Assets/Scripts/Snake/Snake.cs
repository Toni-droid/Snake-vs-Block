using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(SnakeGenerator))]
public class Snake : MonoBehaviour
{
    [SerializeField] private SnakeHead _head;
    [SerializeField] float _speed;
    [SerializeField] float _speedR;
    private Vector3 _previosMousePosition;
    [SerializeField] private Transform SnakeHead;
    public List<Segment> _segment;
    private SnakeGenerator _snakeGenerator;
    [SerializeField] private float _segmentsSprigness;
    [SerializeField] private int _tailSize;
    [SerializeField] private Text _textScoreCount;
    [SerializeField] private Text _textBestScore;
    
    public event UnityAction<int> SizeUpdeted;
    public Game Game;
    public int ScoreCount = 0;
    public int _bestScore = 0;
    void Awake()
    {
        _snakeGenerator = GetComponent<SnakeGenerator>();
        _segment = _snakeGenerator.Generate(_tailSize);
        SizeUpdeted?.Invoke(_segment.Count);
        if (PlayerPrefs.HasKey("SaveScore"))
        {
            _bestScore = PlayerPrefs.GetInt("SaveScore");
        }

        if (PlayerPrefs.HasKey("SaveTailSize"))
        {
            _tailSize = PlayerPrefs.GetInt("SaveTailSize");
        }

    }
    private void Start()
    {
        SizeUpdeted?.Invoke(_segment.Count);
        
    }

    private void OnEnable()
    {
        _head.BlockCollided += OnBlockCollided;
        _head.BonusCollected += OnBonusCollected;

    }

    private void OnDisable()
    {
        _head.BlockCollided -= OnBlockCollided;
        _head.BonusCollected -= OnBonusCollected;
    }
    private void FixedUpdate()
    {
        _head.transform.position += _head.transform.forward * _speed * Time.fixedDeltaTime;
        if (Input.GetMouseButton(0))
        {

            Vector3 delta = Input.mousePosition - _previosMousePosition;

            _head.transform.position += _head.transform.right * _speedR * delta.x * Time.fixedDeltaTime;

                      
        }
        _previosMousePosition = Input.mousePosition;
        Vector3 previousPosition = _head.transform.position;
        foreach (Segment segment in _segment)
        {
            Vector3 tempPosition = segment.transform.position;
            segment.transform.position = Vector3.Lerp(segment.transform.position, previousPosition, _segmentsSprigness * Time.fixedDeltaTime);
            previousPosition = tempPosition;
        }

        if (_segment.Count > ScoreCount)
        {
            ScoreCount = _segment.Count;
            BestScore();
            _textScoreCount.text = ScoreCount.ToString();
            _textBestScore.text = ($"BEST SCORE: {_bestScore.ToString()}");
        }

        
        
       
    }

    public void BestScore()
    {
        if (ScoreCount > _bestScore)
        {
            _bestScore = ScoreCount;
            PlayerPrefs.SetInt("SaveScore", _bestScore);
        }
    }

    public void TailSize()
    {
        _tailSize = ScoreCount;
        PlayerPrefs.SetInt("SaveTailSize", _tailSize);

    }

        private void OnBlockCollided()
    {
        Segment deletedSegment = _segment[_segment.Count - 1];
        _segment.Remove(deletedSegment);
        Destroy(deletedSegment.gameObject);
        SizeUpdeted?.Invoke(_segment.Count);
    }

    private void OnBonusCollected(int bonusSize)
    {
        _segment.AddRange(_snakeGenerator.Generate(bonusSize));
        SizeUpdeted?.Invoke(_segment.Count);
    }

   
}
