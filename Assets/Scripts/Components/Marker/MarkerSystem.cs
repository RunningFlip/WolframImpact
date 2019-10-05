using System.Collections.Generic;


public class MarkerSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(MarkerComponent), typeof(SelectionTargetComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            int entityID = entities[i].GetEntityID();
            MarkerComponent markerComponent = ComponentManager.GetComponent<MarkerComponent>(entityID);

            if (!markerComponent.init)
            {
                Entity entity = entities[i];

                SelectionTargetComponent selectionTargetComponent = ComponentManager.GetComponent<SelectionTargetComponent>(entityID);
                
                //Marker
                markerComponent.init = true;
                markerComponent.markerObject = entity.entityGameObject.GetComponentInChildren<MarkerLink>().markerObject;
                markerComponent.markerTransform = markerComponent.markerObject.transform;

                //Events
                selectionTargetComponent.onSelectionStarts.AddListener(delegate { SetMarkerStatus(markerComponent, true); });
                selectionTargetComponent.onSelectionEnds.AddListener(delegate { SetMarkerStatus(markerComponent, false); });
            }
        }
    }


    /// <summary>
    /// Actovates or deactivates the visibility of the marker.
    /// </summary>
    /// <param name="_status"></param>
    private void SetMarkerStatus(MarkerComponent _markerComponent, bool _status)
    {
        if (_markerComponent.firstSetted)
        {
            _markerComponent.markerObject.SetActive(_status);
        }
    }
}
