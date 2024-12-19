using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IPlayerComponent
{
    private Player _player;
    [SerializeField] private int _maxHealth;
    [SerializeField] private NotifyValue<int> _curHealth;
    public event Action<int> OnHealthDec;

    public float HpRatio => (float)_curHealth.Value / _maxHealth;

    public void Initialize(Player player)
    {
        _player = player;
        _curHealth = new NotifyValue<int>(_maxHealth);
        _curHealth.OnvalueChanged += HandleHealthChanged;
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            ChangeValue(-15);
    }
#endif
    private void HandleHealthChanged(int prev, int next)
    {
        OnHealthDec?.Invoke(next);
        if (next == 0)
        {
            //Á×¾úÀ»¶§
            _player.Dead();
        }

    }
    public void ChangeValue(int val)
    {
        _curHealth.Value = Mathf.Clamp(_curHealth.Value + val, 0, _maxHealth);
    }
}
