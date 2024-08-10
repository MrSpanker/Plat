using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlackWhiteFilter : MonoBehaviour
{
    [SerializeField] private VolumeProfile _volumeProfile;

    private ColorAdjustments _colorAdjustments;

    private void Start()
    {
        _volumeProfile.TryGet<ColorAdjustments>(out var colorAdjustments);
        _colorAdjustments = colorAdjustments;
    }

    public void SetSaturation(float normalizedHP)
    {
        _colorAdjustments.saturation.value = Mathf.Lerp(-100,0, normalizedHP);
    }
}
