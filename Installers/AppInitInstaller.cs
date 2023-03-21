using KBSL_MOD.Manager;
using Zenject;

namespace KBSL_MOD.Installers
{
    public class AppInitInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PlayerManager>().AsSingle();
        }
    }
}