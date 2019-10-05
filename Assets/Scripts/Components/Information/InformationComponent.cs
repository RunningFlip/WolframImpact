using UnityEngine;


[CreateAssetMenu(fileName = "InformationComponent", menuName = "ScriptableObjects/Components Configs/InformationComponent")]
public class InformationComponent : EntityComponent
{
    public InformationPackage informationPackage = new InformationPackage();
}
