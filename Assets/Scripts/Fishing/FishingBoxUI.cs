using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FishingBoxUI : MonoBehaviour
{
    [SerializeField] private FishingManager _fishingManager;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private GameObject _fishingBar;
    [SerializeField] private Image _fishUI;
    [SerializeField] private Slider _progresbar;
    [SerializeField] private InputAction _hookAction;
    [SerializeField] private float _speed;
    [SerializeField] private float _topEdgeFishingBox;
    [SerializeField] private float _bottomEdgeFishingBox;
    [SerializeField] private float _progresbarSpeed;

    private void Awake()
    {
        _fishingManager = FishingManager.Instance;
    }

    private void OnEnable()
    {
        _progresbar.value = _progresbar.maxValue / 4;

        _hookAction.Enable();

        _topEdgeFishingBox = _fishingBar.GetComponent<Image>().rectTransform.rect.height / 2
            - _rectTransform.rect.height / 2;
        _bottomEdgeFishingBox = -_topEdgeFishingBox;

        var position = Random.Range(_topEdgeFishingBox, _bottomEdgeFishingBox);
        _rectTransform.localPosition = new Vector2(0, position);
    }
    private void OnDisable()
    {
        _hookAction.Enable();
    }

    private void Update()
    {
        Fishing();
        CheckFishingBoxUIPosition();
    }

    public void Fishing()
    {
        if (_hookAction.IsPressed())
        {
            _rectTransform.localPosition += Vector3.up * _speed * Time.deltaTime;
        }
        else
        {
            _rectTransform.localPosition += Vector3.down * _speed * Time.deltaTime;
        }

        if (_fishUI.rectTransform.localPosition.y < _rectTransform.localPosition.y + _rectTransform.rect.height / 2 &&
            _fishUI.rectTransform.localPosition.y > _rectTransform.localPosition.y - _rectTransform.rect.height / 2)
        {
            _progresbar.value += _progresbarSpeed * Time.deltaTime;
            if (_progresbar.value >= _progresbar.maxValue)
            {
                _fishingManager.StopFishing(true);
            }
        }
        else
        {
            _progresbar.value -= _progresbarSpeed * Time.deltaTime;
            if (_progresbar.value <= _progresbar.minValue)
            {
                _fishingManager.StopFishing(false);
            }
        }
    }

    public void CheckFishingBoxUIPosition()
    {
        if (_rectTransform.localPosition.y < _bottomEdgeFishingBox)
        {
            _rectTransform.localPosition = new Vector2(0, _bottomEdgeFishingBox);
        }
        else
        {
            if (_rectTransform.localPosition.y > _topEdgeFishingBox)
            {
                _rectTransform.localPosition = new Vector2(0, _topEdgeFishingBox);
            }
        }
    }
}