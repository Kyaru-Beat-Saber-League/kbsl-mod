using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace KBSL_MOD.Utils
{
    public static class WebRequestUtils
    {
        [CanBeNull] private static string AuthType { get; set; }
        [CanBeNull] private static string AccessToken { get; set; }
        [CanBeNull] private static string RefreshToken { get; set; }

        public static void SetCredential(string authType, string accessToken, string refreshToken)
        {
            AuthType = authType;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
        
        public static IEnumerator Get(string url, Action<DownloadHandler> onSuccess, Action<string> onFailure)
        {
            var request = UnityWebRequest.Get(url);
            request.SetRequestHeader("Content-Type", "application/json");
            if (!string.IsNullOrEmpty(AccessToken))
            {
                request.SetRequestHeader("Authorization", $"{AuthType}{AccessToken}");
            }
            
            yield return request.SendWebRequest();

            if (request.responseCode == 200)
            {
                onSuccess(request.downloadHandler);
            }
            else
            {
                onFailure(request.error);
            }
        }

        public static IEnumerator Post(string url, string body, Action<DownloadHandler> onSuccess, Action<string> onFailure)
        {
            var request = new UnityWebRequest(url);
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            if (!string.IsNullOrEmpty(AccessToken))
            {
                request.SetRequestHeader("Authorization", $"{AuthType}{AccessToken}");
                Plugin.Log.Notice($"{AuthType}{AccessToken}");
            }

            yield return request.SendWebRequest();

            if (request.responseCode == 200)
            {
                onSuccess(request.downloadHandler);
            }
            else
            {
                onFailure(request.error);
            }
        }
    }
}