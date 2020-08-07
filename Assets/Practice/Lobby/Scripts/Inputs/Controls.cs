// GENERATED AUTOMATICALLY FROM 'Assets/Practice/Lobby/Input/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using Object = UnityEngine.Object;

namespace Assets.Practice.Lobby.Scripts.Inputs
{
    public class Controls : IInputActionCollection, IDisposable
    {
        public InputActionAsset Asset { get; }

        public Controls()
        {
            Asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""39a51676-d738-45f3-85e3-ba5583bee819"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""41a64174-84b9-4162-9841-6789773ead1a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""3c566883-1a6c-420f-af1a-98ae4f505898"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""05e6dbc9-5d9b-4092-a66b-e1ab746e1792"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""8958683a-0db2-474c-8906-3b8c732cea02"",
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
                    ""id"": ""4de36aa6-682f-4235-9a33-08aa659ce7fe"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7f1e3438-af69-40a9-bfff-93be3ec515e3"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""98da20e7-b271-4185-bc98-dc8b25a097bc"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""09f9c3eb-5261-4ece-9230-0aa1df53c703"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard & Mouse"",
            ""bindingGroup"": ""Keyboard & Mouse"",
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
        }
    ]
}");
            // Player
            _mPlayer = Asset.FindActionMap("Player", throwIfNotFound: true);
            _mPlayerLook = _mPlayer.FindAction("Look", throwIfNotFound: true);
            _mPlayerMove = _mPlayer.FindAction("Move", throwIfNotFound: true);
        }

        public void Dispose()
        {
            Object.Destroy(Asset);
        }

        public InputBinding? bindingMask
        {
            get => Asset.bindingMask;
            set => Asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => Asset.devices;
            set => Asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => Asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return Asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return Asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            Asset.Enable();
        }

        public void Disable()
        {
            Asset.Disable();
        }

        // Player
        private readonly InputActionMap _mPlayer;

        private IPlayerActions _mPlayerActionsCallbackInterface;
        private readonly InputAction _mPlayerLook;
        private readonly InputAction _mPlayerMove;

        public struct PlayerActions
        {
            private readonly Controls _mWrapper;

            public PlayerActions(Controls wrapper)
            {
                _mWrapper = wrapper;
            }

            public InputAction Look => _mWrapper._mPlayerLook;
            public InputAction Move => _mWrapper._mPlayerMove;

            public InputActionMap Get()
            {
                return _mWrapper._mPlayer;
            }

            public void Enable()
            {
                Get().Enable();
            }

            public void Disable()
            {
                Get().Disable();
            }

            public bool Enabled => Get().enabled;

            public static implicit operator InputActionMap(PlayerActions set)
            {
                return set.Get();
            }

            public void SetCallbacks(IPlayerActions instance)
            {
                if (_mWrapper._mPlayerActionsCallbackInterface != null)
                {
                    Look.started -= _mWrapper._mPlayerActionsCallbackInterface.OnLook;
                    Look.performed -= _mWrapper._mPlayerActionsCallbackInterface.OnLook;
                    Look.canceled -= _mWrapper._mPlayerActionsCallbackInterface.OnLook;
                    Move.started -= _mWrapper._mPlayerActionsCallbackInterface.OnMove;
                    Move.performed -= _mWrapper._mPlayerActionsCallbackInterface.OnMove;
                    Move.canceled -= _mWrapper._mPlayerActionsCallbackInterface.OnMove;
                }
                _mWrapper._mPlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    Look.started += instance.OnLook;
                    Look.performed += instance.OnLook;
                    Look.canceled += instance.OnLook;
                    Move.started += instance.OnMove;
                    Move.performed += instance.OnMove;
                    Move.canceled += instance.OnMove;
                }
            }
        }

        public PlayerActions Player => new PlayerActions(this);
        private int _mKeyboardMouseSchemeIndex = -1;

        public InputControlScheme KeyboardMouseScheme
        {
            get
            {
                if (_mKeyboardMouseSchemeIndex == -1) _mKeyboardMouseSchemeIndex = Asset.FindControlSchemeIndex("Keyboard & Mouse");
                return Asset.controlSchemes[_mKeyboardMouseSchemeIndex];
            }
        }

        public interface IPlayerActions
        {
            void OnLook(InputAction.CallbackContext context);

            void OnMove(InputAction.CallbackContext context);
        }
    }
}