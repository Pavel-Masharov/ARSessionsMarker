using System.Collections;
using Siccity.GLTFUtility;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace AR.MarkersSession
{

    public class ControllerSessionAR : MonoBehaviour
    {
        [SerializeField] private SettingsARSessionSO _settingsARSessionSO;
        [SerializeField] private WindowNotification _windowNotification;
        private ARTrackedImageManager _managerARTrackedImage;
        private Texture2D _loadedTexture;
        private GameObject _prefabObject;
        private Coroutine _coroutineInitializeSession;

        private void Start()
        {
            _managerARTrackedImage = gameObject.GetComponent<ARTrackedImageManager>();
            _coroutineInitializeSession = StartCoroutine(InitializeSession());
        }

        private IEnumerator InitializeSession()
        {
            ARSessionItem sessionItem = _settingsARSessionSO.GetSessionAtIndex(0);
            yield return StartCoroutine(DownloadService.DownloadMarkerImage(sessionItem.pathToMarker, SetImageMarker, _windowNotification.ShowWindow));
            yield return StartCoroutine(DownloadService.DownloadModel(sessionItem.pathToModel, CreatePrefabObject, _windowNotification.ShowWindow));
            SetSettingsTrackedManager();
            _managerARTrackedImage.trackedImagesChanged += Manager_trackedImagesChanged;
        }

        private void SetSettingsTrackedManager()
        {
#if !UNITY_EDITOR
            if (_managerARTrackedImage.descriptor.supportsMutableLibrary)
            {
                var libraryRuntime = _managerARTrackedImage.CreateRuntimeLibrary();
                var mutableLibrary = libraryRuntime as MutableRuntimeReferenceImageLibrary;
                mutableLibrary.ScheduleAddImageWithValidationJob(_loadedTexture, "newTexture", 0.1f);
                _managerARTrackedImage.referenceLibrary = mutableLibrary;
                _managerARTrackedImage.enabled = true;
            }
            else
                _windowNotification.ShowWindow(Constants.MESSAGE_NOT_SUPPURT_MUTABLELABRARY);
#endif
        }



        private void Manager_trackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
        {
            if (!_prefabObject.activeSelf)
            {
                _prefabObject.SetActive(true);
                _prefabObject.AddComponent<SwipeRotation>().Initialize(_settingsARSessionSO.RotationSpeed);
                _prefabObject.AddComponent<SwipeScale>().Initialize(_settingsARSessionSO.MinScale, _settingsARSessionSO.MaxScale, _settingsARSessionSO.SpeedScale);

                foreach (var item in obj.added)
                {
                    _prefabObject.transform.position = item.transform.position;
                    _prefabObject.transform.rotation = item.transform.rotation;
                }
            }
        }

        private void SetImageMarker(Texture2D texture) => _loadedTexture = texture;

        private void CreatePrefabObject(byte[] resultData)
        {
            _prefabObject = Importer.LoadFromBytes(resultData);
            _prefabObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            _prefabObject.SetActive(false);
        }

        private void OnDisable()
        {
            _managerARTrackedImage.trackedImagesChanged -= Manager_trackedImagesChanged;
            StopCoroutine(_coroutineInitializeSession);
        }
    }
}