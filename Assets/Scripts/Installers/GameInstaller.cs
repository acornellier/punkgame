using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<CheckpointManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<TicketManager>().AsSingle();
    }
}