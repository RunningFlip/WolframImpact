using System.Collections.Generic;
using UnityEngine;

public class DevelopmentSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(DevelopmentComponent), typeof(FactionComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            int entityID = entities[i].GetEntityID();

            DevelopmentComponent development = ComponentManager.GetComponent<DevelopmentComponent>(entityID);
            FactionComponent factionComponent = ComponentManager.GetComponent<FactionComponent>(entityID);

            if (!development.init)
            {
                if (factionComponent.userIndex == -1) continue;

                development.init = true;

                //Events in HealthComponent
                HealthComponent healthComponent = ComponentManager.GetComponent<HealthComponent>(entityID);
                healthComponent.onDeath.AddListener(delegate { CancelRoutines(development); });

                //Spawn
                Entity entity = entities[i];
                development.spawn = entity.entityGameObject.GetComponentInChildren<DevelopmentSpawnLink>().spawn;

                //Resource & User
                int userEntityId = GameController.Instance.GetUserEntitId(factionComponent.userIndex);

                ResourceComponent resourceComponent = ComponentManager.GetComponent<ResourceComponent>(userEntityId);
                development.resourceComponent = resourceComponent;

                UserComponent userComponent = ComponentManager.GetComponent<UserComponent>(userEntityId);
                development.userComponent = userComponent;

                //Marker
                MarkerComponent markerComponent = ComponentManager.GetComponent<MarkerComponent>(entityID);
                development.markerComponent = markerComponent;
            }


            if (development.cancelAll)
            {
                development.cancelAll = false;
                CancelAll(development);
            }

            if (development.cancelAt != -1)
            {
                CancelAtIndex(development);
                development.cancelAt = -1;
            }

            if (development.developmentIsActive)
            {
                CheckDevelopingObjects(development, factionComponent);
            }
        }
    }


    private void CheckDevelopingObjects(DevelopmentComponent _development, FactionComponent _factionComponent)
    {
        if (_development.objectsToDevelop.Count > 0)
        {
            _development.passedTime += Time.deltaTime;

            DevelopmentConfig config = _development.objectsToDevelop[0];

            if (_development.passedTime >= config.developmentDuration)
            {
                if (_development.resourceComponent.currentObjectSpace + config.requiredObjectSpace <= _development.resourceComponent.objectCap)
                {
                    SpawnObject(_development, _factionComponent, config);
                }
                else
                {
                    WaitForObjectSpace(_development, _factionComponent);
                }
            }
        }
    }


    private void WaitForObjectSpace(DevelopmentComponent _development, FactionComponent _factionComponent)
    {
        _development.waitsForObjectCap = true;
        DevelopmentConfig config = _development.objectsToDevelop[0];

        _development.waitForObjectSpaceJob = new DefaultJob(delegate 
        {
            if (_development.resourceComponent.currentObjectSpace + config.requiredObjectSpace <= _development.resourceComponent.objectCap)
            {
                SpawnObject(_development, _factionComponent, config);
                _development.waitsForObjectCap = false;

                _development.waitForObjectSpaceJob.CancelJob();
                _development.waitForObjectSpaceJob = null;
            }
        });
    }


    private void SpawnObject(DevelopmentComponent _development, FactionComponent _factionComponent, DevelopmentConfig _config)
    {
        GameObject newObject = GameObject.Instantiate(_config.prefab, _development.spawn.position, Quaternion.Euler(0f, 180f, 0f));
        int entityId = newObject.GetComponent<GameObjectEntityHolder>().GetEntityID();

        _development.waitForObjectSetupJob = new DefaultJob(delegate
        {
            if (ComponentManager.ComponentReady<InformationComponent>(entityId))
            {
                _development.userComponent.factionEntities.Add(entityId);

                //CoWorker infopackage
                InformationComponent info = ComponentManager.GetComponent<InformationComponent>(entityId);
                info.informationPackage.factionComponent.userIndex = _factionComponent.userIndex;
                info.informationPackage.identifierComponent.objectName = _config.prefabName;
                info.informationPackage.identifierComponent.objectSprite = _config.picture;
                info.informationPackage.typeComponent.isMilitaryObject = _config.isMilitaryObject;

                //Resources
                _development.resourceComponent.currentObjectSpace += _config.requiredObjectSpace;

                //Marker
                if (_development.markerComponent.firstSetted)
                {
                    _development.waitForObjectSetupJob = new DefaultJob(delegate
                    {
                        _development.waitFrameJob = new WaitFrameJob(delegate 
                        {
                            _development.currentCommandTarget = ComponentManager.GetComponent<CommandTargetComponent>(entityId);
                            MoveToMarker(_development);
                        }, 1);
                    });
                }

                if (_development.objectsToDevelop.Count > 0)
                {
                    _development.objectsToDevelop.RemoveAt(0);
                }                
                _development.passedTime = 0;

                _development.waitForObjectSetupJob.CancelJob();
                _development.waitForObjectSetupJob = null;
            }
        });
    }


    private void MoveToMarker(DevelopmentComponent _development)
    {
        _development.waitFrameJob.CancelJob();

        TaskHelper.AddTask(_development.currentCommandTarget, CommandType.Command_On_Ground, null, _development.markerComponent.markerTransform.position);
        _development.currentCommandTarget = null;
    }


    private void CancelRoutines(DevelopmentComponent _development)
    {
        if (_development.waitFrameJob != null)
        {
            _development.waitFrameJob.CancelJob();
        }

        if (_development.waitForObjectSetupJob != null)
        {
            _development.waitForObjectSetupJob.CancelJob();
        }
    }


    private void CancelAtIndex(DevelopmentComponent _development)
    {
        _development.resourceComponent.currentEnergy += _development.objectsToDevelop[_development.cancelAt].requiredEnergy;
        _development.resourceComponent.currentMass += _development.objectsToDevelop[_development.cancelAt].requiredMass;

        _development.objectsToDevelop.RemoveAt(_development.cancelAt);
        _development.passedTime = 0;

        if (_development.cancelAt == 0 && _development.waitsForObjectCap)
        {
            _development.waitsForObjectCap = false; //Cause only the first object in line can stop developing
            
            if (_development.waitForObjectSpaceJob != null)
            {
                _development.waitForObjectSpaceJob.CancelJob();
                _development.waitForObjectSpaceJob = null;
            }
        }
    }


    private void CancelAll(DevelopmentComponent _development)
    {
        _development.objectsToDevelop = new List<DevelopmentConfig>();
        _development.passedTime = 0;
    }
}
