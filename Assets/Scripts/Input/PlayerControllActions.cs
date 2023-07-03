//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.1
//     from Assets/Scripts/Input/PlayerControllActions.inputactions
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

public partial class @PlayerControllActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControllActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControllActions"",
    ""maps"": [
        {
            ""name"": ""ActionMap"",
            ""id"": ""010ef8b2-01f5-4682-a2a2-589797a11699"",
            ""actions"": [
                {
                    ""name"": ""DragAction"",
                    ""type"": ""Value"",
                    ""id"": ""0e36638c-dda7-4ab6-a364-99f170957e6e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ClickAction"",
                    ""type"": ""Button"",
                    ""id"": ""21d3e8eb-e4c4-46bd-8f0b-948cd27c472c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""27b722c9-0e47-414b-8f7b-786c4444f069"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""33818bc8-39c6-47d0-a8a4-4727aa9753f8"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DragAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4754139b-b02c-49b3-aed1-d179ac602c74"",
                    ""path"": ""<Pointer>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClickAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8205fa5b-b63d-48cd-a4c7-66f13e0852a5"",
                    ""path"": ""<Pointer>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // ActionMap
        m_ActionMap = asset.FindActionMap("ActionMap", throwIfNotFound: true);
        m_ActionMap_DragAction = m_ActionMap.FindAction("DragAction", throwIfNotFound: true);
        m_ActionMap_ClickAction = m_ActionMap.FindAction("ClickAction", throwIfNotFound: true);
        m_ActionMap_Fire = m_ActionMap.FindAction("Fire", throwIfNotFound: true);
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

    // ActionMap
    private readonly InputActionMap m_ActionMap;
    private List<IActionMapActions> m_ActionMapActionsCallbackInterfaces = new List<IActionMapActions>();
    private readonly InputAction m_ActionMap_DragAction;
    private readonly InputAction m_ActionMap_ClickAction;
    private readonly InputAction m_ActionMap_Fire;
    public struct ActionMapActions
    {
        private @PlayerControllActions m_Wrapper;
        public ActionMapActions(@PlayerControllActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @DragAction => m_Wrapper.m_ActionMap_DragAction;
        public InputAction @ClickAction => m_Wrapper.m_ActionMap_ClickAction;
        public InputAction @Fire => m_Wrapper.m_ActionMap_Fire;
        public InputActionMap Get() { return m_Wrapper.m_ActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionMapActions set) { return set.Get(); }
        public void AddCallbacks(IActionMapActions instance)
        {
            if (instance == null || m_Wrapper.m_ActionMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ActionMapActionsCallbackInterfaces.Add(instance);
            @DragAction.started += instance.OnDragAction;
            @DragAction.performed += instance.OnDragAction;
            @DragAction.canceled += instance.OnDragAction;
            @ClickAction.started += instance.OnClickAction;
            @ClickAction.performed += instance.OnClickAction;
            @ClickAction.canceled += instance.OnClickAction;
            @Fire.started += instance.OnFire;
            @Fire.performed += instance.OnFire;
            @Fire.canceled += instance.OnFire;
        }

        private void UnregisterCallbacks(IActionMapActions instance)
        {
            @DragAction.started -= instance.OnDragAction;
            @DragAction.performed -= instance.OnDragAction;
            @DragAction.canceled -= instance.OnDragAction;
            @ClickAction.started -= instance.OnClickAction;
            @ClickAction.performed -= instance.OnClickAction;
            @ClickAction.canceled -= instance.OnClickAction;
            @Fire.started -= instance.OnFire;
            @Fire.performed -= instance.OnFire;
            @Fire.canceled -= instance.OnFire;
        }

        public void RemoveCallbacks(IActionMapActions instance)
        {
            if (m_Wrapper.m_ActionMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IActionMapActions instance)
        {
            foreach (var item in m_Wrapper.m_ActionMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ActionMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ActionMapActions @ActionMap => new ActionMapActions(this);
    public interface IActionMapActions
    {
        void OnDragAction(InputAction.CallbackContext context);
        void OnClickAction(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
    }
}
