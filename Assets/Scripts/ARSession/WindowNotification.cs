using UnityEngine;
using UnityEngine.UI;

namespace AR.MarkersSession
{
    public class WindowNotification : MonoBehaviour
    {
        [SerializeField] private GameObject _window;
        [SerializeField] private Text _textMessage;
        [SerializeField] private Button _buttonOk;
        private bool _isOpened = false;

        private void Start()
        {
            HideWindow();
            _buttonOk.onClick.AddListener(HideWindow);
        }

        public void ShowWindow(string textMessage)
        {
            if (_isOpened)
                return;

            _textMessage.text = textMessage;
            _window.SetActive(true);
            _isOpened = true;
        }

        public void HideWindow()
        {
            _window.SetActive(false);
            _isOpened = false;
        }

        private void OnDestroy()
        {
            _buttonOk.onClick.RemoveListener(HideWindow);
        }
    }
}