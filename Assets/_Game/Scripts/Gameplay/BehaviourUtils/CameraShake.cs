using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    [SerializeField] private Transform camTransform;

    private Vector3 originalPos;
    private Tween shakeTween;

    private void Awake()
    {
        Instance = this;

        if (camTransform == null)
            camTransform = Camera.main.transform;

        originalPos = camTransform.localPosition;
    }

    public void Shake(float duration = 0.2f, float strength = 0.3f, int vibrato = 20)
    {
        // kill shake cũ nếu có
        shakeTween?.Kill();

        // reset vị trí
        camTransform.localPosition = originalPos;

        shakeTween = camTransform.DOShakePosition(
            duration,
            strength,
            vibrato,
            randomness: 90,
            snapping: false,
            fadeOut: true
        ).OnComplete(() =>
        {
            camTransform.localPosition = originalPos;
        });
    }
}