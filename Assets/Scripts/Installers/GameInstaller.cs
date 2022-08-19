using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<AudioManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<MenuManager>().AsSingle();

        Container.BindInterfacesAndSelfTo<CheckpointManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<TicketManager>().AsSingle();

        Container.Bind<LevelLoader>().FromComponentInHierarchy().AsSingle();
    }
}