using UnityEngine;
using TinyEngineers.Utilities;

namespace TinyEngineers.SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance { get; private set; }
        
        public SaveData CurrentData { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                LoadGame();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SaveGame()
        {
            string json = JsonUtility.ToJson(CurrentData, true);
            PlayerPrefs.SetString(Constants.SAVE_FILE_NAME, json);
            PlayerPrefs.Save();
            Debug.Log("Game Saved!");
        }

        public void LoadGame()
        {
            if (PlayerPrefs.HasKey(Constants.SAVE_FILE_NAME))
            {
                string json = PlayerPrefs.GetString(Constants.SAVE_FILE_NAME);
                CurrentData = JsonUtility.FromJson<SaveData>(json);
            }
            else
            {
                CurrentData = new SaveData();
                SaveGame(); // Create new save file
            }
        }

        public void ResetProgress()
        {
            CurrentData = new SaveData();
            SaveGame();
        }
    }
}
