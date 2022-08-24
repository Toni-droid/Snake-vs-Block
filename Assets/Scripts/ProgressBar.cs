using UnityEngine;
using UnityEngine.UI;


public class ProgressBar : MonoBehaviour
{
    [SerializeField] private SnakeHead _head;
    [SerializeField] private Slider Slider;
    [SerializeField] private LevelGeneration levelGeneration;
    private float _startZ;
    private float _finishZ;
    


    private void Awake()
    {
        
        _startZ = _head.transform.position.z;
       
    }

    private void Start()
    {
        _finishZ = levelGeneration.finishZ;
    }

    private void Update()
    {
        
        float t = Mathf.InverseLerp(_startZ, _finishZ+7f, _head.transform.position.z);
        Slider.value = t;
    }
}
