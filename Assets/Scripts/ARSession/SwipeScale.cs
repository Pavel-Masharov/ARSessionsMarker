using UnityEngine;

namespace AR.MarkersSession
{
    public class SwipeScale : MonoBehaviour
    {
        private float _minScale = 0.05f;
        private float _maxScale = 0.2f;
        private float _scaleSpeed = 0.05f;
        private float _prevTouchDistance;
        private float _currentScale = 1.0f;
        private Vector2 _prevTouchPosFirst, _prevTouchPosSecond;
        private bool _isInitialize = false;

        private void Update()
        {
            if (!_isInitialize)
                return;

            if (Input.touchCount == 2)
            {
                Vector2 touchPos1 = Input.GetTouch(0).position;
                Vector2 touchPos2 = Input.GetTouch(1).position;
                float currentTouchDistance = Vector2.Distance(touchPos1, touchPos2);

                if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(1).phase == TouchPhase.Began)
                {
                    _prevTouchPosFirst = touchPos1;
                    _prevTouchPosSecond = touchPos2;
                    _prevTouchDistance = currentTouchDistance;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    float deltaDistance = currentTouchDistance - _prevTouchDistance;
                    _currentScale += deltaDistance * _scaleSpeed * Time.deltaTime;
                    _currentScale = Mathf.Clamp(_currentScale, _minScale, _maxScale);
                    transform.localScale = Vector3.one * _currentScale;
                    _prevTouchPosFirst = touchPos1;
                    _prevTouchPosSecond = touchPos2;
                    _prevTouchDistance = currentTouchDistance;
                }
            }
        }

        public void Initialize(float minScale, float maxScale, float scaleSpeed)
        {
            _minScale = minScale;
            _maxScale = maxScale;
            _scaleSpeed = scaleSpeed;
            _isInitialize = true;
        }
    }
}
