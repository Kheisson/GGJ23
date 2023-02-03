using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public event Action<UiManager> OnUiLoadedEvent;
        
        private const string UI_SCENE_NAME = "UI Scene";
        private UiManager _uiManager;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            StartCoroutine(LoadUIScene());
        }

        private IEnumerator LoadUIScene()
        {
            var uiScene = SceneManager.LoadSceneAsync(UI_SCENE_NAME, LoadSceneMode.Additive);

            while (!uiScene.isDone)
            {
                yield return null;
            }

            uiScene.allowSceneActivation = true;
            _uiManager = new UiManager();
            OnUiLoadedEvent?.Invoke(_uiManager);
        }
    }
}