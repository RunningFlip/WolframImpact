// GENERATED AUTOMATICALLY FROM 'Assets/Assets/Input/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputMaster : IInputActionCollection
{
    private InputActionAsset asset;
    public InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""MainMenu"",
            ""id"": ""bdab6327-b8f8-411e-af06-9be268dfe5ea"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""id"": ""cb98b2aa-ab7f-481f-b28f-95815f980d41"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""172aae3e-b49b-42f8-a02d-c9affb7d0e63"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                }
            ]
        },
        {
            ""name"": ""Player"",
            ""id"": ""b2bad0fa-c789-420e-8e12-fede43145b58"",
            ""actions"": [
                {
                    ""name"": ""Selection"",
                    ""id"": ""0e9a48be-eb77-47f5-91d1-a72bd47a7716"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Command"",
                    ""id"": ""3f10dc2c-137e-495b-bded-d0bbf1b7476c"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""ContinueAction"",
                    ""id"": ""3cf9584e-2c14-43ef-b282-40ed6195fbed"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""CameraMoveVertical"",
                    ""id"": ""26197ad1-d258-41ff-9115-2be6031e22ab"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""CameraMoveHorizontal"",
                    ""id"": ""29625686-3d1d-4cdd-a77c-eac37e0f695b"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""CameraZoom"",
                    ""id"": ""9c54c2d4-fc63-4829-8610-64ecc0badce6"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""ContextMenu"",
                    ""id"": ""2c7db47a-0679-4463-b2b0-8bb94b86dbe8"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""bindings"": []
                },
                {
                    ""name"": ""Cancel"",
                    ""id"": ""4962c653-edac-4f0c-9fab-45100b6f868b"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""bindings"": []
                },
                {
                    ""name"": ""Destroy"",
                    ""id"": ""b0b608a7-22e0-4cf1-9a59-8b948c643048"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""bindings"": []
                },
                {
                    ""name"": ""LockCamera"",
                    ""id"": ""42597b90-2cd4-49f1-93b2-ba1c045042cc"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""bindings"": []
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4acc8e44-276e-4f79-a52c-5410a5c7696f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Selection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""a947532b-539f-4835-a9ea-282181e340a9"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Command"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""id"": ""579c1384-f191-400e-a96f-b0b920bfa3a2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press"",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""groups"": """",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""a55a1e9c-cfcb-4c77-80ee-9c02ba679538"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""7887da31-338b-4125-bbde-9f3331497518"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""5a592561-a653-4c2d-bf54-cc4b3bbcdbab"",
                    ""path"": ""<Keyboard>/f12"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""ContextMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""ba57edec-0be3-43fe-b402-0b5f8e4d505a"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""ContinueAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""WASD Vertical"",
                    ""id"": ""6f5b2dfc-3365-45dd-8073-90418091e4dd"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMoveVertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""47bd993c-847a-4b3e-88c7-b9ca4e7defcd"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraMoveVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""2938a910-12b8-4498-b9b3-9c2d3f234494"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraMoveVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""ArrowKeys Vertical"",
                    ""id"": ""424b8859-8fad-41a2-b887-49d80c99aa69"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMoveVertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""56fcf7cc-f122-4b12-9919-49a0ba7c754e"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraMoveVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""7b283575-c939-4116-9adf-565fa0b60e1b"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraMoveVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""699ecd64-bbd1-4c55-8799-f06cb545889b"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""348c0f8c-f5b0-46c7-b674-d9b27ade225c"",
                    ""path"": ""<Keyboard>/delete"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Destroy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""WASD Horizontal"",
                    ""id"": ""46ba52c7-fa9a-438a-aec1-eb6029725857"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraMoveHorizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""d29c365c-397b-452f-8557-6a348920f200"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraMoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""dff0532d-c56e-4997-a9f5-fe839b436084"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraMoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""ArrowKeys Horizontal"",
                    ""id"": ""7ed3a3ac-adc0-4837-b5e3-b1d287028e25"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraMoveHorizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""5370d8a9-6784-4c5b-bdbf-e7422a3fa93e"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraMoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""417b9770-7a41-4ba8-a3d9-734834a816b0"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraMoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""a0cc26bb-ced7-4119-a650-427804769816"",
                    ""path"": ""<Keyboard>/leftAlt"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""LockCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                }
            ]
        },
        {
            ""name"": ""ContextMenu"",
            ""id"": ""d806a478-5c5f-47ef-84fa-e100b1ca6d6e"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""id"": ""e4dc3c24-6200-48a4-9f6b-c6c61852c3e1"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""90e2d051-91b9-4a20-b6cd-06d0ba104d35"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""basedOn"": """",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""basedOn"": """",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<XboxOneGamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<PS4DualShockGamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // MainMenu
        m_MainMenu = asset.GetActionMap("MainMenu");
        m_MainMenu_Newaction = m_MainMenu.GetAction("New action");
        // Player
        m_Player = asset.GetActionMap("Player");
        m_Player_Selection = m_Player.GetAction("Selection");
        m_Player_Command = m_Player.GetAction("Command");
        m_Player_ContinueAction = m_Player.GetAction("ContinueAction");
        m_Player_CameraMoveVertical = m_Player.GetAction("CameraMoveVertical");
        m_Player_CameraMoveHorizontal = m_Player.GetAction("CameraMoveHorizontal");
        m_Player_CameraZoom = m_Player.GetAction("CameraZoom");
        m_Player_ContextMenu = m_Player.GetAction("ContextMenu");
        m_Player_Cancel = m_Player.GetAction("Cancel");
        m_Player_Destroy = m_Player.GetAction("Destroy");
        m_Player_LockCamera = m_Player.GetAction("LockCamera");
        // ContextMenu
        m_ContextMenu = asset.GetActionMap("ContextMenu");
        m_ContextMenu_Newaction = m_ContextMenu.GetAction("New action");
    }

    ~InputMaster()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes
    {
        get => asset.controlSchemes;
    }

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // MainMenu
    private InputActionMap m_MainMenu;
    private IMainMenuActions m_MainMenuActionsCallbackInterface;
    private InputAction m_MainMenu_Newaction;
    public struct MainMenuActions
    {
        private InputMaster m_Wrapper;
        public MainMenuActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction { get { return m_Wrapper.m_MainMenu_Newaction; } }
        public InputActionMap Get() { return m_Wrapper.m_MainMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(MainMenuActions set) { return set.Get(); }
        public void SetCallbacks(IMainMenuActions instance)
        {
            if (m_Wrapper.m_MainMenuActionsCallbackInterface != null)
            {
                Newaction.started -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnNewaction;
                Newaction.performed -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnNewaction;
                Newaction.canceled -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_MainMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                Newaction.started += instance.OnNewaction;
                Newaction.performed += instance.OnNewaction;
                Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public MainMenuActions @MainMenu
    {
        get
        {
            return new MainMenuActions(this);
        }
    }

    // Player
    private InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private InputAction m_Player_Selection;
    private InputAction m_Player_Command;
    private InputAction m_Player_ContinueAction;
    private InputAction m_Player_CameraMoveVertical;
    private InputAction m_Player_CameraMoveHorizontal;
    private InputAction m_Player_CameraZoom;
    private InputAction m_Player_ContextMenu;
    private InputAction m_Player_Cancel;
    private InputAction m_Player_Destroy;
    private InputAction m_Player_LockCamera;
    public struct PlayerActions
    {
        private InputMaster m_Wrapper;
        public PlayerActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Selection { get { return m_Wrapper.m_Player_Selection; } }
        public InputAction @Command { get { return m_Wrapper.m_Player_Command; } }
        public InputAction @ContinueAction { get { return m_Wrapper.m_Player_ContinueAction; } }
        public InputAction @CameraMoveVertical { get { return m_Wrapper.m_Player_CameraMoveVertical; } }
        public InputAction @CameraMoveHorizontal { get { return m_Wrapper.m_Player_CameraMoveHorizontal; } }
        public InputAction @CameraZoom { get { return m_Wrapper.m_Player_CameraZoom; } }
        public InputAction @ContextMenu { get { return m_Wrapper.m_Player_ContextMenu; } }
        public InputAction @Cancel { get { return m_Wrapper.m_Player_Cancel; } }
        public InputAction @Destroy { get { return m_Wrapper.m_Player_Destroy; } }
        public InputAction @LockCamera { get { return m_Wrapper.m_Player_LockCamera; } }
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                Selection.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelection;
                Selection.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelection;
                Selection.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelection;
                Command.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCommand;
                Command.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCommand;
                Command.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCommand;
                ContinueAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnContinueAction;
                ContinueAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnContinueAction;
                ContinueAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnContinueAction;
                CameraMoveVertical.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraMoveVertical;
                CameraMoveVertical.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraMoveVertical;
                CameraMoveVertical.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraMoveVertical;
                CameraMoveHorizontal.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraMoveHorizontal;
                CameraMoveHorizontal.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraMoveHorizontal;
                CameraMoveHorizontal.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraMoveHorizontal;
                CameraZoom.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraZoom;
                CameraZoom.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraZoom;
                CameraZoom.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraZoom;
                ContextMenu.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnContextMenu;
                ContextMenu.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnContextMenu;
                ContextMenu.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnContextMenu;
                Cancel.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCancel;
                Cancel.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCancel;
                Cancel.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCancel;
                Destroy.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDestroy;
                Destroy.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDestroy;
                Destroy.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDestroy;
                LockCamera.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLockCamera;
                LockCamera.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLockCamera;
                LockCamera.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLockCamera;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                Selection.started += instance.OnSelection;
                Selection.performed += instance.OnSelection;
                Selection.canceled += instance.OnSelection;
                Command.started += instance.OnCommand;
                Command.performed += instance.OnCommand;
                Command.canceled += instance.OnCommand;
                ContinueAction.started += instance.OnContinueAction;
                ContinueAction.performed += instance.OnContinueAction;
                ContinueAction.canceled += instance.OnContinueAction;
                CameraMoveVertical.started += instance.OnCameraMoveVertical;
                CameraMoveVertical.performed += instance.OnCameraMoveVertical;
                CameraMoveVertical.canceled += instance.OnCameraMoveVertical;
                CameraMoveHorizontal.started += instance.OnCameraMoveHorizontal;
                CameraMoveHorizontal.performed += instance.OnCameraMoveHorizontal;
                CameraMoveHorizontal.canceled += instance.OnCameraMoveHorizontal;
                CameraZoom.started += instance.OnCameraZoom;
                CameraZoom.performed += instance.OnCameraZoom;
                CameraZoom.canceled += instance.OnCameraZoom;
                ContextMenu.started += instance.OnContextMenu;
                ContextMenu.performed += instance.OnContextMenu;
                ContextMenu.canceled += instance.OnContextMenu;
                Cancel.started += instance.OnCancel;
                Cancel.performed += instance.OnCancel;
                Cancel.canceled += instance.OnCancel;
                Destroy.started += instance.OnDestroy;
                Destroy.performed += instance.OnDestroy;
                Destroy.canceled += instance.OnDestroy;
                LockCamera.started += instance.OnLockCamera;
                LockCamera.performed += instance.OnLockCamera;
                LockCamera.canceled += instance.OnLockCamera;
            }
        }
    }
    public PlayerActions @Player
    {
        get
        {
            return new PlayerActions(this);
        }
    }

    // ContextMenu
    private InputActionMap m_ContextMenu;
    private IContextMenuActions m_ContextMenuActionsCallbackInterface;
    private InputAction m_ContextMenu_Newaction;
    public struct ContextMenuActions
    {
        private InputMaster m_Wrapper;
        public ContextMenuActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction { get { return m_Wrapper.m_ContextMenu_Newaction; } }
        public InputActionMap Get() { return m_Wrapper.m_ContextMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(ContextMenuActions set) { return set.Get(); }
        public void SetCallbacks(IContextMenuActions instance)
        {
            if (m_Wrapper.m_ContextMenuActionsCallbackInterface != null)
            {
                Newaction.started -= m_Wrapper.m_ContextMenuActionsCallbackInterface.OnNewaction;
                Newaction.performed -= m_Wrapper.m_ContextMenuActionsCallbackInterface.OnNewaction;
                Newaction.canceled -= m_Wrapper.m_ContextMenuActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_ContextMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                Newaction.started += instance.OnNewaction;
                Newaction.performed += instance.OnNewaction;
                Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public ContextMenuActions @ContextMenu
    {
        get
        {
            return new ContextMenuActions(this);
        }
    }
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.GetControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.GetControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IMainMenuActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
    public interface IPlayerActions
    {
        void OnSelection(InputAction.CallbackContext context);
        void OnCommand(InputAction.CallbackContext context);
        void OnContinueAction(InputAction.CallbackContext context);
        void OnCameraMoveVertical(InputAction.CallbackContext context);
        void OnCameraMoveHorizontal(InputAction.CallbackContext context);
        void OnCameraZoom(InputAction.CallbackContext context);
        void OnContextMenu(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnDestroy(InputAction.CallbackContext context);
        void OnLockCamera(InputAction.CallbackContext context);
    }
    public interface IContextMenuActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
}
