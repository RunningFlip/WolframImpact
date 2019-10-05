using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    [Header("Info UI Single")]
    //GameObject
    [Space]
    public GameObject singleInformationParent;

    //Label
    [Space]
    public Text nameLabel;
    public Text attackLabel;
    public Text damageTypeLabel;
    public Text healthLabel;
    public Text energieLabel;
    public GameObject enemyIndicator;

    [Header("Info UI Single")]
    public Text damageValue;
    public Text damageTypeValue;
    public Text healthValue;
    public Text energieValue;
    public Text objectCapValue;
    [Space]
    public Image picture;
    public Image healthImage;
    public Image energyImage;


    [Header("Info UI Multiple")]
    //GameObject
    [Space]
    public GameObject multipleInformationParent;

    //Info objects
    public InfoMultipleObject[] infoMultipleObjects;


    [Header("Resource UI")]
    public Text energyStorage;
    public Text energyValue;
    public Text massStorage;
    public Text massValue;


    [Header("Command UI")]
    public Transform commandTransform;
    public CommandButton[] commandButtons;


    //Player index
    private int playerIndex = -1;


    public void InitializeUI(int _playerIndex, InputComponent _inputComponent)
    {
        playerIndex = _playerIndex;

        for (int i = 0; i < commandButtons.Length; i++)
        {
            commandButtons[i].InitializeCommandButton(_inputComponent);
        }
    }



    /// <summary>
    /// Shows single info in the UI.
    /// </summary>
    /// <param name="_status">True if InfoUI should be enabled.</param>
    /// <param name="_informationPackage"></param>
    public void UpdateInfoUI(bool _status, InformationPackage _informationPackage = null)
    {
        if (!_status)
        {
            singleInformationParent.SetActive(false);
            multipleInformationParent.SetActive(false);
        }
        else
        {
            singleInformationParent.SetActive(_status);

            nameLabel.text = _informationPackage.identifierComponent.objectName;
            picture.sprite = _informationPackage.identifierComponent.objectSprite;

            bool isMyObject = _informationPackage.factionComponent.userIndex == playerIndex;
            enemyIndicator.SetActive(!isMyObject);

            damageValue.text = _informationPackage.damage.ToString();
            damageTypeValue.text = _informationPackage.damageType.ToString();

            healthValue.text = _informationPackage.healthComponent.currentHealth + " / " + _informationPackage.healthComponent.maxHealth;
            energieValue.text = _informationPackage.energyComponent.currentEnergy + " / " + _informationPackage.energyComponent.maxEnergy;

            healthImage.fillAmount = _informationPackage.healthComponent.currentHealth / _informationPackage.healthComponent.maxHealth;
            energyImage.fillAmount = _informationPackage.energyComponent.currentEnergy / _informationPackage.energyComponent.maxEnergy;
        }
    }


    /// <summary>
    /// Show multiple infos in the UI.
    /// </summary>
    /// <param name="_status">True if InfoUI should be enabled.</param>
    /// <param name="_informationPackage"></param>
    public void UpdateInfoUI(bool _status, List<InformationPackage> _informationPackages)
    {
        multipleInformationParent.SetActive(_status);

        for (int i = 0; i < infoMultipleObjects.Length; i++)
        {
            if (i < _informationPackages.Count)
            {
                infoMultipleObjects[i].gameObject.SetActive(true);

                InformationPackage infoPackage = _informationPackages[i];

                infoMultipleObjects[i].picture.sprite = infoPackage.identifierComponent.objectSprite;

                infoMultipleObjects[i].healthBar.fillAmount = infoPackage.healthComponent.currentHealth / infoPackage.healthComponent.maxHealth;
                infoMultipleObjects[i].energyBar.fillAmount = infoPackage.energyComponent.currentEnergy / infoPackage.energyComponent.maxEnergy;
            }
            else
            {
                infoMultipleObjects[i].gameObject.SetActive(false);
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="_usedSlots"></param>
    /// <param name="_commandButtons"></param>
    public void UpdateCommandUI(bool _status, CommandTargetComponent _firstCommandTargetComponent)
    {
        if (_status)
        {
            List<Command> commands = _firstCommandTargetComponent.commands;

            for (int i = 0; i < commandButtons.Length; i++)
            {
                if (i < commands.Count)
                {
                    commandButtons[i].ConnectWithActionConfig(commands[i]);
                }
                else
                {
                    commandButtons[i].DisconnectFromActionConfig();
                }
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="_usedSlots"></param>
    /// <param name="_commandButtons"></param>
    public void UpdateCommandUI(bool _status)
    {
        if (!_status)
        {
            for (int i = 0; i < commandButtons.Length; i++)
            {
                commandButtons[i].DisconnectFromActionConfig();
            }
        }
    }



    //Updates Resource Energy UI
    public void UpdateEnergyValueUI(float _energyValue)
    {
        energyValue.text = _energyValue.ToString();
    }
    public void UpdateEnergyStorageUI(float _energyStorage)
    {
        energyStorage.text = _energyStorage.ToString();
    }

    //Updates Resource Mass UI
    public void UpdateMassValueUI(float _massValue)
    {
        massValue.text = _massValue.ToString();
    }
    public void UpdateMassStorageUI(float _massStorage)
    {
        massStorage.text = _massStorage.ToString();
    }

    //Updates Objectcap UI
    public void UpdateObjectCapUI(float _objectCapValue, float _currentObjectSpace)
    {
        objectCapValue.text = _currentObjectSpace + " / " + _objectCapValue;
    }

}
