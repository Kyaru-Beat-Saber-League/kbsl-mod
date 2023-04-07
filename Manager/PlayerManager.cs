using System;
using KBSL_MOD.Models;
using Zenject;

namespace KBSL_MOD.Manager
{
    public class PlayerManager : IInitializable
    {
        public static PlayerModel PlayerModel;
        
        [Inject] private IPlatformUserModel _platformUserModel;

        public async void Initialize()
        {
            var playerInfo = await _platformUserModel.GetUserInfo();
            PlayerModel.UserName = playerInfo.userName;
            PlayerModel.PlatformUserId = playerInfo.platformUserId;
            
            Plugin.Log.Notice("====================");
            Plugin.Log.Notice(playerInfo.userName);
            Plugin.Log.Notice(playerInfo.platformUserId);
            Plugin.Log.Notice(playerInfo.platform.ToString());
            Plugin.Log.Notice("====================");
        }
    }
}