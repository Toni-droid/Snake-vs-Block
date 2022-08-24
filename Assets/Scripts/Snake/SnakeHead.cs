using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnakeHead : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private Game Game;
    [SerializeField] AudioSource _audionyam;
    [SerializeField] private Snake _snake;

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
            if (_snake._segment.Count == 0)
            {
                _snake.enabled = false;

                Game.OnSnakeDied();
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
        Game.OnSnakeReachedFinish();

    }
}
