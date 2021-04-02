// GENERATED AUTOMATICALLY FROM 'Assets/Input System/Player Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player Controls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""d4acfb13-2014-4044-86ac-47e694f7c08b"",
            ""actions"": [
                {
                    ""name"": ""Move Left"",
                    ""type"": ""Button"",
                    ""id"": ""f0c8fa6b-0d6d-48ff-87a7-e4998a32ab2c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move Right"",
                    ""type"": ""Button"",
                    ""id"": ""6bb273e2-ec90-4425-9cb0-1b57e3c8f6cd"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""51cbe2a8-141f-428a-897b-fbc5ed4c15bb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""b7d13aa8-e19d-4e0c-9ab7-9c5a82e9413f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Normal Attack"",
                    ""type"": ""Button"",
                    ""id"": ""ba876282-ad2d-42c7-bc55-898f872eac3c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Special Attack"",
                    ""type"": ""Button"",
                    ""id"": ""e6f9ff96-7db7-43a8-80d7-099105fcf5b6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""e2a378cb-2818-4cce-82f0-5a99d2f11407"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Parry"",
                    ""type"": ""Button"",
                    ""id"": ""a9ad7563-6793-416c-8a64-6b028872c139"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""Button"",
                    ""id"": ""b2687775-420e-4dcd-b7de-e74ed47b6fb1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""8ec5d58a-14c0-4431-ae43-6fdb0005dcb7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShortHop"",
                    ""type"": ""Button"",
                    ""id"": ""4ce51085-e8c7-4eac-b30a-4361545a461f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FullHop"",
                    ""type"": ""Button"",
                    ""id"": ""38b703c3-4870-4920-9e2e-b4bc4e31fddc"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwapForm"",
                    ""type"": ""Button"",
                    ""id"": ""34842858-9b7e-4e27-a653-c029f1048506"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1f35276e-961d-4f4c-8c6d-a5aded3e69fb"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd32beff-7ca8-4595-ae60-9c87153ceba8"",
                    ""path"": ""<HID::mayflash limited MAYFLASH GameCube Controller Adapter>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mayflash GameCube"",
                    ""action"": ""Move Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d242943d-1bed-4ee7-85d7-d5efa5789260"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b7c1d819-8d11-4e0c-b258-4fb9d5cfde0c"",
                    ""path"": ""<HID::mayflash limited MAYFLASH GameCube Controller Adapter>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mayflash GameCube"",
                    ""action"": ""Move Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7493a50f-d2a3-4c02-abdc-434edf3f5e76"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1e12303-2bf4-4d75-bf0a-7b067a9aa309"",
                    ""path"": ""<Joystick>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mayflash GameCube"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""045bc842-d2f4-4632-bd35-201d5ea47cd7"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bbce8374-8c7d-4a67-9f78-15d9a2dcdfb5"",
                    ""path"": ""<HID::mayflash limited MAYFLASH GameCube Controller Adapter>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mayflash GameCube"",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d4a8ff1b-39c7-452d-93a1-0acf88071282"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Normal Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aac6fb08-da14-4f3c-9733-a7cb51d77178"",
                    ""path"": ""<HID::mayflash limited MAYFLASH GameCube Controller Adapter>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mayflash GameCube"",
                    ""action"": ""Normal Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06ab4103-7808-4094-92ae-d13c8e36462b"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Special Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""95c507fb-1cb0-4902-9515-7c621755174d"",
                    ""path"": ""<HID::mayflash limited MAYFLASH GameCube Controller Adapter>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mayflash GameCube"",
                    ""action"": ""Special Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a0e8677-d9d2-406c-a64d-5b533672e98b"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9af7ff37-af30-4047-a12c-95712b3b134e"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Parry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""586e1ff2-1e00-4c4a-b9d4-17dfdaaef142"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bac47ea2-ae2e-47d9-9178-ec740140d3eb"",
                    ""path"": ""<HID::mayflash limited MAYFLASH GameCube Controller Adapter>/button6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mayflash GameCube"",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e151793b-569c-4bde-84f6-5520d49fdf17"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64263019-9daa-4c44-91e3-830071ff6dbc"",
                    ""path"": ""<HID::mayflash limited MAYFLASH GameCube Controller Adapter>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mayflash GameCube"",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""261b96e0-0556-490d-86da-7a821ca15adf"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ShortHop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f02f17b7-7317-4cd9-bfc7-acf0c3580886"",
                    ""path"": ""<HID::mayflash limited MAYFLASH GameCube Controller Adapter>/button4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mayflash GameCube"",
                    ""action"": ""ShortHop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ada5f1f9-dc46-44cc-b0b8-d1cff54e2c6d"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""FullHop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86a34902-ef4a-465b-ae81-8efde41a7b66"",
                    ""path"": ""<HID::mayflash limited MAYFLASH GameCube Controller Adapter>/button5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mayflash GameCube"",
                    ""action"": ""FullHop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5bf753eb-b438-471e-ad4f-b33e1effa0be"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SwapForm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""01e7f777-d52c-4eca-9716-7b1ddd7ef604"",
                    ""path"": ""<Keyboard>/rightShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SwapForm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Mayflash GameCube"",
            ""bindingGroup"": ""Mayflash GameCube"",
            ""devices"": [
                {
                    ""devicePath"": ""<HID::mayflash limited MAYFLASH GameCube Controller Adapter>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_MoveLeft = m_Gameplay.FindAction("Move Left", throwIfNotFound: true);
        m_Gameplay_MoveRight = m_Gameplay.FindAction("Move Right", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Down = m_Gameplay.FindAction("Down", throwIfNotFound: true);
        m_Gameplay_NormalAttack = m_Gameplay.FindAction("Normal Attack", throwIfNotFound: true);
        m_Gameplay_SpecialAttack = m_Gameplay.FindAction("Special Attack", throwIfNotFound: true);
        m_Gameplay_Dash = m_Gameplay.FindAction("Dash", throwIfNotFound: true);
        m_Gameplay_Parry = m_Gameplay.FindAction("Parry", throwIfNotFound: true);
        m_Gameplay_Block = m_Gameplay.FindAction("Block", throwIfNotFound: true);
        m_Gameplay_Up = m_Gameplay.FindAction("Up", throwIfNotFound: true);
        m_Gameplay_ShortHop = m_Gameplay.FindAction("ShortHop", throwIfNotFound: true);
        m_Gameplay_FullHop = m_Gameplay.FindAction("FullHop", throwIfNotFound: true);
        m_Gameplay_SwapForm = m_Gameplay.FindAction("SwapForm", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_MoveLeft;
    private readonly InputAction m_Gameplay_MoveRight;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Down;
    private readonly InputAction m_Gameplay_NormalAttack;
    private readonly InputAction m_Gameplay_SpecialAttack;
    private readonly InputAction m_Gameplay_Dash;
    private readonly InputAction m_Gameplay_Parry;
    private readonly InputAction m_Gameplay_Block;
    private readonly InputAction m_Gameplay_Up;
    private readonly InputAction m_Gameplay_ShortHop;
    private readonly InputAction m_Gameplay_FullHop;
    private readonly InputAction m_Gameplay_SwapForm;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveLeft => m_Wrapper.m_Gameplay_MoveLeft;
        public InputAction @MoveRight => m_Wrapper.m_Gameplay_MoveRight;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Down => m_Wrapper.m_Gameplay_Down;
        public InputAction @NormalAttack => m_Wrapper.m_Gameplay_NormalAttack;
        public InputAction @SpecialAttack => m_Wrapper.m_Gameplay_SpecialAttack;
        public InputAction @Dash => m_Wrapper.m_Gameplay_Dash;
        public InputAction @Parry => m_Wrapper.m_Gameplay_Parry;
        public InputAction @Block => m_Wrapper.m_Gameplay_Block;
        public InputAction @Up => m_Wrapper.m_Gameplay_Up;
        public InputAction @ShortHop => m_Wrapper.m_Gameplay_ShortHop;
        public InputAction @FullHop => m_Wrapper.m_Gameplay_FullHop;
        public InputAction @SwapForm => m_Wrapper.m_Gameplay_SwapForm;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @MoveLeft.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveLeft;
                @MoveRight.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveRight;
                @MoveRight.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveRight;
                @MoveRight.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveRight;
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Down.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDown;
                @NormalAttack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNormalAttack;
                @NormalAttack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNormalAttack;
                @NormalAttack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNormalAttack;
                @SpecialAttack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialAttack;
                @SpecialAttack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialAttack;
                @SpecialAttack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialAttack;
                @Dash.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Parry.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnParry;
                @Parry.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnParry;
                @Parry.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnParry;
                @Block.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBlock;
                @Block.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBlock;
                @Block.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBlock;
                @Up.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUp;
                @ShortHop.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShortHop;
                @ShortHop.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShortHop;
                @ShortHop.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShortHop;
                @FullHop.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFullHop;
                @FullHop.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFullHop;
                @FullHop.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFullHop;
                @SwapForm.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwapForm;
                @SwapForm.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwapForm;
                @SwapForm.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwapForm;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveLeft.started += instance.OnMoveLeft;
                @MoveLeft.performed += instance.OnMoveLeft;
                @MoveLeft.canceled += instance.OnMoveLeft;
                @MoveRight.started += instance.OnMoveRight;
                @MoveRight.performed += instance.OnMoveRight;
                @MoveRight.canceled += instance.OnMoveRight;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @NormalAttack.started += instance.OnNormalAttack;
                @NormalAttack.performed += instance.OnNormalAttack;
                @NormalAttack.canceled += instance.OnNormalAttack;
                @SpecialAttack.started += instance.OnSpecialAttack;
                @SpecialAttack.performed += instance.OnSpecialAttack;
                @SpecialAttack.canceled += instance.OnSpecialAttack;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Parry.started += instance.OnParry;
                @Parry.performed += instance.OnParry;
                @Parry.canceled += instance.OnParry;
                @Block.started += instance.OnBlock;
                @Block.performed += instance.OnBlock;
                @Block.canceled += instance.OnBlock;
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @ShortHop.started += instance.OnShortHop;
                @ShortHop.performed += instance.OnShortHop;
                @ShortHop.canceled += instance.OnShortHop;
                @FullHop.started += instance.OnFullHop;
                @FullHop.performed += instance.OnFullHop;
                @FullHop.canceled += instance.OnFullHop;
                @SwapForm.started += instance.OnSwapForm;
                @SwapForm.performed += instance.OnSwapForm;
                @SwapForm.canceled += instance.OnSwapForm;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_MayflashGameCubeSchemeIndex = -1;
    public InputControlScheme MayflashGameCubeScheme
    {
        get
        {
            if (m_MayflashGameCubeSchemeIndex == -1) m_MayflashGameCubeSchemeIndex = asset.FindControlSchemeIndex("Mayflash GameCube");
            return asset.controlSchemes[m_MayflashGameCubeSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnMoveLeft(InputAction.CallbackContext context);
        void OnMoveRight(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnNormalAttack(InputAction.CallbackContext context);
        void OnSpecialAttack(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnParry(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnShortHop(InputAction.CallbackContext context);
        void OnFullHop(InputAction.CallbackContext context);
        void OnSwapForm(InputAction.CallbackContext context);
    }
}
