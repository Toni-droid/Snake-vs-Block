using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    [SerializeField] private Vector2Int _destroyPriceRange;
    [SerializeField] ParticleSystem _featherExplosion;
    [SerializeField] private Color[] _colors;
    [SerializeField] private AudioSource _audioSourceCollider;
    private int _destroyPrice;
    private int _filling;
    private MeshRenderer _meshRenderer;
    public int LeftToFill => _destroyPrice - _filling;  
    public event UnityAction<int> FillingUpdated;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material.color = _colors[Random.Range(0, _colors.Length)];
        _destroyPrice = Random.Range(_destroyPriceRange.x, _destroyPriceRange.y);
        FillingUpdated?.Invoke(LeftToFill);
        _audioSourceCollider = GetComponent<AudioSource>();
    }

    public void Fill()
    {
        _audioSourceCollider.Play();
        _filling++;
        FillingUpdated?.Invoke(LeftToFill);

        if (_filling == _destroyPrice)
        {
            Destroy(gameObject);
            _featherExplosion.Play();


        }
    }

    
}
