
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using KBSL_MOD.Common;
using KBSL_MOD.Models;
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

        // TODO: 로그인 재시도 로직 넣어야함
        private static IEnumerator DoLogin()
        {
            var authToken = GetSteamTicket();
            yield return new WaitUntil(() => authToken.IsCompletedSuccessfully);

            yield return WebRequestUtils.Get(
                url: string.Format(KbslConsts.Auth, authToken.Result.token),
                onSuccess: x =>
                {
                    var response = JsonConvert.DeserializeObject<WebResponseModel<AuthModel>>(x.text);
                    WebRequestUtils.SetCredential(
                        authType: response.Data.TokenType,
                        accessToken: response.Data.AccessToken,
                        refreshToken: response.Data.RefreshToken);
                },
                onFailure: x => Plugin.Log.Notice(x));
        }
    }
}