using UnityEngine;

public class QuickSaveLoad : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
        }
        else if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadGame();
        }
    }

    private void SaveGame()
    {
        SaveData currentData = SaveLoadManager.LoadGame();
        SaveLoadManager.SaveGame(currentData);
        Debug.Log("<color=green>Game Saved (F5)</color>");
    }

    private void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        Debug.Log("<color=cyan>Game Loaded (F9)</color>");
    }
}
