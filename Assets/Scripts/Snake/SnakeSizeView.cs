using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Snake))]
public class SnakeSizeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _view;

    private Snake _snake;

    private void Awake()
    {
       _snake = GetComponent<Snake>();
    }

    private void OnEnable()
    {
        _snake.SizeUpdeted += OnSizeUpdeted;
    }

    private void OnDisable()
    {
        _snake.SizeUpdeted -= OnSizeUpdeted;
    }

    private void OnSizeUpdeted(int size)
    {
        _view.text = size.ToString();
    }
}
