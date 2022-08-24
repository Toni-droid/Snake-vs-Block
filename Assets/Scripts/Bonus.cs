using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private TMP_Text _view;
    [SerializeField] private Vector2Int _bonusSizeRange;
    [SerializeField] private ParticleSystem _healOnceLoop;
    

    private int _bonusSize;
    private void Start()
    {
        
        _bonusSize = Random.Range(_bonusSizeRange.x, _bonusSizeRange.y);
        _view.text = _bonusSize.ToString();
    }

    public int Collect()
    {
        
        Destroy(gameObject);
        _healOnceLoop.Play();
        return _bonusSize;
    }
}
