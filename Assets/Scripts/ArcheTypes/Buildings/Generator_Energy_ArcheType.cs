using UnityEngine;


[CreateAssetMenu(fileName = "Generator_Energy_ArcheType", menuName = "ScriptableObjects/ArcheType Configs/Generator_Energy_ArcheType")]
public class Generator_Energy_ArcheType : SelectableArcheType
{
    [Header("Generator")]
    public GeneratorType generatorType;
    public int amountPerTick;


    //Components
    private GeneratorComponent generatorComponent;


    public override void Init()
    {
        if (init) return;
        base.Init();

        //Components      
        generatorComponent = ScriptableObject.CreateInstance<GeneratorComponent>();

        //Generator
        generatorComponent.generatorType = generatorType;
        generatorComponent.amountPerTick = amountPerTick;


        //Add components
        AddComponentsToList(
            generatorComponent
            );
    }
}
