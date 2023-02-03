using System;
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
        
        private void RegisterButtonListeners(UiManager manager)
        {
            _buttons = GetComponentsInChildren<Button>();
            
            if(_buttons.Length == 0) return;
            _uiManager = manager;
            
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
    }
}
