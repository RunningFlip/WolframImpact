using UnityEngine;


[CreateAssetMenu(fileName = "SetupComponent", menuName = "ScriptableObjects/Components Configs/SetupComponent")]
public class SetupComponent : EntityComponent
{
    public int userIndex = -1;
    public int colorIndex = -1;
}
