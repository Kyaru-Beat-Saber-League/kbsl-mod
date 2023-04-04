
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using KBSL_MOD.Common;
using Newtonsoft.Json;
using UnityEngine;

namespace KBSL_MOD.Utils
{
    internal static class AuthUtils
    {
        public static IEnumerator Login()
        {
            yield return DoLogin();
        }

        private static async Task<PlatformUserAuthTokenData> GetSteamTicket()
        {
            return await new SteamPlatformUserModel().GetUserAuthToken();
        }

        private static IEnumerator DoLogin()
        {
            var authToken = GetSteamTicket();
            yield return new WaitUntil(() => authToken.IsCompletedSuccessfully);
            
            Plugin.Log.Notice(authToken.Result.token);

            yield return WebRequestUtils.Get(
                url: string.Format(KbslConsts.Auth, authToken.Result.token),
                onSuccess: x => Plugin.Log.Notice(x.text),
                onFailure: x => Plugin.Log.Notice(x));
        }
    }
}