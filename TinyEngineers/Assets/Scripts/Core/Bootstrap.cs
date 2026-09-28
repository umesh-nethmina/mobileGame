using UnityEngine;
using TinyEngineers.Utilities;

namespace TinyEngineers.Core
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameObject gameManagerPrefab;
        [SerializeField] private GameObject sceneLoaderPrefab;

        private void Start()
        {
            InitializeCoreSystems();
        }

        private void InitializeCoreSystems()
        {
            // Instantiate core singletons if they don't exist
            if (GameManager.Instance == null && gameManagerPrefab != null)
                Instantiate(gameManagerPrefab);

            if (SceneLoader.Instance == null && sceneLoaderPrefab != null)
                Instantiate(sceneLoaderPrefab);

            // Load main menu
            SceneLoader.Instance.LoadScene(Constants.SCENE_MAIN_MENU);
        }
    }
}
