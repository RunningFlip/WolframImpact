using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MiniMapSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(MiniMapComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            int entityID = entities[i].GetEntityID();

            MiniMapComponent miniMapComponent = ComponentManager.GetComponent<MiniMapComponent>(entityID);

            if (!miniMapComponent.init)
            {
                miniMapComponent.init = true;
                Entity entity = entities[i];
                MiniMapSetup(miniMapComponent, entity);
            }

            CalculateMapFrustum(miniMapComponent);
        }
    }


    /// <summary>
    /// Setups all neccessary proerties of the given minimap component.
    /// </summary>
    /// <param name="_miniMap"></param>
    private void MiniMapSetup(MiniMapComponent _miniMap, Entity _entity)
    {
        //Links
        _miniMap.miniMapLink = _entity.entityGameObject.GetComponentInChildren<MiniMapLink>();
        _miniMap.rendererLink = _entity.entityGameObject.GetComponentInChildren<MiniMapRendererLink>();

        //Buttons
        _miniMap.minimapButton = _miniMap.miniMapLink.minimapImage.GetComponent<Button>();
        _miniMap.minimapButton.onClick.AddListener(delegate { OnMinimapClick(_miniMap); });

        //Vectors
        _miniMap.minimapOrigin = _miniMap.miniMapLink.minimapImage.transform.position;
        _miniMap.screenSize = new Vector3(Screen.width / 2, Screen.height / 2);

        //Transforms
        _miniMap.gameplayCamera = Camera.main;
        _miniMap.gameplayCamTrans = _miniMap.gameplayCamera.transform;

        //Minimap camera size init
        _miniMap.miniMapLink.minimapCamera.orthographicSize = (GameController.Instance.map.mapGroundCollider.transform.localScale.x / 2);
        if (!_miniMap.miniMapLink.minimapCamera.allowHDR)
        {
            Debug.LogWarning("HDR is not active! The frustum borders will not be shown in the minimap! You have to enable HDR!");
        }

        //RayCasts
        GameParameter gameParameter = GameController.Instance.GameParameter;
        _miniMap.rayCastLength = gameParameter.minimapFrustumRayCastLength;
        _miniMap.minimapMask = gameParameter.minimapLayerMask;
    }


    /// <summary>
    /// Updates the gameplay cam, after the minimap was clicked.
    /// </summary>
    private void OnMinimapClick(MiniMapComponent _miniMap)
    {
        _miniMap.mousePosition = Input.mousePosition;

        float size = (_miniMap.miniMapLink.minimapImage.rectTransform.rect.width * _miniMap.miniMapLink.canvas.scaleFactor) / 2;

        float minimapX = ((_miniMap.mousePosition.x - _miniMap.minimapOrigin.x) * 100f) / size;
        float minimapY = ((_miniMap.mousePosition.y - _miniMap.minimapOrigin.y) * 100f) / size;

        float worldX = ((GameController.Instance.map.mapGroundCollider.bounds.size.x / 2f) * minimapX) / 100f;
        float worldZ = ((GameController.Instance.map.mapGroundCollider.bounds.size.z / 2f) * minimapY) / 100f;

        float sqrLen = 0;

        RaycastHit hit;

        if (Physics.Raycast(_miniMap.gameplayCamera.ScreenPointToRay(_miniMap.screenSize), out hit, _miniMap.rayCastLength, _miniMap.minimapMask))
        {
            _miniMap.camPosOnLevelBackground = new Vector3(_miniMap.gameplayCamTrans.localPosition.x, GameController.Instance.map.mapBackgroundCollider.transform.position.y, _miniMap.gameplayCamTrans.localPosition.z);
            sqrLen = Mathf.Pow((hit.point - _miniMap.camPosOnLevelBackground).sqrMagnitude, 0.5f);
        }
        _miniMap.gameplayCamTrans.localPosition = new Vector3(worldX, _miniMap.gameplayCamTrans.localPosition.y, worldZ - sqrLen);
    }



    private void CalculateMapFrustum(MiniMapComponent _miniMap)
    {
        _miniMap.topLeftCorner = _miniMap.gameplayCamera.ScreenPointToRay(new Vector3(0f, 0f));
        _miniMap.topRightCorner = _miniMap.gameplayCamera.ScreenPointToRay(new Vector3(Screen.width, 0f));
        _miniMap.bottomLeftCorner = _miniMap.gameplayCamera.ScreenPointToRay(new Vector3(0, Screen.height));
        _miniMap.bottomRightCorner = _miniMap.gameplayCamera.ScreenPointToRay(new Vector3(Screen.width, Screen.height));


        if (Physics.Raycast(_miniMap.topLeftCorner, out _miniMap.hits[0], _miniMap.rayCastLength, _miniMap.minimapMask))
        {
            _miniMap.rendererLink.topLeftPosition = _miniMap.hits[0].point;
        }
        if (Physics.Raycast(_miniMap.topRightCorner, out _miniMap.hits[1], _miniMap.rayCastLength, _miniMap.minimapMask))
        {
            _miniMap.rendererLink.topRightPosition = _miniMap.hits[1].point;
        }
        if (Physics.Raycast(_miniMap.bottomLeftCorner, out _miniMap.hits[2], _miniMap.rayCastLength, _miniMap.minimapMask))
        {
            _miniMap.rendererLink.bottomLeftPosition = _miniMap.hits[2].point;
        }
        if (Physics.Raycast(_miniMap.bottomRightCorner, out _miniMap.hits[3], _miniMap.rayCastLength, _miniMap.minimapMask))
        {
            _miniMap.rendererLink.bottomRightPosition = _miniMap.hits[3].point;
        }

        _miniMap.rendererLink.topLeftPosition = _miniMap.miniMapLink.minimapCamera.WorldToViewportPoint(_miniMap.rendererLink.topLeftPosition);
        _miniMap.rendererLink.topRightPosition = _miniMap.miniMapLink.minimapCamera.WorldToViewportPoint(_miniMap.rendererLink.topRightPosition);
        _miniMap.rendererLink.bottomLeftPosition = _miniMap.miniMapLink.minimapCamera.WorldToViewportPoint(_miniMap.rendererLink.bottomLeftPosition);
        _miniMap.rendererLink.bottomRightPosition = _miniMap.miniMapLink.minimapCamera.WorldToViewportPoint(_miniMap.rendererLink.bottomRightPosition);

        _miniMap.rendererLink.topLeftPosition.z = -1f;
        _miniMap.rendererLink.topRightPosition.z = -1f;
        _miniMap.rendererLink.bottomLeftPosition.z = -1f;
        _miniMap.rendererLink.bottomRightPosition.z = -1f;
    }
}
