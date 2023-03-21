using KBSL_MOD.Events;
using KBSL_MOD.Manager;
using Zenject;

namespace KBSL_MOD.Installers
{
    public class GamePlayDataInstaller : MonoInstaller
    {
        [Inject] public readonly PlayerManager _playerManager;

        public override void InstallBindings()
        {
            var mainConfig = Plugin.MainConfig;

            if (!mainConfig.Enabled) return;

            Plugin.Log.Notice("Loading ScoreInstaller...");
            Plugin.Log.Notice(_playerManager.UserName);
            Plugin.Log.Notice(_playerManager.PlatformUserId);

            Container.BindInterfacesTo<NoteEventHandler>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ScoreEventHandler>().AsSingle().NonLazy();
        }
    }
}