using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectionTargetSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(SelectionTargetComponent), typeof(HealthComponent), typeof(EnergyComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            Entity entity = entities[i];
            int entityID = entity.GetEntityID();

            SelectionTargetComponent targetComp = ComponentManager.GetComponent<SelectionTargetComponent>(entityID);
            HealthComponent health = ComponentManager.GetComponent<HealthComponent>(entityID);
            EnergyComponent energy = ComponentManager.GetComponent<EnergyComponent>(entityID);

            if (!targetComp.init)
            {
                targetComp.init = true;
                SelectionTargetSetup(entity, targetComp, health, energy);
            }


            if (targetComp.selected)
            {
                //Updates the canvas rotation, so the canvas is alway looking to the camera.
                targetComp.selectionTypeUI.canvasTransform.LookAt(
                    targetComp.selectionTypeUI.canvasTransform.position + targetComp.mainCamTransform.rotation * targetComp.vectorBack,
                    targetComp.mainCamTransform.rotation * targetComp.vectorUp);
            }
        }
    }


    /// <summary>
    /// Setups the selection target component and all neccessary properties.
    /// </summary>
    /// <param name="_selTargetComponent"></param>
    private void SelectionTargetSetup(Entity _entity, SelectionTargetComponent _target, HealthComponent _health, EnergyComponent _energy)
    {
        //Camera
        _target.mainCam = Camera.main;
        _target.mainCamTransform = _target.mainCam.transform;

        //UI
        _target.selectionTypeUI = _entity.entityGameObject.GetComponentInChildren<SelectionTypeUI>();
        _target.colorGradient = GameController.Instance.GameParameter.worldUIHealthGradient;
        _target.deactiveColor = new Color(0, 0, 0, 0);

        //Silhouette
        _target.selectionTypeUI.silhouetteRenderer.color = _target.deactiveColor;

        //Wold Bars
        _target.selectionTypeUI.bars.alpha = 0;
        _target.selectionTypeUI.startAplha = GameController.Instance.GameParameter.defaultWoldBarAlpha;

        //Events
        _target.onSelectionStarts.AddListener(delegate { SetObjectStatus(_target, _health, _energy, true); });
        _target.onSelectionEnds.AddListener(delegate { SetObjectStatus(_target, _health, _energy, false); });
    }


    /// <summary>
    /// Sets the visibility of the silhouette and the world bars.
    /// </summary>
    /// <param name="_status"></param>
    private void SetObjectStatus(SelectionTargetComponent _target, HealthComponent _health, EnergyComponent _energy, bool _status)
    {
        _target.selected = _status;

        if (_status)
        {
            //Renderings
            _target.selectionTypeUI.bars.alpha = _target.selectionTypeUI.startAplha;
            SetBarFillAmount(_target.selectionTypeUI.healthBar, _health.maxHealth, _health.currentHealth);
            SetBarFillAmount(_target.selectionTypeUI.energyBar, _energy.maxEnergy, _energy.currentEnergy);
            
            SetSihouetteColor(_target, _health.maxHealth, _health.currentHealth);

            //Bars
            if (_health != null)
            {
                _health.onCurrentHealthChanged.AddListener(delegate { SetBarFillAmount(_target.selectionTypeUI.healthBar, _health.maxHealth, _health.currentHealth); SetSihouetteColor(_target, _health.maxHealth, _health.currentHealth); });
                _health.onMaxHealthChanged.AddListener(delegate { SetBarFillAmount(_target.selectionTypeUI.healthBar, _health.maxHealth, _health.currentHealth); SetSihouetteColor(_target, _health.maxHealth, _health.currentHealth); });
            }
            if (_energy != null)
            {
                _energy.onCurrentEnergyChanged.AddListener(delegate { SetBarFillAmount(_target.selectionTypeUI.energyBar, _energy.maxEnergy, _energy.currentEnergy); });
                _energy.onMaxEnergyChanged.AddListener(delegate { SetBarFillAmount(_target.selectionTypeUI.energyBar, _energy.maxEnergy, _energy.currentEnergy); });
            }
        }
        else
        {
            //Renderings
            _target.selectionTypeUI.bars.alpha = 0;
            _target.selectionTypeUI.silhouetteRenderer.color = _target.deactiveColor;

            //Bars
            if (_health != null)
            {
                _health.onCurrentHealthChanged.RemoveListener(delegate { SetBarFillAmount(_target.selectionTypeUI.healthBar, _health.maxHealth, _health.currentHealth); SetSihouetteColor(_target, _health.maxHealth, _health.currentHealth); });
                _health.onMaxHealthChanged.RemoveListener(delegate { SetBarFillAmount(_target.selectionTypeUI.healthBar, _health.maxHealth, _health.currentHealth); SetSihouetteColor(_target, _health.maxHealth, _health.currentHealth); });
            }
            if (_energy != null)
            {
                _energy.onCurrentEnergyChanged.RemoveListener(delegate { SetBarFillAmount(_target.selectionTypeUI.energyBar, _energy.maxEnergy, _energy.currentEnergy); });
                _energy.onMaxEnergyChanged.RemoveListener(delegate { SetBarFillAmount(_target.selectionTypeUI.energyBar, _energy.maxEnergy, _energy.currentEnergy); });
            }
        }
    }


    /// <summary>
    /// Sets the fill amount of a speicific bar image.
    /// </summary>
    /// <param name="_image"></param>
    /// <param name="_max"></param>s
    /// <param name="_current"></param>
    private void SetBarFillAmount(Image _image, float _max, float _current)
    {
        _image.fillAmount = _current / _max;
    }


    /// <summary>
    /// Sets the color of the silhouette.
    /// </summary>
    /// <param name="_image"></param>
    /// <param name="_max"></param>s
    /// <param name="_current"></param>
    private void SetSihouetteColor(SelectionTargetComponent _target, float _max, float _current)
    {
        _target.selectionTypeUI.silhouetteRenderer.color = _target.colorGradient.Evaluate(_current / _max);
    }
}
