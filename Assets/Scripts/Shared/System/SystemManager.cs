
public class SystemManager
{
    //System table
    private IComponentSystem[] systems = new IComponentSystem[]
    {
        new StateSystem(),
        new HealthSystem(),
        new EnergySystem(),
        new DeathSystem(),
        new AudioSystem(),   
        new CollisionSystem(),
    };


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
    }
}
