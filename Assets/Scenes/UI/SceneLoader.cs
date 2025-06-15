using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Bu fonksiyonu butona baðlayacaðýz
    public void LoadSceneByName(string LevelSon)
    {
        SceneManager.LoadScene(LevelSon);
    }
}
