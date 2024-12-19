using DG.Tweening;
using GGMPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour, IPoolable
{
    [SerializeField] string matarialPropertyName = "_Value";
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _upperSpriteRenderer;
    private Pool _myPool;
    [SerializeField] private PoolTypeSO _myType;
    public PoolTypeSO PoolType => _myType;

    public GameObject GameObject => gameObject;

    public void ResetItem()
    {
        _spriteRenderer.color = new Color(1, 1, 1);
        _upperSpriteRenderer.color = new Color(1, 1, 1);
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetAfterImage(Sprite spirte, bool isFlip, Vector3 position, float fadeTime, float angle, Vector2 offset, float SetmatValue)
    {
        _spriteRenderer.sprite = spirte;
        transform.position = position;
        transform.localScale = new Vector3(isFlip ? -1 : 1, 1, 1);

        MatValue(SetmatValue);

        if (angle == 0)
        {
            _upperSpriteRenderer.enabled = false;

            _spriteRenderer.DOFade(0f, fadeTime).OnComplete(() =>
            {
                _myPool.Push(this);
            });
        }
        else
        {
            _upperSpriteRenderer.enabled = true;

            _upperSpriteRenderer.transform.parent.rotation = Quaternion.Euler(0, 0, angle);

            _upperSpriteRenderer.transform.localPosition = -offset;
            _upperSpriteRenderer.transform.parent.localPosition = offset;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(_spriteRenderer.DOFade(0f, fadeTime));
            sequence.Join(_upperSpriteRenderer.DOFade(0f, fadeTime));
            sequence.OnComplete(() =>
            {
                _myPool.Push(this);
            });
            sequence.Play();
        }
    }
    private void MatValue(float value)
    {
        _spriteRenderer.material.SetFloat(matarialPropertyName, value);
        _upperSpriteRenderer.material.SetFloat(matarialPropertyName, value);
    }
    public void SetUpPool(Pool pool)
    {
        _myPool = pool;
    }
}
