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
                    ""name"": ""Attack1"",
                    ""type"": ""Button"",
                    ""id"": ""f5616699-21f9-4b58-aa9d-8ac4e50fd841"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Attack2"",
                    ""type"": ""Button"",
                    ""id"": ""e60b9ef7-7e38-4e83-98d6-c26f7f15e9eb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Switch"",
                    ""type"": ""Button"",
                    ""id"": ""2206c3df-2ff9-433e-bd69-c747bc287326"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""ea48d916-390e-4412-ab63-e1323c916780"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""a2b1891d-3fad-47e7-a45b-549b1a410649"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""MoneyCheat"",
                    ""type"": ""Button"",
                    ""id"": ""7d55a281-06f0-429d-a494-57be671345e1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""b0644a3c-f63e-4669-9b08-b1ef284afaec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""f91b6fd0-ae99-416a-8227-097db47ddb96"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""b11ebd30-db6b-4730-8352-e213fa9dab99"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Drop"",
                    ""type"": ""Button"",
                    ""id"": ""b103ae0b-2a07-453b-a8da-2aebac0bfe8b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Rewind"",
                    ""type"": ""Button"",
                    ""id"": ""40c2c0bf-2ca1-477f-88e5-77c73ea91bfe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""LifeCheat"",
                    ""type"": ""Button"",
                    ""id"": ""444612bf-4bf8-4dcd-b355-e6a4f0fa123d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""DungeonCheat"",
                    ""type"": ""Button"",
                    ""id"": ""b75aa85b-41ce-43cb-8b1c-758adcc075a8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""NormalizeTime"",
                    ""type"": ""Button"",
                    ""id"": ""0d44a26c-902c-443a-845f-ce6c476c313f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
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
                    ""name"": ""Keyboard"",
                    ""id"": ""cd53aa8f-a009-49a1-b681-3c4c8cca6a95"",
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
                    ""id"": ""a720e22d-626b-4269-aa3d-5ec0406846fa"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""365b40e7-5ae6-4435-9dcb-605c4829656f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""64197f7c-40c5-44a3-9216-d3f596c25a80"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""dbf56293-194f-44c2-934c-53cee4c6f36a"",
                    ""path"": ""<Keyboard>/d"",
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
                    ""id"": ""e09f4c4a-8ea5-4700-91d7-e0095facf867"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c34e588-e116-4d98-8baa-3bca41a04b84"",
                    ""path"": ""<DualShockGamepad>/leftShoulder"",
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
                    ""id"": ""dccce192-5c38-4585-84bf-076dfd9034c8"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""efd748fd-af50-4dc6-9d1c-66a17769aeb2"",
                    ""path"": ""<DualShockGamepad>/rightShoulder"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""1e73946c-1a02-4a2d-bb46-4ce53be47e10"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26edd114-204c-4ad9-913d-8804288bcb87"",
                    ""path"": ""<XInputController>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9e9ea5e-7c5d-424a-9a94-a99a78d444d2"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""972a7ab0-e6dd-4f20-a537-4af70a569d2f"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3067a460-44f7-4eb2-aa29-b8ac638b8e35"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d048f879-7ab5-481d-8dd3-d4a02467c788"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2408a18-e10d-46a1-a86c-c7a8c26bbf9c"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3d56a05a-56bf-440f-8fc5-e1931865228c"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e374c73-3d33-467e-8a63-5f9f063abc5c"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8348947-9665-461f-b93b-9fa5b44d3010"",
                    ""path"": ""<Keyboard>/#(M)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoneyCheat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c675381-a063-4218-a668-665fcedab829"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoneyCheat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f2fc7260-972a-48e1-bbf5-974c3d3f4e7e"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d12513b-43d6-4828-b2ec-05c6815aa680"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69fe4e7c-eb1f-401c-85c0-83fc9ed877ab"",
                    ""path"": ""<DualShockGamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3caabc4-e0ee-4e17-9579-c8508ff1f365"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18a5b66e-4bc5-45df-9b4f-11b1fddba3a3"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64b934fd-e620-49e5-b54d-c783dc7d1ec3"",
                    ""path"": ""<DualShockGamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65406ea1-4e6d-4986-a06e-6a0e1bef85bb"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""038754de-8012-44f0-bf00-15702396663a"",
                    ""path"": ""<XInputController>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dde3e5c3-55e2-4e68-a75f-a2649cf7f7ad"",
                    ""path"": ""<DualShockGamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
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
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b3a398b-e28c-4fcc-9ef7-b6db18463704"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4446735-310b-4194-b78b-3a07fb808db9"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72439400-1ecc-4b46-a7f8-3abc765526e4"",
                    ""path"": ""<Keyboard>/capsLock"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rewind"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""351df75c-c53e-45a8-bd4c-26a7797fe24d"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rewind"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c33ac5eb-f2fc-4216-940a-775c65d7df56"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rewind"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a084318b-2f58-413d-95f9-44e4b1c7e970"",
                    ""path"": ""<DualShockGamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rewind"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af977fd4-dfe4-4762-b88c-e057a6fb9b03"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LifeCheat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac6bb998-c124-41b5-a0af-d1a5d328cf58"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LifeCheat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb211288-3b9f-4bc5-b3b9-ef0ddc9f8233"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DungeonCheat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc83b9de-8a1e-4450-a8d7-0077620f5cde"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NormalizeTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66214079-8bcb-4ec0-880e-d56833ab32a0"",
                    ""path"": ""<DualShockGamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NormalizeTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69a074aa-6a4a-4027-971c-01e6ef8caa76"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NormalizeTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""TimeControl"",
            ""id"": ""feada16f-16ea-4390-a982-34bc35cd7e86"",
            ""actions"": [
                {
                    ""name"": ""SlowDownTime"",
                    ""type"": ""Button"",
                    ""id"": ""60235f00-aff6-4945-b767-f0ceca46d567"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""01096b7a-b3f3-45c0-8baf-17ffe813c996"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SlowDownTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce59643c-e82f-43a5-aec0-2e386b0c2102"",
                    ""path"": ""<DualShockGamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SlowDownTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""05614274-804a-416c-9734-b4711aad9bfb"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SlowDownTime"",
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
        m_Keyboard_Attack1 = m_Keyboard.FindAction("Attack1", throwIfNotFound: true);
        m_Keyboard_Attack2 = m_Keyboard.FindAction("Attack2", throwIfNotFound: true);
        m_Keyboard_Switch = m_Keyboard.FindAction("Switch", throwIfNotFound: true);
        m_Keyboard_Roll = m_Keyboard.FindAction("Roll", throwIfNotFound: true);
        m_Keyboard_MousePosition = m_Keyboard.FindAction("MousePosition", throwIfNotFound: true);
        m_Keyboard_MoneyCheat = m_Keyboard.FindAction("MoneyCheat", throwIfNotFound: true);
        m_Keyboard_Pause = m_Keyboard.FindAction("Pause", throwIfNotFound: true);
        m_Keyboard_Interact = m_Keyboard.FindAction("Interact", throwIfNotFound: true);
        m_Keyboard_Inventory = m_Keyboard.FindAction("Inventory", throwIfNotFound: true);
        m_Keyboard_Drop = m_Keyboard.FindAction("Drop", throwIfNotFound: true);
        m_Keyboard_Rewind = m_Keyboard.FindAction("Rewind", throwIfNotFound: true);
        m_Keyboard_LifeCheat = m_Keyboard.FindAction("LifeCheat", throwIfNotFound: true);
        m_Keyboard_DungeonCheat = m_Keyboard.FindAction("DungeonCheat", throwIfNotFound: true);
        m_Keyboard_NormalizeTime = m_Keyboard.FindAction("NormalizeTime", throwIfNotFound: true);
        // TimeControl
        m_TimeControl = asset.FindActionMap("TimeControl", throwIfNotFound: true);
        m_TimeControl_SlowDownTime = m_TimeControl.FindAction("SlowDownTime", throwIfNotFound: true);
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
    private readonly InputAction m_Keyboard_Attack1;
    private readonly InputAction m_Keyboard_Attack2;
    private readonly InputAction m_Keyboard_Switch;
    private readonly InputAction m_Keyboard_Roll;
    private readonly InputAction m_Keyboard_MousePosition;
    private readonly InputAction m_Keyboard_MoneyCheat;
    private readonly InputAction m_Keyboard_Pause;
    private readonly InputAction m_Keyboard_Interact;
    private readonly InputAction m_Keyboard_Inventory;
    private readonly InputAction m_Keyboard_Drop;
    private readonly InputAction m_Keyboard_Rewind;
    private readonly InputAction m_Keyboard_LifeCheat;
    private readonly InputAction m_Keyboard_DungeonCheat;
    private readonly InputAction m_Keyboard_NormalizeTime;
    public struct KeyboardActions
    {
        private @Controler m_Wrapper;
        public KeyboardActions(@Controler wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Keyboard_Movement;
        public InputAction @LookAround => m_Wrapper.m_Keyboard_LookAround;
        public InputAction @Attack1 => m_Wrapper.m_Keyboard_Attack1;
        public InputAction @Attack2 => m_Wrapper.m_Keyboard_Attack2;
        public InputAction @Switch => m_Wrapper.m_Keyboard_Switch;
        public InputAction @Roll => m_Wrapper.m_Keyboard_Roll;
        public InputAction @MousePosition => m_Wrapper.m_Keyboard_MousePosition;
        public InputAction @MoneyCheat => m_Wrapper.m_Keyboard_MoneyCheat;
        public InputAction @Pause => m_Wrapper.m_Keyboard_Pause;
        public InputAction @Interact => m_Wrapper.m_Keyboard_Interact;
        public InputAction @Inventory => m_Wrapper.m_Keyboard_Inventory;
        public InputAction @Drop => m_Wrapper.m_Keyboard_Drop;
        public InputAction @Rewind => m_Wrapper.m_Keyboard_Rewind;
        public InputAction @LifeCheat => m_Wrapper.m_Keyboard_LifeCheat;
        public InputAction @DungeonCheat => m_Wrapper.m_Keyboard_DungeonCheat;
        public InputAction @NormalizeTime => m_Wrapper.m_Keyboard_NormalizeTime;
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
                @Attack1.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnAttack1;
                @Attack1.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnAttack1;
                @Attack1.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnAttack1;
                @Attack2.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnAttack2;
                @Attack2.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnAttack2;
                @Attack2.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnAttack2;
                @Switch.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSwitch;
                @Switch.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSwitch;
                @Switch.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSwitch;
                @Roll.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRoll;
                @MousePosition.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMousePosition;
                @MoneyCheat.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMoneyCheat;
                @MoneyCheat.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMoneyCheat;
                @MoneyCheat.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMoneyCheat;
                @Pause.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnPause;
                @Interact.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnInteract;
                @Inventory.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnInventory;
                @Drop.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnDrop;
                @Drop.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnDrop;
                @Drop.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnDrop;
                @Rewind.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRewind;
                @Rewind.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRewind;
                @Rewind.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRewind;
                @LifeCheat.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnLifeCheat;
                @LifeCheat.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnLifeCheat;
                @LifeCheat.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnLifeCheat;
                @DungeonCheat.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnDungeonCheat;
                @DungeonCheat.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnDungeonCheat;
                @DungeonCheat.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnDungeonCheat;
                @NormalizeTime.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnNormalizeTime;
                @NormalizeTime.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnNormalizeTime;
                @NormalizeTime.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnNormalizeTime;
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
                @Attack1.started += instance.OnAttack1;
                @Attack1.performed += instance.OnAttack1;
                @Attack1.canceled += instance.OnAttack1;
                @Attack2.started += instance.OnAttack2;
                @Attack2.performed += instance.OnAttack2;
                @Attack2.canceled += instance.OnAttack2;
                @Switch.started += instance.OnSwitch;
                @Switch.performed += instance.OnSwitch;
                @Switch.canceled += instance.OnSwitch;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @MoneyCheat.started += instance.OnMoneyCheat;
                @MoneyCheat.performed += instance.OnMoneyCheat;
                @MoneyCheat.canceled += instance.OnMoneyCheat;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @Drop.started += instance.OnDrop;
                @Drop.performed += instance.OnDrop;
                @Drop.canceled += instance.OnDrop;
                @Rewind.started += instance.OnRewind;
                @Rewind.performed += instance.OnRewind;
                @Rewind.canceled += instance.OnRewind;
                @LifeCheat.started += instance.OnLifeCheat;
                @LifeCheat.performed += instance.OnLifeCheat;
                @LifeCheat.canceled += instance.OnLifeCheat;
                @DungeonCheat.started += instance.OnDungeonCheat;
                @DungeonCheat.performed += instance.OnDungeonCheat;
                @DungeonCheat.canceled += instance.OnDungeonCheat;
                @NormalizeTime.started += instance.OnNormalizeTime;
                @NormalizeTime.performed += instance.OnNormalizeTime;
                @NormalizeTime.canceled += instance.OnNormalizeTime;
            }
        }
    }
    public KeyboardActions @Keyboard => new KeyboardActions(this);

    // TimeControl
    private readonly InputActionMap m_TimeControl;
    private ITimeControlActions m_TimeControlActionsCallbackInterface;
    private readonly InputAction m_TimeControl_SlowDownTime;
    public struct TimeControlActions
    {
        private @Controler m_Wrapper;
        public TimeControlActions(@Controler wrapper) { m_Wrapper = wrapper; }
        public InputAction @SlowDownTime => m_Wrapper.m_TimeControl_SlowDownTime;
        public InputActionMap Get() { return m_Wrapper.m_TimeControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TimeControlActions set) { return set.Get(); }
        public void SetCallbacks(ITimeControlActions instance)
        {
            if (m_Wrapper.m_TimeControlActionsCallbackInterface != null)
            {
                @SlowDownTime.started -= m_Wrapper.m_TimeControlActionsCallbackInterface.OnSlowDownTime;
                @SlowDownTime.performed -= m_Wrapper.m_TimeControlActionsCallbackInterface.OnSlowDownTime;
                @SlowDownTime.canceled -= m_Wrapper.m_TimeControlActionsCallbackInterface.OnSlowDownTime;
            }
            m_Wrapper.m_TimeControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SlowDownTime.started += instance.OnSlowDownTime;
                @SlowDownTime.performed += instance.OnSlowDownTime;
                @SlowDownTime.canceled += instance.OnSlowDownTime;
            }
        }
    }
    public TimeControlActions @TimeControl => new TimeControlActions(this);
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
        void OnAttack1(InputAction.CallbackContext context);
        void OnAttack2(InputAction.CallbackContext context);
        void OnSwitch(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnMoneyCheat(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnDrop(InputAction.CallbackContext context);
        void OnRewind(InputAction.CallbackContext context);
        void OnLifeCheat(InputAction.CallbackContext context);
        void OnDungeonCheat(InputAction.CallbackContext context);
        void OnNormalizeTime(InputAction.CallbackContext context);
    }
    public interface ITimeControlActions
    {
        void OnSlowDownTime(InputAction.CallbackContext context);
    }
}
