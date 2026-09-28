using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TinyEngineers.Utilities;

namespace TinyEngineers.Core
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            GameManager.Instance.ChangeState(GameState.Loading);
            
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            
            while (!operation.isDone)
            {
                // Optional: Update loading UI progress here
                yield return null;
            }
            
            // Wait one frame to ensure objects initialize
            yield return null; 
            
            if (sceneName == Constants.SCENE_MAIN_MENU)
                GameManager.Instance.ChangeState(GameState.MainMenu);
            else if (sceneName == Constants.SCENE_PIZZA_LEVEL)
                GameManager.Instance.ChangeState(GameState.Playing);
            else if (sceneName == Constants.SCENE_RESULTS)
                GameManager.Instance.ChangeState(GameState.Results);
        }
    }
}
