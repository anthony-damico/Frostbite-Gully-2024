/*
MIT License

Copyright(c) 2019 Mitchel Thompson
www.angryarugula.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Reflection;
using System.Linq;
using System.IO;
using System;
using UnityEngine.InputSystem.LowLevel;

namespace Arugula.Peripherals
{

    public static class InputExtensions
    {
        public static Vector2 ToDpad(this Vector2 vec, float deadZone = 0.25f)
        {
            if (vec.x > deadZone) vec.x = 1;
            else if (vec.x < -deadZone) vec.x = -1;
            else vec.x = 0;

            if (vec.y > deadZone) vec.y = 1;
            else if (vec.y < -deadZone) vec.y = -1;
            else vec.y = 0;

            return vec;
        }

        public static float ToDigitalAxis(this float f, float deadZone = 0.25f)
        {
            if (f > deadZone)
                return 1;
            if (f < -deadZone)
                return -1;

            return 0;
        }
    }

    public class InputActionsWrapper : MonoBehaviour
    {
        public abstract class Control
        {
            public enum State
            {
                Enabled,
                Disabled,
                Rebinding
            }

            protected static string[] compositeRebindValidLayouts = new string[] { "Stick", "Axis", "Button", "DiscreteButton", "Key", "Vector2" };
            protected internal Guid guid;
            internal InputAction action;
            protected PlayerInput input;
            protected string[] rebindValidLayouts = new string[0];
            public abstract State state { get; protected set; }
            internal abstract void RegisterCallbacks(InputActionAsset asset);
            internal abstract void UnregisterCallbacks(InputActionAsset asset);
            public abstract bool Rebind(int controlIndex = -1, bool generalized = false, IEnumerable<InputDevice> devices = null);
            public abstract void CancelRebind();
            public abstract object GetValue();
            public abstract string name { get; }
            //public abstract string binding { get; }
            public abstract string devicePath { get; }
        }

        public class Control<T> : Control
        {
            public override State state { get; protected set; }
            protected T value;
            public T Value { get { return value; } }
            public virtual T DigitalValue { get { return value; } }

            private IEnumerable<InputDevice> rebindDevices;
            private Dictionary<InputControl, float> rebindStartingValues = new Dictionary<InputControl, float>();
            private Dictionary<InputControl, float> rebindValues = new Dictionary<InputControl, float>();
            private InputControl rebindControl;
            private float rebindControlSelectTime;
            private float rebindLockoutTime;
            private bool rebindInitialized;
            private bool rebindGeneralized;
            private int rebindingIndex = -1;

            public override string name { get { return action.name; } }
            public override string devicePath { get { return action.bindings[0].effectivePath; } }
            internal override void RegisterCallbacks(InputActionAsset asset)
            {
                if (action == null)
                    action = asset.FindAction(guid);

                if (action != null)
                    action.Enable();
            }

            internal override void UnregisterCallbacks(InputActionAsset asset)
            {
                if (state == State.Rebinding)
                    CancelRebind();

                action = null;
            }

            public override bool Rebind(int bindingIndex = -1, bool generalized = false, IEnumerable<InputDevice> devices = null)
            {
                if (bindingIndex == -1)
                    bindingIndex = 0;

                if (action.bindings[bindingIndex].isComposite)
                {
                    Debug.LogWarning("Cannot rebind a composite binding.");
                    return false;
                }

                rebindingIndex = bindingIndex;

                action.Disable();

                state = State.Rebinding;
                rebindDevices = devices ?? InputSystem.devices;
                rebindControl = null;
                rebindGeneralized = generalized;

                rebindLockoutTime = Time.unscaledTime + 0.2f;
                rebindInitialized = false;

                rebindStartingValues.Clear();
                rebindValues.Clear();

                InputSystem.onAfterUpdate += OnRebindPoll;

                return true;
            }

            void OnRebindPoll()
            {
                if (!rebindInitialized)
                {
                    foreach (var d in rebindDevices)
                    {
                        foreach (var control in d.allControls)
                        {
                            if (control.noisy)
                                continue;
                            if (control.path.StartsWith("/Mouse/delta"))
                                continue;
                            if (control.path.StartsWith("/Mouse/position"))
                                continue;
                            if (control.path.StartsWith("/Mouse/press"))
                                continue;

                            bool validControl = action.bindings[rebindingIndex].isPartOfComposite ? compositeRebindValidLayouts.Contains(control.layout) : rebindValidLayouts.Contains(control.layout);
                            if (validControl)
                            {
                                if (control.valueType == typeof(float))
                                    rebindValues[control] = rebindStartingValues[control] = (float)control.ReadValueAsObject();
                                else if (control.valueType == typeof(Vector2))
                                    rebindValues[control] = rebindStartingValues[control] = ((Vector2)control.ReadValueAsObject()).magnitude;
                            }
                        }
                    }

                    rebindInitialized = true;
                }

                foreach (var device in InputSystem.devices)
                {
                    if (rebindDevices.Contains(device))
                    {
                        var controls = device.allControls;
                        var controlCount = controls.Count;
                        for (int i = 0; i < controlCount; ++i)
                        {
                            var control = controls[i];

                            if (control.noisy)
                                continue;

                            if (control.path.StartsWith("/Mouse/delta"))
                                continue;
                            if (control.path.StartsWith("/Mouse/position"))
                                continue;
                            if (control.path.StartsWith("/Mouse/press"))
                                continue;

                            if (rebindValues.ContainsKey(control))
                            {
                                if (control.valueType == typeof(float))
                                    rebindValues[control] = (float)control.ReadValueAsObject();
                                else if (control.valueType == typeof(Vector2))
                                    rebindValues[control] = ((Vector2)control.ReadValueAsObject()).magnitude;

                                float x = rebindValues[control];

                                if (Mathf.Abs(x - rebindStartingValues[control]) > 0.25f)
                                {
                                    if (rebindControl != control)
                                    {
                                        rebindControl = control;
                                        rebindControlSelectTime = Time.unscaledTime + 0.25f;
                                    }
                                }
                                else
                                {
                                    if (rebindControl == control)
                                        rebindControl = null;
                                }
                            }
                        }
                    }
                }


                if (rebindControl != null && Time.unscaledTime > rebindControlSelectTime)
                {
                    CompleteRebind();
                }
            }

            void CompleteRebind()
            {
                if (state != State.Rebinding)
                    return;

                rebindStartingValues.Clear();
                rebindValues.Clear();

                InputSystem.onAfterUpdate -= OnRebindPoll;
                state = State.Enabled;

                string layout = rebindGeneralized ? FindLayoutThatIntroducesControl(rebindControl) : rebindControl.device.layout;

                string layoutPath = "";
                if (rebindControl.parent == rebindControl.device)
                    layoutPath = string.Format("<{0}>/{1}", layout, rebindControl.name);
                else
                    layoutPath = string.Format("<{0}>/{1}/{2}", layout, rebindControl.parent.name, rebindControl.name);

                Debug.Log(rebindingIndex + " : " + layoutPath);
                action.ChangeBinding(rebindingIndex).WithPath(layoutPath);
                action.ApplyBindingOverride(rebindingIndex, layoutPath);
                action.Enable();
                action.actionMap.Enable();
            }

            string FindLayoutThatIntroducesControl(InputControl control)
            {
                var topmostChild = control;
                while (topmostChild.parent != control.device)
                    topmostChild = topmostChild.parent;

                var deviceLayoutName = control.device.layout;
                var baseLayoutName = deviceLayoutName;

                while (true)
                {
                    baseLayoutName = InputSystem.GetNameOfBaseLayout(baseLayoutName);
                    if (baseLayoutName == null)
                        break;

                    UnityEngine.InputSystem.Utilities.InternedString controlName = new UnityEngine.InputSystem.Utilities.InternedString(topmostChild.name);
                    var controlItem = InputSystem.LoadLayout(baseLayoutName).FindControl(controlName);
                    if (controlItem != null)
                        deviceLayoutName = baseLayoutName;
                }
                return deviceLayoutName;
            }

            public override void CancelRebind()
            {
                rebindControl = null;
                rebindStartingValues.Clear();
                rebindValues.Clear();
                InputSystem.onAfterUpdate -= OnRebindPoll;
                state = State.Enabled;
                action.Enable();
            }

            public override string ToString()
            {
                return action.name + " : " + action.GetBindingDisplayString() + " : Enabled " + action.enabled;
            }

            public override object GetValue()
            {
                return (object)value;
            }
        }

        public class Stick : Control<Vector2>
        {
            public Stick(string guidStr)
            {
                guid = Guid.Parse(guidStr);
                rebindValidLayouts = new string[] { "Stick", "DPad", "Vector2" };
            }

            public float X { get { return value.x; } }
            public float Y { get { return value.y; } }
            public override Vector2 DigitalValue { get { return value.ToDpad(); } }

            void Update(Vector2 vec)
            {
                value = vec;
            }

            void Poll()
            {
                if (!action.enabled)
                    return;

                Update(action.ReadValue<Vector2>());
            }

            override internal void RegisterCallbacks(InputActionAsset asset)
            {
                base.RegisterCallbacks(asset);
                InputSystem.onAfterUpdate += Poll;
            }

            override internal void UnregisterCallbacks(InputActionAsset asset)
            {
                base.UnregisterCallbacks(asset);
                InputSystem.onAfterUpdate -= Poll;
            }
        }

        public class Axis : Control<float>
        {
            public Axis(string guidStr)
            {
                guid = Guid.Parse(guidStr);
                rebindValidLayouts = new string[] { "Axis" };
            }

            void Update(float vec)
            {
                value = vec;
            }

            void Poll()
            {
                if (!action.enabled)
                    return;

                Update(action.ReadValue<float>());
            }

            override internal void RegisterCallbacks(InputActionAsset asset)
            {
                base.RegisterCallbacks(asset);
                InputSystem.onAfterUpdate += Poll;
            }

            override internal void UnregisterCallbacks(InputActionAsset asset)
            {
                base.UnregisterCallbacks(asset);
                InputSystem.onAfterUpdate -= Poll;
            }

            public override float DigitalValue { get { return value.ToDigitalAxis(); } }
        }

        public class Button : Control<bool>
        {
            public Button(string guidStr)
            {
                guid = Guid.Parse(guidStr);
                rebindValidLayouts = new string[] { "Button", "DiscreteButton", "Key" };
            }

            bool buttonState;
            bool prevButtonState;

            public bool Pressed { get { return buttonState; } }
            public bool WasPressed { get { return buttonState & !prevButtonState; } }
            public bool WasReleased { get { return !buttonState & prevButtonState; } }

            void Update(bool value)
            {
                this.value = value;
                //bool s = value > InputSystem.settings.defaultButtonPressPoint;
                prevButtonState = buttonState;
                buttonState = value;
            }

            void Poll()
            {
                if (!action.enabled)
                    return;

                Update(action.ReadValue<float>() > InputSystem.settings.defaultButtonPressPoint);
            }

            override internal void RegisterCallbacks(InputActionAsset asset)
            {
                base.RegisterCallbacks(asset);
                InputSystem.onAfterUpdate += Poll;
            }

            override internal void UnregisterCallbacks(InputActionAsset asset)
            {
                base.UnregisterCallbacks(asset);
                InputSystem.onAfterUpdate -= Poll;
            }
        }

        public InputActionAsset inputActionAsset;
        public bool autoSaveBindings = false;
        public bool debugBindingGUI = false;
        public int UserIndex { get { return playerInput.user.index; } }
        protected PlayerInput playerInput;
        protected List<Control> controls = new List<Control>();
        Control rebindingControl;
        //bool instanced = false;

        uint playerInputUserId;

        public void Rebind(Control control, int controlIndex = -1, bool generalized = false)
        {
            if (rebindingControl != null)
                return;

            rebindingControl = control;

            if (playerInput != null)
            {
                //bind only to device associated with player, definitely generalize.
                if (playerInput.user.index >= 0)
                    control.Rebind(controlIndex, true, playerInput.user.pairedDevices);
                else
                    control.Rebind(controlIndex, true);
            }
            else
            {
                //bind to any device
                control.Rebind(controlIndex, generalized);
            }
        }


        public virtual void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            Type type = GetType();

            foreach (var f in type.GetFields())
            {
                Type t = f.FieldType;
                while (t != typeof(object))
                {
                    if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Control<>))
                    {
                        controls.Add((Control)f.GetValue(this));
                        break;
                    }
                    else
                    {
                        t = t.BaseType;
                    }
                }
            }
        }



        string GetDataFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Application.companyName, Application.productName);
        }

        string GetFileName()
        {
            string suffix = playerInput != null ? "_Player" + playerInputUserId : "";

            return string.Format("{0}{1}.json", GetType().Name, suffix);
        }

        public void Save()
        {
            string dir = GetDataFolder();
            string filePath = Path.Combine(dir, GetFileName());

            Directory.CreateDirectory(dir);

            File.WriteAllText(filePath, inputActionAsset.ToJson());
        }

        public void Load()
        {
            foreach (var c in controls)
                c.UnregisterCallbacks(inputActionAsset);

            string filePath = Path.Combine(GetDataFolder(), GetFileName());
            if (File.Exists(filePath))
            {
                string jsonStr = File.ReadAllText(filePath);
                inputActionAsset.LoadFromJson(jsonStr);
            }

            foreach (var c in controls)
                c.RegisterCallbacks(inputActionAsset);
        }

        public virtual void OnEnable()
        {
            if (playerInput != null)
            {
                inputActionAsset = playerInput.actions;
                playerInputUserId = playerInput.user.id;
                //Debug.Log(playerInput.playerIndex + " : " + playerInput.user.id);
            }
            else
            {
                //TODO: Instancing to prevent global changes??
                //if (!instanced)
                //{
                //    inputActionsAsset = Instantiate(inputActionsAsset);
                //    instanced = true;
                //}
            }

            foreach (var c in controls)
                c.RegisterCallbacks(inputActionAsset);

            Load();

            Rumble(0, 0);
        }

        public virtual void OnDisable()
        {
            foreach (var c in controls)
                c.UnregisterCallbacks(playerInput != null ? playerInput.actions : inputActionAsset);

            Rumble(0, 0);

            if (autoSaveBindings)
            {
                Save();
            }
        }

        //TODO: use events instead!  I'm lazy.
        private void Update()
        {
            if (rebindingControl != null)
            {
                if (rebindingControl.state != Control.State.Rebinding)
                    rebindingControl = null;
            }
        }

        public void Rumble(float lowFreq, float highFreq)
        {
            //TODO: support rumble on more than just gamepads
            IEnumerable<InputDevice> pads = null;

            if (playerInput != null)
                playerInput.devices.Where(x => x is Gamepad).ToArray();
            else
                InputSystem.devices.Where(x => x is Gamepad).ToArray();

            if (pads != null)
            {
                foreach (var p in pads)
                {
                    ((Gamepad)p).SetMotorSpeeds(lowFreq, highFreq);
                }
            }

        }

        private void OnGUI()
        {
            if (!debugBindingGUI)
                return;

            if (rebindingControl == null)
            {
                foreach (var c in controls)
                {
                    GUILayout.Label(c.name + "\t\t" + c.GetValue());
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Space(40);
                        for (int i = 0; i < c.action.bindings.Count; i++)
                        {
                            InputBinding b = c.action.bindings[i];
                            if (b.isComposite)
                                continue;
                            GUILayout.Space(50);
                            GUILayout.BeginVertical();
                            if (GUILayout.Button(b.ToString()))
                            {
                                if (c.Rebind(i, true))
                                {
                                    rebindingControl = c;
                                }
                            }
                            GUILayout.EndVertical();
                        }

                    }
                    GUILayout.EndHorizontal();
                }
            }
            else
            {
                GUILayout.Label("Rebinding " + rebindingControl.name);
            }

        }
    }
}