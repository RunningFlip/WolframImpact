using UnityEngine;
using System;


[Serializable, CreateAssetMenu(fileName = "Development Config", menuName = "ScriptableObjects/Development Configs")]
public class DevelopmentConfig : ScriptableObject
{
    [Header("Development Values")]
    public int requiredEnergy;
    public int requiredMass;
    public int requiredObjectSpace;
    public float developmentDuration;

    [Header("Selection sorting")]
    public bool isMilitaryObject;

    [Header("Prefab")]
    public string prefabName;
    public GameObject prefab;

    [Header("Picture")]
    public Sprite picture;

    [Header("Audio")]
    public int volume;
    public AudioClip finishDevelopmentClip;
}
