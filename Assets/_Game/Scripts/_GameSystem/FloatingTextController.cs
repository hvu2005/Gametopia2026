using UnityEngine;
using TMPro; // Nếu dùng TextMeshPro
using DG.Tweening;

public class FloatingTextController : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("Dùng TMP_Text nếu là Text trong UI Canvas, hoặc TextMeshPro nếu là World Space.")]
    public TextMeshPro textMesh; // Dành cho prefab 3D world space text
    public SpriteRenderer iconRenderer; // Dành cho prefab dạng icon hiệu ứng (stun, poison)

    [Header("Animation Settings")]
    public float floatDistance = 1.5f;
    public float floatDuration = 0.8f;
    public Ease easeType = Ease.OutExpo;

    [HideInInspector]
    public string poolName; // Được Manager gán vào

    private void Awake()
    {
        if (textMesh == null) textMesh = GetComponentInChildren<TextMeshPro>();
        if (iconRenderer == null) iconRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Init(FloatingTextEventData data)
    {
        transform.DOKill(); // Dừng tất cả animation cũ nếu dùng chung instance
        
        // Reset Alpha và Position ban đầu (nếu cần offset local so với target gốc)
        // transform.localPosition = Vector3.zero;

        Color startColorText = Color.white;
        Color startColorIcon = Color.white;
        
        if (textMesh != null)
        {
            if (!string.IsNullOrEmpty(data.customText))
            {
                textMesh.text = data.customText;
            }
            else
            {
                textMesh.text = data.Value.ToString();
            }
            startColorText = textMesh.color;
            startColorText.a = 1f;
            textMesh.color = startColorText;
        }

        if (iconRenderer != null)
        {
            startColorIcon = iconRenderer.color;
            startColorIcon.a = 1f;
            iconRenderer.color = startColorIcon;
        }

        // Tạo Sequence animation
        Sequence seq = DOTween.Sequence();
        
        // Animation bay lên
        seq.Append(transform.DOMoveY(transform.position.y + floatDistance, floatDuration).SetEase(easeType));

        // Nửa thời gian sau bắt đầu mờ dần
        float fadeDuration = floatDuration / 2f;
        seq.Insert(floatDuration / 2f, DOVirtual.Float(1f, 0f, fadeDuration, (v) => {
            if (textMesh != null)
            {
                startColorText.a = v;
                textMesh.color = startColorText;
            }
            if (iconRenderer != null)
            {
                startColorIcon.a = v;
                iconRenderer.color = startColorIcon;
            }
        }));

        // Sau khi hoàn thành sequence, trả về pool (hoặc xóa)
        seq.OnComplete(() =>
        {
            if (PoolController.Instance != null && !string.IsNullOrEmpty(poolName))
            {
                try 
                {
                    var pool = PoolController.Instance.GetPool(poolName);
                    pool.Release(gameObject);
                }
                catch
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        });
    }
}
