using System;
using Zenject;

namespace KBSL_MOD.Manager
{
    public class PlayerManager : IInitializable
    {
        private readonly IPlatformUserModel _platformUserModel;
        
        public PlayerManager(IPlatformUserModel platformUserModel)
        {
            _platformUserModel = platformUserModel;
        }

        public async void Initialize()
        {
            var playerInfo = await _platformUserModel.GetUserInfo();
            
            Plugin.Log.Notice("====================");
            Plugin.Log.Notice(playerInfo.userName);
            Plugin.Log.Notice(playerInfo.platformUserId);
            Plugin.Log.Notice("====================");
        }
    }
}