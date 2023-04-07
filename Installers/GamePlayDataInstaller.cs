using KBSL_MOD.EventHandlers;
using KBSL_MOD.Manager;
using KBSL_MOD.Utils;
using Zenject;

namespace KBSL_MOD.Installers
{
    public class GamePlayDataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var mainConfig = Plugin.MainConfig;

            if (!mainConfig.Enabled) return;

            Plugin.Log.Notice("Loading ScoreInstaller...");
            
            if (!GamePlayUtils.isGameStart) return;
            
            Container.BindInterfacesTo<MainGameEventHandler>().AsSingle().NonLazy();
        }
    }
}