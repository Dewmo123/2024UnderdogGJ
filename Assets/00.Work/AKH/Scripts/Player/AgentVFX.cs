using GGMPool;
using UnityEngine;

public class AgentVFX : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private bool _canGenerateAfterImage;
    [SerializeField] private float _generateTerm;
    [SerializeField] private PoolManagerSO _poolManager;
    [SerializeField] private PoolTypeSO _afterImage;
    [SerializeField] private SpriteRenderer lowerRenderer;
    [SerializeField] private RotateablePlayerVIsual _rotateablePlayerVIsual;
    private float _currentTime;
    private Player _player;

    public void Initialize(Player player)
    {
        _player = player;
    }

    public void ToggleAfterImage(bool value)
    {
        _canGenerateAfterImage = value;
    }
    private void Update()
    {
        if (!_canGenerateAfterImage) return;
        _currentTime += Time.deltaTime;
        if (_currentTime > _generateTerm)
        {
            _currentTime = 0;
            AfterImage img = _poolManager.Pop(_afterImage) as AfterImage;
            bool isRotated = lowerRenderer.gameObject.activeInHierarchy;
            Sprite sprite = isRotated ? lowerRenderer.sprite : _player.GetCompo<PlayerAnimator>().Renderer.sprite;
            bool isFlip = !_player.GetCompo<PlayerMovement>().IsFacingRight();

            float angle = isRotated ? _rotateablePlayerVIsual.ReversedAngle : 0;

            img.SetAfterImage(sprite, isFlip, transform.position, 0.2f, angle, _rotateablePlayerVIsual.offset, isTimeSlow ? 1f : 0);
        }
    }
    [SerializeField] bool isTimeSlow;
    public void SetIsTimeSlow(bool value)
    {
        isTimeSlow = value;
    }
}
