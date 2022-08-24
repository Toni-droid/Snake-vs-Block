using UnityEngine;
using UnityEngine.UI;


public class ProgressBar : MonoBehaviour
{
    public SnakeHead _head;
    //public Transform FinishPlatform;
    public Slider Slider;
    public LevelGeneration levelGeneration;
    //public float AcceptableFinishPlayerDistance = 1f;

    private float _startZ;
    private float _finishZ;
    

    //private float _minimumReachedZ;

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
