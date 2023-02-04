using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class GameLoader : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private GameObject mainCamera;
        [SerializeField] private GameObject[] miscelleniousObjects;
        
        private const int GAME_START_DELAY = 2;
        private const string MAIN_MENU_SCENE = "Main Menu";
        private const string START_BUTTON_TAG = "GameController";

        private void Start()
        {
            GameObject.FindGameObjectWithTag(START_BUTTON_TAG)
                      .GetComponent<Button>().onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            StartCoroutine(WakeUpManagers());
        }
        
        private IEnumerator WakeUpManagers()
        {
            SceneManager.UnloadSceneAsync(MAIN_MENU_SCENE);
            yield return new WaitForSeconds(GAME_START_DELAY);
            gameManager.gameObject.SetActive(true);
            
            //get all components on the player and enable them
            var components = player.GetComponents<MonoBehaviour>();
            foreach (var component in components)
            {
                component.enabled = true;
            }

            foreach (var item in miscelleniousObjects)
            {
                item.SetActive(true);
            }
            mainCamera.SetActive(true);

            Destroy(gameObject);
        }
    }
}