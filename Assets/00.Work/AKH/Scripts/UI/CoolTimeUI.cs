using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeUI : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private Vector3 _offset;
    private RectTransform rTrm;
    [SerializeField] private Transform _images;
    [SerializeField] private Image image;
    private PlayerSniper _sniper;
    private void Start()
    {
        _sniper = _target.GetCompo<PlayerSniper>();
        rTrm = GetComponent<RectTransform>();
    }
    private void Update()
    {
        rTrm.position = Camera.main.WorldToScreenPoint(_target.transform.position + _offset);
        _images.gameObject.SetActive(_sniper.isCoolTime);
        if (_sniper.isCoolTime)
        {
            image.fillAmount = _sniper.Ratio;
        }
    }
    [ContextMenu("AddOffset")]
    public void AddOffset()
    {
        rTrm = GetComponent<RectTransform>();
        rTrm.position = Camera.main.WorldToScreenPoint(_target.transform.position + _offset);
    }
}
