using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class WorldItem : MonoBehaviour
{
    public ItemDataSO Data { get; private set; }

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    private bool _isPickedUp;

    public void Init(ItemDataSO data)
    {
        Data = data;
        _isPickedUp = false;

        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_collider == null)
            _collider = GetComponent<BoxCollider2D>();

        _spriteRenderer.sprite = data.Icon;
        _collider.isTrigger = true;
        _collider.enabled = true;
        gameObject.SetActive(true);
    }

    public void FlyToSlot(Vector3 targetWorldPos, float duration, Action onComplete)
    {
        if (_isPickedUp) return;
        _isPickedUp = true;
        _collider.enabled = false;

        StartCoroutine(FlyCoroutine(targetWorldPos, duration, onComplete));
    }

    private IEnumerator FlyCoroutine(Vector3 target, float duration, Action onComplete)
    {
        Vector3 start = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            float eased = 1f - Mathf.Pow(1f - t, 3f);

            float arc = Mathf.Sin(t * Mathf.PI) * 0.5f;

            Vector3 pos = Vector3.Lerp(start, target, eased);
            pos.y += arc;

            transform.position = pos;

            float scale = Mathf.Lerp(1f, 0.3f, eased);
            transform.localScale = Vector3.one * scale;

            yield return null;
        }

        transform.position = target;
        onComplete?.Invoke();

        transform.localScale = Vector3.one;
        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (_isPickedUp || Data == null) return;

        if (InventoryUI.Instance != null)
        {
            InventoryUI.Instance.TryPickUp(this);
        }
        else
        {
            Debug.LogWarning("WorldItem: InventoryUI.Instance is null!");
        }
    }

    private void OnEnable()
    {
        _isPickedUp = false;
        transform.localScale = Vector3.one;
    }
}
