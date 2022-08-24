using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(SnakeGenerator))]
public class Snake : MonoBehaviour
{
    [SerializeField] private SnakeHead _head;
    [SerializeField] float _speed, _speedR;
    [SerializeField] private Transform SnakeHead;
    [SerializeField] private float _segmentsSprigness;
    [SerializeField] private Text _textScoreCount, _textBestScore;
    [SerializeField] private SnakeGenerator _snakeGenerator;
    [SerializeField] private Game Game;
    private Vector3 _previosMousePosition;
    public List<Segment> Segment;
    public int StartTailSize;
    public event UnityAction<int> SizeUpdeted;
    public int ScoreCount = 0;
    public int _bestScore = 0;



    void Awake()
    {
        _snakeGenerator = GetComponent<SnakeGenerator>();
        Segment = _snakeGenerator.Generate(PlayerPrefs.GetInt("SaveTailSize", StartTailSize));
        SizeUpdeted?.Invoke(Segment.Count);
       _bestScore = PlayerPrefs.GetInt("SaveScore", 0);
        
    }

    private void Start()
    {
        SizeUpdeted?.Invoke(Segment.Count);
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
        foreach (Segment segment in Segment)
        {
            Vector3 tempPosition = segment.transform.position;
            segment.transform.position = Vector3.Lerp(segment.transform.position, previousPosition, _segmentsSprigness * Time.fixedDeltaTime);
            previousPosition = tempPosition;
        }

        if (Segment.Count > ScoreCount)
        {
            ScoreCount = Segment.Count;
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

    private void OnBlockCollided()
    {
        Segment deletedSegment = Segment[Segment.Count - 1];
        Segment.Remove(deletedSegment);
        Destroy(deletedSegment.gameObject);
        SizeUpdeted?.Invoke(Segment.Count);
    }

    private void OnBonusCollected(int bonusSize)
    {
        Segment.AddRange(_snakeGenerator.Generate(bonusSize));
        SizeUpdeted?.Invoke(Segment.Count);
    }

   
}
