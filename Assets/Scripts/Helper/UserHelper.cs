using System.Collections;
using UnityEngine;


public static class UserHelper
{
    /// <summary>
    /// Spawns the base at the spawnpoint and instantiates the construction workers
    /// </summary>
    public static IEnumerator SpawnBase(Map _map, UserComponent _userComponent, ResourceComponent _resouceComponent, int _userIndex)
    {
        //Component init
        GameParameter parameter = GameController.Instance.GameParameter;

        //Base spawn Vec
        Vector3 baseSpawnVector = _map.factionSpawnPoints[_userIndex].position;

        //Base spawn
        GameObject baseObject = GameObject.Instantiate(parameter.baseConfig.prefab, baseSpawnVector, Quaternion.identity);
        int baseEntityId = baseObject.GetComponent<GameObjectEntityHolder>().GetEntityID();

        _userComponent.factionEntities.Add(baseEntityId);

        yield return new WaitUntil(() => ComponentManager.ComponentReady<InformationComponent>(baseEntityId));

        //Base infopackage
        InformationComponent baseInfo = ComponentManager.GetComponent<InformationComponent>(baseEntityId);
        baseInfo.informationPackage.factionComponent.userIndex = _userIndex;
        baseInfo.informationPackage.identifierComponent.objectName = parameter.baseConfig.prefabName;
        baseInfo.informationPackage.identifierComponent.objectSprite = parameter.baseConfig.picture;
        baseInfo.informationPackage.typeComponent.isMilitaryObject = parameter.baseConfig.isMilitaryObject;       

        //CoWorker spawn value init
        int coWorkerAmount = parameter.coWorkerAmount;

        //CoWorker spawns
        for (int i = 0; i < coWorkerAmount; i++)
        {
            float angle = i * Mathf.PI * 2 / coWorkerAmount;

            Vector3 spawnVec = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * parameter.coWorkerSpawnRadius;

            GameObject coWorker = GameObject.Instantiate(parameter.coWorkerConfig.prefab, baseSpawnVector + spawnVec, Quaternion.Euler(0f, 180f, 0f));
            int coWorkerEntityId = coWorker.GetComponent<GameObjectEntityHolder>().GetEntityID();

            yield return new WaitUntil(() => ComponentManager.ComponentReady<InformationComponent>(coWorkerEntityId));

            _userComponent.factionEntities.Add(coWorkerEntityId);

            //CoWorker infopackage
            InformationComponent coWorkerInfo = ComponentManager.GetComponent<InformationComponent>(coWorkerEntityId);
            coWorkerInfo.informationPackage.factionComponent.userIndex = _userIndex;
            coWorkerInfo.informationPackage.identifierComponent.objectName = parameter.coWorkerConfig.prefabName;
            coWorkerInfo.informationPackage.identifierComponent.objectSprite = parameter.coWorkerConfig.picture;
            coWorkerInfo.informationPackage.typeComponent.isMilitaryObject = parameter.coWorkerConfig.isMilitaryObject;

            //Resources
            _resouceComponent.currentObjectSpace += parameter.coWorkerConfig.requiredObjectSpace;
        }
    }


    /// <summary>
    /// Remove object from list and check, if the objectList is empty, the controller is not alive.
    /// </summary>
    /// <param name="_object"></param>
    public static void RemoveObject(int _userIndex, int _entityIdToRemove)
    {
        int userEntityId = GameController.Instance.GetUserEntitId(_userIndex);
        UserComponent user = ComponentManager.GetComponent<UserComponent>(userEntityId);

        user.factionEntities.Remove(_entityIdToRemove);

        if (user.factionEntities.Count == 0)
        {
            user.alive = false;
            GameController.Instance.KillUser();
        }
    }
}
