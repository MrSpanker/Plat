using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecayTimer : MonoBehaviour
{
    [SerializeField] private DecayManager _decayManager;
    [SerializeField] private float _time;
    [Range(0f, 1f)][SerializeField] private List<float> _stage;

    private float _elapsedTime;
    private int _currentStageIndex = 0;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _time)
        {
            _decayManager.ActiveFinalStage();
            return;
        }

        float normalizedTime = _elapsedTime / _time;

        if (_currentStageIndex < _stage.Count && normalizedTime >= _stage[_currentStageIndex])
        {
            _decayManager.ActiveStage(_currentStageIndex);
            _currentStageIndex++;

            if (_currentStageIndex >= _stage.Count)
                Debug.LogWarning("Последняя стадия перед смертью");
        }
    }

    public void Reset()
    {
        _elapsedTime = 0f;
        _currentStageIndex = 0;
    }
}