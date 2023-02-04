using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ItemSelection : MonoBehaviour
    {
        private Button[] _buttons;
        private UiManager _uiManager;

        private void Awake()
        {
            GameManager.Instance.OnUiLoadedEvent += RegisterButtonListeners;
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnUiLoadedEvent -= RegisterButtonListeners;
        }

        private void Update()
        {
            //check for keyboard keys 1-3 and select the corresponding item
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                OnButtonClicked(_buttons[0]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                OnButtonClicked(_buttons[1]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                OnButtonClicked(_buttons[2]);
            }
        }

        private void RegisterButtonListeners(UiManager manager)
        {
            _buttons = GetComponentsInChildren<Button>();
            
            if(_buttons.Length == 0) return;
            _uiManager = manager;

            GameStartedSetup();
            
            foreach (var button in _buttons)
            {
                button.onClick.AddListener(() => OnButtonClicked(button));
            }
        }
        
        private void OnButtonClicked(Button buttonClicked)
        {
            foreach (var button in _buttons)
            {
                button.GetComponent<Image>().color = button == buttonClicked ? Color.green : Color.white;
            }

            var index = Array.IndexOf(_buttons, buttonClicked);
            _uiManager.OnItemSelected(index);
        }
        
        private void GameStartedSetup()
        {
            OnButtonClicked(_buttons[2]); //Select the empty hand work item on game start
        }
    }
}
