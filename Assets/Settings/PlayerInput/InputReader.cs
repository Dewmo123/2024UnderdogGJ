using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInput;

[CreateAssetMenu(menuName = "SO/PlayerInput")]
public class InputReader : ScriptableObject, IInputActions,IPlayerComponent
{
    private PlayerInput _playerInput;
    private Player _player;

    public NotifyValue<bool> leftClick = new NotifyValue<bool>();
    public NotifyValue<bool> rightClick = new NotifyValue<bool>();
    
    public event Action OnLeftPerform;
    public event Action OnLeftCanceled;

    public event Action OnRightPerform;
    public event Action OnRightCanceled;

    public Vector2 MousePos { get; private set; }
    public Vector2 MouseWorldPos => Camera.main.ScreenToWorldPoint(MousePos);

    private void OnEnable()
    {
        if (_playerInput == null)
            _playerInput = new PlayerInput();
        leftClick.OnvalueChanged += HandleLeftClick;
        rightClick.OnvalueChanged += HandleRightClick;
        _playerInput.Input.SetCallbacks(this);
        _playerInput.Input.Enable();
    }

    private void HandleRightClick(bool prev, bool next)
    {
        if (next)
            OnRightPerform?.Invoke();
        else
            OnRightCanceled?.Invoke();
    }

    private void HandleLeftClick(bool prev, bool next)
    {
        if (next)
            OnLeftPerform?.Invoke();
        else
            OnLeftCanceled?.Invoke();

    }
    private void OnDisable()
    {
        leftClick.OnvalueChanged -= HandleLeftClick;
    }
    public void Disable()
    {
        _playerInput.Input.Disable();
    }
    public void Enable()
    {
        _playerInput.Input.Enable();
    }
    public void Initialize(Player player)
    {
        _player = player;
    }
    public Vector2 MoveVector { get; private set; }
    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        MoveVector = context.ReadValue<Vector2>();
    }
    public void OnMousePos(InputAction.CallbackContext context)
    {
        Vector2 mouseScreenPos = context.ReadValue<Vector2>();
        MousePos = mouseScreenPos;
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
            leftClick.Value = true;
        if (context.canceled)
            leftClick.Value = false;
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        if (context.performed)
            rightClick.Value = true;
        if (context.canceled)
            rightClick.Value = false;
    }
}
