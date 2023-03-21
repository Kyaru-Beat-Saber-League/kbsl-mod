using System;
using Zenject;

namespace KBSL_MOD.Manager
{
    public class PlayerManager : IInitializable
    {
        public string UserName { get; set; }
        public string PlatformUserId { get; set; }
        
        [Inject] private readonly IPlatformUserModel _platformUserModel;

        public async void Initialize()
        {
            var playerInfo = await _platformUserModel.GetUserInfo();
            UserName = playerInfo.userName;
            PlatformUserId = playerInfo.platformUserId;
            
            Plugin.Log.Notice("====================");
            Plugin.Log.Notice(playerInfo.userName);
            Plugin.Log.Notice(playerInfo.platformUserId);
            Plugin.Log.Notice(playerInfo.platform.ToString());
            Plugin.Log.Notice("====================");
        }
    }
}