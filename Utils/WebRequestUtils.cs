using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace KBSL_MOD.Utils
{
    public static class WebRequestUtils
    {
        public static IEnumerator Get(string url, Action<DownloadHandler> onSuccess, Action<string> onFailure)
        {
            var request = UnityWebRequest.Get(url);
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

        public static IEnumerator Post(string url, List<IMultipartFormSection> form,
            Action<DownloadHandler> onSuccess, Action onFailure)
        {
            var request = UnityWebRequest.Post(url, form);
            yield return request.SendWebRequest();

            if (request.responseCode == 200)
            {
                onSuccess(request.downloadHandler);
            }
            else
            {
                onFailure();
            }
        }
    }
}