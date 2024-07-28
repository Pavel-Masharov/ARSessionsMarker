using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace AR.MarkersSession
{
    public static class DownloadService
    {
        public static IEnumerator DownloadMarkerImage(string url, Action<Texture2D> actionSuccess, Action<string> actionFail)
        {
            bool isDownloadSuccess = false;
            while (!isDownloadSuccess)
            {
                var request = UnityWebRequestTexture.GetTexture(url);
                if (!InternetService.InternetManager.IsInternetWorking)              
                    actionFail?.Invoke(Constants.MESSAGE_NOT_INTERNET_CONNECTION);
                
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                    actionFail?.Invoke(Constants.MESSAGE_NOT_LOADING_MARKER);
                else
                {
                    isDownloadSuccess = true;
                    actionSuccess?.Invoke(DownloadHandlerTexture.GetContent(request));
                }
                request.Dispose();
            }
        }

        public static IEnumerator DownloadModel(string url, Action<byte[]> actionSuccess, Action<string> actionFail)
        {
            bool isDownloadSuccess = false;
            while (!isDownloadSuccess)
            {
                var request = UnityWebRequest.Get(url);
                if (!InternetService.InternetManager.IsInternetWorking)
                    actionFail?.Invoke(Constants.MESSAGE_NOT_INTERNET_CONNECTION);

                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                    actionFail?.Invoke(Constants.MESSAGE_NOT_LOADING_MODEL);                
                else
                {
                    isDownloadSuccess = true;
                    actionSuccess?.Invoke(request.downloadHandler.data);
                }
                request.Dispose();
            }
        }
    }
}
