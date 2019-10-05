using UnityEngine;
using System;


[Serializable, CreateAssetMenu(fileName = "ConstructionConfig", menuName = "ScriptableObjects/Construction Configs")]
public class ConstructionConfig : ScriptableObject
{
    [Header("Construction Values")]
    public int requiredEnergy;
    public int requiredMass;

    [Header("Selection sorting")]
    public bool isMilitaryObject;

    [Header("Prefab")]
    public string prefabName;
    public GameObject prefab;
    public GameObject temporaryPrefab;

    [Header("Picture")]
    public Sprite picture;

    [Header("Audio")]
    public int volume;
    public AudioClip finishConstructionClip;
}
