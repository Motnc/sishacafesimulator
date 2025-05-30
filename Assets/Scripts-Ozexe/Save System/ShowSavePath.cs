using UnityEngine;

public class ShowSavePath : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Save Path: " + Application.persistentDataPath);
    }
}
