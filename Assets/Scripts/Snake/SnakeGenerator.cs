using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeGenerator : MonoBehaviour
{
    //[SerializeField] private int _snakeSize;
    [SerializeField] private Segment _segmentTemplate;

    public List<Segment> Generate(int count)
    {
        List<Segment> segment = new List<Segment>();
        for (int i = 0; i < count; i++)
        {
            segment.Add(Instantiate(_segmentTemplate, transform));
        }
         return segment;
    }
}
