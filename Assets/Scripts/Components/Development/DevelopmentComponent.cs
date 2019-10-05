using UnityEngine;
using System;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "DevelopmentComponent", menuName = "ScriptableObjects/Components Configs/DevelopmentComponent")]
public class DevelopmentComponent : EntityComponent
{
    [Header("Develop status")]
    public bool developmentIsActive = true;


    //Flag
    [NonSerialized] public bool cancelAll;
    [NonSerialized] public bool waitsForObjectCap;

    //Development
    [NonSerialized] public int cancelAt = -1;

    //To build list
    [NonSerialized] public List<DevelopmentConfig> objectsToDevelop = new List<DevelopmentConfig>();
    [NonSerialized] public CommandTargetComponent currentCommandTarget;

    //Time
    [NonSerialized] public float passedTime;

    //Component
    [NonSerialized] public ResourceComponent resourceComponent;
    [NonSerialized] public UserComponent userComponent;
    [NonSerialized] public MarkerComponent markerComponent;

    //Spawn
    [NonSerialized] public Transform spawn;

    //Job
    [NonSerialized] public DefaultJob waitForObjectSpaceJob;
    [NonSerialized] public DefaultJob waitForObjectSetupJob;
    [NonSerialized] public DefaultJob moveToMarkerJob;
    [NonSerialized] public WaitFrameJob waitFrameJob;
}
