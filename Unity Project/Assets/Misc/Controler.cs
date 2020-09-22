// GENERATED AUTOMATICALLY FROM 'Assets/Misc/Controler.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controler : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controler()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controler"",
    ""maps"": [
        {
            ""name"": ""Keyboard"",
            ""id"": ""9d34a369-1aa4-4dad-8c80-3cb771c11955"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""d0a3708e-0aea-45cb-b5fc-520e85d8644f"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookAround"",
                    ""type"": ""Value"",
                    ""id"": ""ec4761a4-d712-4569-b20b-9e5fb6381571"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Test"",
                    ""type"": ""Button"",
                    ""id"": ""7d55a281-06f0-429d-a494-57be671345e1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack1"",
                    ""type"": ""Button"",
                    ""id"": ""f5616699-21f9-4b58-aa9d-8ac4e50fd841"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack2"",
                    ""type"": ""Button"",
                    ""id"": ""e60b9ef7-7e38-4e83-98d6-c26f7f15e9eb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""4aef1dbf-1140-4b28-b396-bd69995151a8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6b37b23d-581e-4eaf-afaa-e2188ee3648d"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a4c20637-bf89-469e-b17c-ce49d422e117"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8736b103-206d-4995-a3a1-dd81a71ece64"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0887a40a-d386-4dd7-bbdf-7d9ad14441c2"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Stick"",
                    ""id"": ""e15de764-d8a7-4405-acda-e5a894365ad7"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone(min=0.5),Normalize(min=1,max=1,zero=1)"",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""08e895f8-2329-4480-8663-2d447a8fb424"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""dc0cb13a-f5cb-4f13-a929-f6f41b79e67e"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3defedf1-1106-4fb4-8882-8b6371fd5e69"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0d0d4655-1e0b-438d-9a9a-79e8dda1c26e"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f8348947-9665-461f-b93b-9fa5b44d3010"",
                    ""path"": ""<Keyboard>/#(M)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d5d5297f-adf7-48aa-a7d3-c0c8ca737f13"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5f12486-d56b-44e5-a9ad-8444d7d29a3f"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e09f4c4a-8ea5-4700-91d7-e0095facf867"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70c2d521-c1e3-4137-ad8b-2773680fc817"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99e1f559-3b37-4c44-aa5e-0dd4936cf923"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dccce192-5c38-4585-84bf-076dfd9034c8"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9485d58-dc63-46af-8bab-d77d05e4e3ec"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone(min=0.5),NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""LookAround"",
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
            ""devices"": []
        }
    ]
}");
        // Keyboard
        m_Keyboard = asset.FindActionMap("Keyboard", throwIfNotFound: true);
        m_Keyboard_Movement = m_Keyboard.FindAction("Movement", throwIfNotFound: true);
        m_Keyboard_LookAround = m_Keyboard.FindAction("LookAround", throwIfNotFound: true);
        m_Keyboard_Test = m_Keyboard.FindAction("Test", throwIfNotFound: true);
        m_Keyboard_Attack1 = m_Keyboard.FindAction("Attack1", throwIfNotFound: true);
        m_Keyboard_Attack2 = m_Keyboard.FindAction("Attack2", throwIfNotFound: true);
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

    // Keyboard
    private readonly InputActionMap m_Keyboard;
    private IKeyboardActions m_KeyboardActionsCallbackInterface;
    private readonly InputAction m_Keyboard_Movement;
    private readonly InputAction m_Keyboard_LookAround;
    private readonly InputAction m_Keyboard_Test;
    private readonly InputAction m_Keyboard_Attack1;
    private readonly InputAction m_Keyboard_Attack2;
    public struct KeyboardActions
    {
        private @Controler m_Wrapper;
        public KeyboardActions(@Controler wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Keyboard_Movement;
        public InputAction @LookAround => m_Wrapper.m_Keyboard_LookAround;
        public InputAction @Test => m_Wrapper.m_Keyboard_Test;
        public InputAction @Attack1 => m_Wrapper.m_Keyboard_Attack1;
        public InputAction @Attack2 => m_Wrapper.m_Keyboard_Attack2;
        public InputActionMap Get() { return m_Wrapper.m_Keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardActions instance)
        {
            if (m_Wrapper.m_KeyboardActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMovement;
                @LookAround.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnLookAround;
                @LookAround.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnLookAround;
                @LookAround.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnLookAround;
                @Test.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTest;
                @Test.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTest;
                @Test.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTest;
                @Attack1.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnAttack1;
                @Attack1.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnAttack1;
                @Attack1.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnAttack1;
                @Attack2.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnAttack2;
                @Attack2.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnAttack2;
                @Attack2.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnAttack2;
            }
            m_Wrapper.m_KeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @LookAround.started += instance.OnLookAround;
                @LookAround.performed += instance.OnLookAround;
                @LookAround.canceled += instance.OnLookAround;
                @Test.started += instance.OnTest;
                @Test.performed += instance.OnTest;
                @Test.canceled += instance.OnTest;
                @Attack1.started += instance.OnAttack1;
                @Attack1.performed += instance.OnAttack1;
                @Attack1.canceled += instance.OnAttack1;
                @Attack2.started += instance.OnAttack2;
                @Attack2.performed += instance.OnAttack2;
                @Attack2.canceled += instance.OnAttack2;
            }
        }
    }
    public KeyboardActions @Keyboard => new KeyboardActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IKeyboardActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnLookAround(InputAction.CallbackContext context);
        void OnTest(InputAction.CallbackContext context);
        void OnAttack1(InputAction.CallbackContext context);
        void OnAttack2(InputAction.CallbackContext context);
    }
}
