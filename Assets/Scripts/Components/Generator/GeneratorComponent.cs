using UnityEngine;


[CreateAssetMenu(fileName = "GeneratorComponent", menuName = "ScriptableObjects/Components Configs/GeneratorComponent")]
public class GeneratorComponent : EntityComponent
{
    public int amountPerTick;
    public GeneratorType generatorType;
}
