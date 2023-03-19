using KBSL_MOD.ConfigModels;
using KBSL_MOD.Events;
using Zenject;

namespace KBSL_MOD.Installers
{
    public class ScoreInstaller : MonoInstaller
    {
        [Inject] private readonly PlayerDataModel DataModel;

        public override void InstallBindings()
        {
            MainConfigModel mainConfig = Plugin.MainConfig;

            if (!mainConfig.Enabled) return;

            Plugin.Log.Notice("Loading ScoreInstaller...");

            Container.BindInterfacesTo<NoteEventHandler>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ScoreEventHandler>().AsSingle().NonLazy();
        }
    }
}