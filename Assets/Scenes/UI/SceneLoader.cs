using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Bu fonksiyonu butona ba�layaca��z
    public void LoadSceneByName(string LevelSon)
    {
        SceneManager.LoadScene(LevelSon);
    }
}
