using System.Collections.Generic;
using UnityEngine;


public class TurretSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(TurretComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            int entityID = entities[i].GetEntityID();

            TurretComponent turret = ComponentManager.GetComponent<TurretComponent>(entityID);

            if (!turret.init)
            {
                turret.init = true;
                turret.turretLink = entities[i].entityGameObject.GetComponentInChildren<TurretLink>();
            }

            if (turret.resetTurrets)
            {
                turret.resetTurrets = false;
                ResetTurrets(turret);
            }

            if (turret.updateTurrets)
            {
                turret.updateTurrets = false;
                UpdateTurrets(turret);
            }
        }
    }


    /// <summary>
    /// Updates the turret rotation.
    /// </summary>
    /// <param name="_turret"></param>
    private void UpdateTurrets(TurretComponent _turret)
    {
        if (_turret.turretLink.turretTransforms.Length > 0)
        {
            for (int i = 0; i < _turret.turretLink.turretTransforms.Length; i++)
            {
                Vector3 rotation = TransformHelper.GetDirection(_turret.turretLink.turretTransforms[i].position, _turret.targetVector);

                if (rotation != _turret.nullVec)
                {
                    _turret.turretLink.turretTransforms[i].rotation = TransformHelper.LookToDirection(rotation);
                }
            }
        }
    }


    /// <summary>
    /// Resets the turret rotation.
    /// </summary>
    /// <param name="_turret"></param>
    private void ResetTurrets(TurretComponent _turret)
    {
        _turret.targetVector = _turret.nullVec;

        for (int i = 0; i < _turret.turretLink.turretTransforms.Length; i++)
        {
            if (_turret.turretLink.turretTransforms[i] != null)
            {
                _turret.turretLink.turretTransforms[i].localRotation = Quaternion.identity;
            }
        }
    }
}
