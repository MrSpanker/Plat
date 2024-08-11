using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Experimental.Rendering.Universal;

public class DecayManager : MonoBehaviour
{
    [SerializeField] private DecayTimer _decayTimer;
    [SerializeField] private float _recoveryTime = 2f;

    [Header("Свет")]
    [SerializeField] private Light2D _light2D;
    private float _startIntensity;
    private float _startInnerRadius;
    [SerializeField] private float _endIntensity = 0.18f;
    [SerializeField] private float _endInnerRadius = 0.1f;
    [SerializeField] private float _fadeLightDuration = 10f;

    //[Header("Звук")]
    //[SerializeField] private AudioMixer _audioMixer;
    //private float _startVolume;
    //[SerializeField] private float _endVolume = 0.18f;
    //[SerializeField] private float _fadeVolumeDuration = 10f;

    [Header("Скорость персонажа")]
    [SerializeField] private PlayerController playerController;
    private float _startMoveSpeed;
    [SerializeField] private float _endMoveSpeed = 5f;
    [SerializeField] private float _fadeMoveSpeedDuration = 7f;

    private void Start()
    {
        _startIntensity = _light2D.intensity;
        _startInnerRadius = _light2D.pointLightInnerRadius;

        //_startVolume = _audioMixer.

        _startMoveSpeed = playerController.moveSpeed;
    }

    public void ResetDecay()
    {
        StopAllCoroutines();

        StartCoroutine(FadeLightIntensity(_light2D.intensity, _light2D.pointLightInnerRadius, _startIntensity, _startInnerRadius, _recoveryTime));
        StartCoroutine(FadeMoveSpeed(_endMoveSpeed, _startMoveSpeed, _recoveryTime));
        _decayTimer.Reset();
        Debug.Log("Увядание перезапустилось");
    }

    public void ActiveStage(int stageNumber)
    {
        Debug.Log("Увядание " + stageNumber);
        switch (stageNumber)
        {
            case 0:
                Stage1();
                break;

            case 1:
                Stage2();
                break;

            case 2:
                break;

            case 3:
                break;
        }
    }

    private void Stage1()
    {
        StartCoroutine(FadeLightIntensity(_startIntensity, _startInnerRadius, _endIntensity, _endInnerRadius, _fadeLightDuration));
    }

    private void Stage2()
    {
        StartCoroutine(FadeMoveSpeed(_startMoveSpeed, _endMoveSpeed, _fadeMoveSpeedDuration));
    }

    public void ActiveFinalStage()
    {

    }

    private IEnumerator FadeLightIntensity(float startIntensity, float startInnerRadius, float endIntensity, float endInnerRadius, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _light2D.intensity = Mathf.Lerp(startIntensity, endIntensity, elapsed / duration);
            _light2D.pointLightInnerRadius = Mathf.Lerp(startInnerRadius, endInnerRadius, elapsed / duration);
            yield return null;
        }
        _light2D.intensity = endIntensity;
        _light2D.pointLightInnerRadius = endInnerRadius;
    }

    private IEnumerator FadeMoveSpeed(float startMoveSpeed, float endMoveSpeed, float duration)
    {
        float elapsed = 0f;
        while (elapsed < _fadeMoveSpeedDuration)
        {
            elapsed += Time.deltaTime;
            playerController.moveSpeed = Mathf.Lerp(startMoveSpeed, endMoveSpeed, elapsed / duration);
            yield return null;
        }
        playerController.moveSpeed = endMoveSpeed;
    }
}