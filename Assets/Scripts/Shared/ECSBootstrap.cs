using System.Collections.Generic;
using UnityEngine;


public class ECSBootstrap : MonoBehaviour
{
    //Managers
    private EntityManager entityManager;
    private ComponentManager componentManager;
    private SystemManager systemManager;


    private void Awake()
    {
        entityManager = new EntityManager();
        componentManager = new ComponentManager();
        systemManager = new SystemManager();

        Debug.Log("ECS Bootstrap started! All managers were created!");
    }


    private void Start()
    {

    }


    private void Update()
    {
        systemManager.SystemUpdate();
    }
}
