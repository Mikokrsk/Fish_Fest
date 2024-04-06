using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishUI : MonoBehaviour
{
    [SerializeField] private float _topEdgefishUI;
    [SerializeField] private float _bottomEdgefishUI;
    [SerializeField] private RectTransform _recTransform;
    [SerializeField] private float speed;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float direction;
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;

    private void OnEnable()
    {
        _minSpeed = speed / 2;
        _maxSpeed = speed + speed / 2;
        var position = Random.Range(_topEdgefishUI, _bottomEdgefishUI);
        _recTransform.localPosition = new Vector2(0, position);
        StartCoroutine(ChangeDirection());
    }

    private void OnDisable()
    {
        StopCoroutine(ChangeDirection());
    }

    void Update()
    {
        _recTransform.localPosition += Vector3.down * direction * speed * Time.deltaTime;
        if (_recTransform.localPosition.y < _bottomEdgefishUI)
        {
            _recTransform.localPosition = new Vector2(0, _bottomEdgefishUI);
            direction = -direction;
        }
        else
        {
            if (_recTransform.localPosition.y > _topEdgefishUI)
            {
                _recTransform.localPosition = new Vector2(0, _topEdgefishUI);
                direction = -direction;
            }
        }
    }

    IEnumerator ChangeDirection()
    {
        direction = Random.Range(-1, 1);
        if (direction == 0)
        {
            direction = 1;
        }
        speed = Random.Range(_minSpeed, _maxSpeed);

        var delay = Random.Range(_minDelay, _maxDelay);
        yield return new WaitForSeconds(delay);
        StartCoroutine(ChangeDirection());
    }
}