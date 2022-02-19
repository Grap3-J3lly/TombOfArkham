//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/UI/ControlLayout.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @ControlLayout : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ControlLayout()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ControlLayout"",
    ""maps"": [
        {
            ""name"": ""Basics"",
            ""id"": ""03afed85-c168-493d-8ee3-e232f6283328"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""be05ecce-f526-47cc-b1d4-15c2f55941c6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""b888ebf4-1a70-490b-856d-4f8101d2fb63"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""5a6e0d7e-cee3-4349-8b3f-5a09164f334d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2c97fb82-1c15-4fd4-8860-b2d64120d43b"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PrimaryControlLayout"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard Keys"",
                    ""id"": ""168a2126-1111-475b-8118-cbece8f4ebb1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b734a0e5-8ea9-43fd-9a94-9220995fc37e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PrimaryControlLayout"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c1bc8331-94d9-4cdd-b4b7-b1a741dc79fd"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PrimaryControlLayout"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f354d1f4-df33-4720-a9cc-a93f3b3eb393"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PrimaryControlLayout"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f79a017c-e033-4c58-ae9c-816dc21fed6c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PrimaryControlLayout"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9ec2d86d-3217-431e-bc1b-929af549e23b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PrimaryControlLayout"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d07a41de-d4d4-44e0-84c4-ec81092d36dc"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PrimaryControlLayout"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menus"",
            ""id"": ""46ad811f-bc09-4315-980f-95a4ecf72c87"",
            ""actions"": [
                {
                    ""name"": ""PlayLevel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f3e040c5-9a37-4326-8ee5-8df32cdfb69e"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""NextScreen"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cc3e86ea-fed2-48da-a8bb-619e2fe71de2"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RestartLevel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f652d959-821f-4b0a-bcf9-d7e03c001764"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleMusic"",
                    ""type"": ""PassThrough"",
                    ""id"": ""64248c49-fe3a-41b7-a4dc-947bc635d92f"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleSound"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8e47b053-5cb6-4d74-a021-613ebfc733eb"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChangeGameVolume"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9c3b3794-068e-46fe-8d71-088e4f111cf8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChangeMusicVolume"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cedcc405-63b0-41aa-b590-fdbad8501cdf"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChangeSoundVolume"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4ea0fc18-dc49-4db1-8f7c-024537c0f125"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""PassThrough"",
                    ""id"": ""23a8179f-bad4-4183-933b-d5a734c2dd4c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Unpause"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9d667094-bbb1-4bae-a226-4930bf6d26fe"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleButtonInversion"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b30a5f59-d8a7-4200-8d82-fa0f7b6e7914"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9481db81-50ff-4f89-b7c7-5a91b3956e0e"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PrimaryControlLayout"",
                    ""action"": ""PlayLevel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6bcf7bd5-53e5-44c3-8090-9f4e348f5278"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PrimaryControlLayout"",
                    ""action"": ""NextScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20c0905d-d73e-4085-ab8f-d73a4c6ba538"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PrimaryControlLayout"",
                    ""action"": ""RestartLevel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d9b76b4-17df-4aef-851b-b3fb763412f6"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PrimaryControlLayout"",
                    ""action"": ""ToggleMusic"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50f4eef7-b0b3-4bc2-9eeb-2c0f14feee65"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PrimaryControlLayout"",
                    ""action"": ""ToggleSound"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08800061-e6ee-4ee9-b2a1-d76e0583cdfc"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PrimaryControlLayout"",
                    ""action"": ""ChangeGameVolume"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5bf264e1-8b67-49e3-a1b4-f60b947b10ef"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PrimaryControlLayout"",
                    ""action"": ""ChangeMusicVolume"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6f792d8-be1e-45de-bab9-a5a8e453c4b2"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PrimaryControlLayout"",
                    ""action"": ""ChangeSoundVolume"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90b5586f-77eb-4d32-ac89-832042e35447"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PrimaryControlLayout"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e52e894f-c352-4a54-b727-a4b2f6422403"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PrimaryControlLayout"",
                    ""action"": ""Unpause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53c74f25-ffc1-4e6c-aef2-db024cbcf07c"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PrimaryControlLayout"",
                    ""action"": ""ToggleButtonInversion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PrimaryControlLayout"",
            ""bindingGroup"": ""PrimaryControlLayout"",
            ""devices"": [
                {
                    ""devicePath"": ""<AndroidGamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<AndroidJoystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Basics
        m_Basics = asset.FindActionMap("Basics", throwIfNotFound: true);
        m_Basics_Move = m_Basics.FindAction("Move", throwIfNotFound: true);
        m_Basics_Jump = m_Basics.FindAction("Jump", throwIfNotFound: true);
        m_Basics_Attack = m_Basics.FindAction("Attack", throwIfNotFound: true);
        // Menus
        m_Menus = asset.FindActionMap("Menus", throwIfNotFound: true);
        m_Menus_PlayLevel = m_Menus.FindAction("PlayLevel", throwIfNotFound: true);
        m_Menus_NextScreen = m_Menus.FindAction("NextScreen", throwIfNotFound: true);
        m_Menus_RestartLevel = m_Menus.FindAction("RestartLevel", throwIfNotFound: true);
        m_Menus_ToggleMusic = m_Menus.FindAction("ToggleMusic", throwIfNotFound: true);
        m_Menus_ToggleSound = m_Menus.FindAction("ToggleSound", throwIfNotFound: true);
        m_Menus_ChangeGameVolume = m_Menus.FindAction("ChangeGameVolume", throwIfNotFound: true);
        m_Menus_ChangeMusicVolume = m_Menus.FindAction("ChangeMusicVolume", throwIfNotFound: true);
        m_Menus_ChangeSoundVolume = m_Menus.FindAction("ChangeSoundVolume", throwIfNotFound: true);
        m_Menus_Pause = m_Menus.FindAction("Pause", throwIfNotFound: true);
        m_Menus_Unpause = m_Menus.FindAction("Unpause", throwIfNotFound: true);
        m_Menus_ToggleButtonInversion = m_Menus.FindAction("ToggleButtonInversion", throwIfNotFound: true);
    }

    public void Dispose()
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

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Basics
    private readonly InputActionMap m_Basics;
    private IBasicsActions m_BasicsActionsCallbackInterface;
    private readonly InputAction m_Basics_Move;
    private readonly InputAction m_Basics_Jump;
    private readonly InputAction m_Basics_Attack;
    public struct BasicsActions
    {
        private @ControlLayout m_Wrapper;
        public BasicsActions(@ControlLayout wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Basics_Move;
        public InputAction @Jump => m_Wrapper.m_Basics_Jump;
        public InputAction @Attack => m_Wrapper.m_Basics_Attack;
        public InputActionMap Get() { return m_Wrapper.m_Basics; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BasicsActions set) { return set.Get(); }
        public void SetCallbacks(IBasicsActions instance)
        {
            if (m_Wrapper.m_BasicsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_BasicsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_BasicsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_BasicsActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_BasicsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_BasicsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_BasicsActionsCallbackInterface.OnJump;
                @Attack.started -= m_Wrapper.m_BasicsActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_BasicsActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_BasicsActionsCallbackInterface.OnAttack;
            }
            m_Wrapper.m_BasicsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
            }
        }
    }
    public BasicsActions @Basics => new BasicsActions(this);

    // Menus
    private readonly InputActionMap m_Menus;
    private IMenusActions m_MenusActionsCallbackInterface;
    private readonly InputAction m_Menus_PlayLevel;
    private readonly InputAction m_Menus_NextScreen;
    private readonly InputAction m_Menus_RestartLevel;
    private readonly InputAction m_Menus_ToggleMusic;
    private readonly InputAction m_Menus_ToggleSound;
    private readonly InputAction m_Menus_ChangeGameVolume;
    private readonly InputAction m_Menus_ChangeMusicVolume;
    private readonly InputAction m_Menus_ChangeSoundVolume;
    private readonly InputAction m_Menus_Pause;
    private readonly InputAction m_Menus_Unpause;
    private readonly InputAction m_Menus_ToggleButtonInversion;
    public struct MenusActions
    {
        private @ControlLayout m_Wrapper;
        public MenusActions(@ControlLayout wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlayLevel => m_Wrapper.m_Menus_PlayLevel;
        public InputAction @NextScreen => m_Wrapper.m_Menus_NextScreen;
        public InputAction @RestartLevel => m_Wrapper.m_Menus_RestartLevel;
        public InputAction @ToggleMusic => m_Wrapper.m_Menus_ToggleMusic;
        public InputAction @ToggleSound => m_Wrapper.m_Menus_ToggleSound;
        public InputAction @ChangeGameVolume => m_Wrapper.m_Menus_ChangeGameVolume;
        public InputAction @ChangeMusicVolume => m_Wrapper.m_Menus_ChangeMusicVolume;
        public InputAction @ChangeSoundVolume => m_Wrapper.m_Menus_ChangeSoundVolume;
        public InputAction @Pause => m_Wrapper.m_Menus_Pause;
        public InputAction @Unpause => m_Wrapper.m_Menus_Unpause;
        public InputAction @ToggleButtonInversion => m_Wrapper.m_Menus_ToggleButtonInversion;
        public InputActionMap Get() { return m_Wrapper.m_Menus; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenusActions set) { return set.Get(); }
        public void SetCallbacks(IMenusActions instance)
        {
            if (m_Wrapper.m_MenusActionsCallbackInterface != null)
            {
                @PlayLevel.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnPlayLevel;
                @PlayLevel.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnPlayLevel;
                @PlayLevel.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnPlayLevel;
                @NextScreen.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnNextScreen;
                @NextScreen.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnNextScreen;
                @NextScreen.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnNextScreen;
                @RestartLevel.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnRestartLevel;
                @RestartLevel.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnRestartLevel;
                @RestartLevel.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnRestartLevel;
                @ToggleMusic.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnToggleMusic;
                @ToggleMusic.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnToggleMusic;
                @ToggleMusic.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnToggleMusic;
                @ToggleSound.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnToggleSound;
                @ToggleSound.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnToggleSound;
                @ToggleSound.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnToggleSound;
                @ChangeGameVolume.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnChangeGameVolume;
                @ChangeGameVolume.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnChangeGameVolume;
                @ChangeGameVolume.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnChangeGameVolume;
                @ChangeMusicVolume.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnChangeMusicVolume;
                @ChangeMusicVolume.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnChangeMusicVolume;
                @ChangeMusicVolume.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnChangeMusicVolume;
                @ChangeSoundVolume.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnChangeSoundVolume;
                @ChangeSoundVolume.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnChangeSoundVolume;
                @ChangeSoundVolume.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnChangeSoundVolume;
                @Pause.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnPause;
                @Unpause.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnUnpause;
                @Unpause.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnUnpause;
                @Unpause.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnUnpause;
                @ToggleButtonInversion.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnToggleButtonInversion;
                @ToggleButtonInversion.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnToggleButtonInversion;
                @ToggleButtonInversion.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnToggleButtonInversion;
            }
            m_Wrapper.m_MenusActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlayLevel.started += instance.OnPlayLevel;
                @PlayLevel.performed += instance.OnPlayLevel;
                @PlayLevel.canceled += instance.OnPlayLevel;
                @NextScreen.started += instance.OnNextScreen;
                @NextScreen.performed += instance.OnNextScreen;
                @NextScreen.canceled += instance.OnNextScreen;
                @RestartLevel.started += instance.OnRestartLevel;
                @RestartLevel.performed += instance.OnRestartLevel;
                @RestartLevel.canceled += instance.OnRestartLevel;
                @ToggleMusic.started += instance.OnToggleMusic;
                @ToggleMusic.performed += instance.OnToggleMusic;
                @ToggleMusic.canceled += instance.OnToggleMusic;
                @ToggleSound.started += instance.OnToggleSound;
                @ToggleSound.performed += instance.OnToggleSound;
                @ToggleSound.canceled += instance.OnToggleSound;
                @ChangeGameVolume.started += instance.OnChangeGameVolume;
                @ChangeGameVolume.performed += instance.OnChangeGameVolume;
                @ChangeGameVolume.canceled += instance.OnChangeGameVolume;
                @ChangeMusicVolume.started += instance.OnChangeMusicVolume;
                @ChangeMusicVolume.performed += instance.OnChangeMusicVolume;
                @ChangeMusicVolume.canceled += instance.OnChangeMusicVolume;
                @ChangeSoundVolume.started += instance.OnChangeSoundVolume;
                @ChangeSoundVolume.performed += instance.OnChangeSoundVolume;
                @ChangeSoundVolume.canceled += instance.OnChangeSoundVolume;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Unpause.started += instance.OnUnpause;
                @Unpause.performed += instance.OnUnpause;
                @Unpause.canceled += instance.OnUnpause;
                @ToggleButtonInversion.started += instance.OnToggleButtonInversion;
                @ToggleButtonInversion.performed += instance.OnToggleButtonInversion;
                @ToggleButtonInversion.canceled += instance.OnToggleButtonInversion;
            }
        }
    }
    public MenusActions @Menus => new MenusActions(this);
    private int m_PrimaryControlLayoutSchemeIndex = -1;
    public InputControlScheme PrimaryControlLayoutScheme
    {
        get
        {
            if (m_PrimaryControlLayoutSchemeIndex == -1) m_PrimaryControlLayoutSchemeIndex = asset.FindControlSchemeIndex("PrimaryControlLayout");
            return asset.controlSchemes[m_PrimaryControlLayoutSchemeIndex];
        }
    }
    public interface IBasicsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
    }
    public interface IMenusActions
    {
        void OnPlayLevel(InputAction.CallbackContext context);
        void OnNextScreen(InputAction.CallbackContext context);
        void OnRestartLevel(InputAction.CallbackContext context);
        void OnToggleMusic(InputAction.CallbackContext context);
        void OnToggleSound(InputAction.CallbackContext context);
        void OnChangeGameVolume(InputAction.CallbackContext context);
        void OnChangeMusicVolume(InputAction.CallbackContext context);
        void OnChangeSoundVolume(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnUnpause(InputAction.CallbackContext context);
        void OnToggleButtonInversion(InputAction.CallbackContext context);
    }
}
