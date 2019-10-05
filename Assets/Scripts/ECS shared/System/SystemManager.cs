
public class SystemManager
{
    //System table
    private IComponentSystem[] systems = new IComponentSystem[]
    {
        new AudioSystem(),
        new BulletSystem(),
        new CameraSystem(),
        new CollisionSystem(),
        new CommandSystem(),
        new CommandTargetSystem(),
        new ConstructionSystem(),
        new DeathSystem(),
        new DetectionSystem(),
        new DevelopmentSystem(),
        new EnergySystem(),
        new GeneratorSystem(),
        new HealthSystem(),
        new InputSystem(),
        new MarkerSystem(),
        new MiniMapSystem(),
        new ResourceSystem(),
        new SelectionSystem(),
        new SelectionTargetSystem(),
        new SetupSystem(),
        new StateSystem(),
        new StorageSystem(),
        new TurretSystem(),
        new UITransmitterSystem(),
    };

    //Job system
    private JobSystem jobSystem = new JobSystem();


    public SystemManager()
    {
        //Calls the OnCreate functions in all systems.
        for (int i = 0; i < systems.Length; i++)
        {
            systems[i].OnCreate();
        }
    }


    /// <summary>
    /// Udates all existing component-systems.
    /// </summary>
    public void SystemUpdate()
    {
        for (int i = 0; i < systems.Length; i++)
        {
            systems[i].OnUpdate();
        }

        jobSystem.OnUpadate();
    }
}
