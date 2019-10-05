using System.Collections.Generic;
using UnityEngine;


public class CameraSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(CameraComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            int entityID = entities[i].GetEntityID();

            CameraComponent cameraComponent = ComponentManager.GetComponent<CameraComponent>(entityID);
            
            if (!cameraComponent.init)
            {
                //Setup for all camera components
                cameraComponent.init = true;
                CameraSetup(entities[i], cameraComponent);
                ResetZoom(cameraComponent);
            }
            else
            {
                //Action for all camera components with main tag.
                entities = entities.ForEach(typeof(MainCameraTagComponent));

                if (!cameraComponent.useCameraMovement) continue;

                //Movement
                CheckBorderTrigger(cameraComponent);                            //Mouse     -> first priority
                if (cameraComponent.movement != cameraComponent.nullVector)     //Keyboard  -> second priority
                {
                    Move(cameraComponent);
                }

                //Rotation
                if (cameraComponent.rotation != 0f) Rotate(cameraComponent);
                if (cameraComponent.resetRotation)
                {
                    cameraComponent.resetRotation = false;
                    cameraComponent.cameraTransform.localRotation = cameraComponent.resetQuaternion;
                }

                //Zoom
                if (cameraComponent.zoom != 0f) Zoom(cameraComponent);
                if (cameraComponent.resetZoom)
                {
                    cameraComponent.resetZoom = false;
                    ResetZoom(cameraComponent);
                }
            }
        }
    }


    /// <summary>
    /// Moves the camera
    /// </summary>
    private void Move(CameraComponent _cameraComponent)
    {
        //if (!CheckXIsValid()) movement.x = 0;
        //if (!CheckZIsValid()) movement.z = 0;

        _cameraComponent.desireMove = _cameraComponent.movement.normalized * _cameraComponent.moveSpeed * Time.deltaTime;
        _cameraComponent.dir = new Vector3(0f, _cameraComponent.cameraTransform.eulerAngles.y, 0f);
        _cameraComponent.desireMove = Quaternion.Euler(_cameraComponent.dir) * _cameraComponent.desireMove;

        _cameraComponent.cameraTransform.localPosition += _cameraComponent.desireMove;
        _cameraComponent.movement = _cameraComponent.nullVector; //Reset current movement
    }


    /// <summary>
    /// If mouse enters the cameraboarder trigger, the camera will move in the direction
    /// </summary>
    private void CheckBorderTrigger(CameraComponent _cameraComponent)
    {
        Vector3 mousePosition = Input.mousePosition;

        int viewportWidthX = _cameraComponent.cam.pixelWidth;
        int viewportWidthY = _cameraComponent.cam.pixelHeight;

        // X
        if (mousePosition.x < _cameraComponent.borderTriggerSize) _cameraComponent.movement.x = -_cameraComponent.triggerMovementStep;
        else if (mousePosition.x > viewportWidthX - _cameraComponent.borderTriggerSize) _cameraComponent.movement.x = _cameraComponent.triggerMovementStep;


        // Y
        if (mousePosition.y < _cameraComponent.borderTriggerSize) _cameraComponent.movement.z = -_cameraComponent.triggerMovementStep;
        else if (mousePosition.y > viewportWidthY - _cameraComponent.borderTriggerSize) _cameraComponent.movement.z = _cameraComponent.triggerMovementStep;
    }


    /// <summary>
    /// Rotates the camera around it's center in a specific direction.
    /// </summary>
    private void Rotate(CameraComponent _cameraComponent)
    {
        _cameraComponent.cameraTransform.Rotate(Vector3.up, _cameraComponent.rotation * _cameraComponent.rotationSpeed * 100f * Time.deltaTime, Space.World);

        _cameraComponent.rotation = 0f;
    }


    /// <summary>
    /// Zooms the camera
    /// </summary>
    private void Zoom(CameraComponent _cameraComponent)
    {
        _cameraComponent.zoomVec = _cameraComponent.cameraTransform.localPosition;

        _cameraComponent.zoomVec.y += _cameraComponent.zoom * _cameraComponent.zoomSpeed * 100f * Time.deltaTime;
        _cameraComponent.zoomVec.y = Mathf.Clamp(_cameraComponent.zoomVec.y, _cameraComponent.minZoom, _cameraComponent.maxZoom);

        _cameraComponent.cameraTransform.localPosition = _cameraComponent.zoomVec;

        _cameraComponent.zoom = 0f;
    }


    /// <summary>
    /// Resets the zoom value to the default size.
    /// </summary>
    private void ResetZoom(CameraComponent _cameraComponent)
    {
        _cameraComponent.resetVector = new Vector3(_cameraComponent.cameraTransform.position.x, _cameraComponent.defaultZoom, _cameraComponent.cameraTransform.position.z);
        _cameraComponent.cameraTransform.position = _cameraComponent.resetVector;
    }


    /// <summary>
    /// Setups all values of the camera component that is needed.
    /// </summary>
    /// <param name="_cameraComponent"></param>
    private void CameraSetup(Entity _entity, CameraComponent _cameraComponent)
    {
        //Gameparameter
        GameParameter gameParameter = GameController.Instance.GameParameter;

        //Camera
        CameraLink cameraLink = Object.FindObjectOfType<CameraLink>();
        _cameraComponent.cam = cameraLink.cam;
        _cameraComponent.cameraTransform = cameraLink.cameraMovementTransform;

        //Movement values
        _cameraComponent.moveSpeed = gameParameter.cameraSpeed;
        _cameraComponent.borderTriggerSize = gameParameter.cameraTriggerSize;
        _cameraComponent.triggerMovementStep = gameParameter.cameraTriggerMovementStep;

        //Rotation values
        _cameraComponent.rotationSpeed = gameParameter.cameraRotationSpeed;
        _cameraComponent.resetQuaternion = _cameraComponent.cameraTransform.localRotation;

        //Zoom values
        _cameraComponent.defaultZoom = gameParameter.cameraDefaultZoom;
        _cameraComponent.minZoom = gameParameter.cameraMinZoom;
        _cameraComponent.maxZoom = gameParameter.cameraMaxZoom;
        _cameraComponent.zoomSpeed = gameParameter.cameraZoomSpeed;
    }
}
