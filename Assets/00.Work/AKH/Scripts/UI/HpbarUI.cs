using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpbarUI : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _hpPivot;
    [SerializeField] private Transform _backBarPivot;
    private PlayerHealth _health;
    [SerializeField] private float _waitingTime;
    private float _lastHitTime;
    private bool _ischaseFill;

    private void Start()
    {
        _health = _player.GetCompo<PlayerHealth>();
        _health.OnHealthDec += HandleHitEvnet;
    }
    private void HandleHitEvnet(int a)
    {
        _hpPivot.localScale = new Vector3(_health.HpRatio,1);
        _lastHitTime = Time.time;
    }
    private void OnDestroy()
    {
        _health.OnHealthDec -= HandleHitEvnet;
    }
    private void Update()
    {
        if (_player == null) return;

        if (!_ischaseFill && _lastHitTime + _waitingTime > Time.time)
        {
            _ischaseFill = true;
            _backBarPivot.DOScale(_hpPivot.localScale, 0.5f)
                .SetEase(Ease.InCubic)
                .OnComplete(() => _ischaseFill = false);
        }
    }
}
