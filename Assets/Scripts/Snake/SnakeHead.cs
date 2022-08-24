using UnityEngine;
using UnityEngine.Events;

public class SnakeHead : MonoBehaviour
{
    [SerializeField] private Snake _snake;
    [SerializeField] private Game _game;

    private Rigidbody _rb;
    private AudioSource _audionyam;
    

    public event UnityAction<int> BonusCollected;
    public event UnityAction BlockCollided;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audionyam = GetComponent<AudioSource>();
    }

    public void Move(Vector3 newPosition)
    {
        _rb.MovePosition(newPosition);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Block block)) 
        {
            if (_snake.Segment.Count == 0)
            {
                _snake.enabled = false;
                _game.OnSnakeDied();

                PlayerPrefs.SetInt("SaveTailSize", _snake.StartTailSize);
            }
            else
            {
                BlockCollided?.Invoke();
                block.Fill();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Bonus bonus))
        {
            _audionyam.Play();
            BonusCollected?.Invoke(bonus.Collect());
        }
    }

    public void ReachFinish()
    {
        _game.OnSnakeReachedFinish();
    }
}
