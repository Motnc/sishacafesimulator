using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Bu fonksiyonu butona baðlayacaðýz
    public void LoadSceneByName(string LevelSon_Motnc)
    {
        SceneManager.LoadScene(LevelSon_Motnc);
    }
}
