using UnityEngine;

namespace AR.MarkersSession
{
    public class SwipeRotation : MonoBehaviour
    {
        private float _rotationSpeed = 180f;
        private Vector2 _startTouchPosition;
        private Vector2 _currentTouchPosition;
        private bool _isSwipeDetected = false;
        private bool _isInitialize = false;

        void Update()
        {
            if (!_isInitialize)
                return;

            DetectSwipe();
            RotateObject();
        }

        public void Initialize(float rotationSpeed)
        {
            _rotationSpeed = rotationSpeed;
            _isInitialize = true;
        }

        private void DetectSwipe()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startTouchPosition = Input.mousePosition;
                _isSwipeDetected = false;
            }

            if (Input.GetMouseButton(0))
            {
                _currentTouchPosition = Input.mousePosition;
                Vector2 touchDelta = _currentTouchPosition - _startTouchPosition;
                if (Mathf.Abs(touchDelta.x) > Mathf.Abs(touchDelta.y))
                    _isSwipeDetected = true;                
                else               
                    _isSwipeDetected = false;          
            }
        }

        private void RotateObject()
        {
            if (Input.touchCount == 0)
                return;

            if (_isSwipeDetected)
            {
                float rotationDirection = Mathf.Sign(_currentTouchPosition.x - _startTouchPosition.x);
                transform.Rotate(Vector3.up * rotationDirection * _rotationSpeed * Time.deltaTime, Space.Self);
            }
        }
    }
}
