using System.Collections.Generic;
using UnityEngine;

public class NargileAssembly : MonoBehaviour
{
    public List<GameObject> nargileParts; // Sahnedeki par�alar
    public List<Transform> placementPoints; // Par�alar�n gitmesi gereken noktalar
    private int placedCount = 0; // Ka� par�a yerle�tirildi�ini takip eder

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            AssembleNargile();
        }
    }

    void AssembleNargile()
    {
        if (placedCount < nargileParts.Count)
        {
            nargileParts[placedCount].transform.position = placementPoints[placedCount].position;
            nargileParts[placedCount].transform.rotation = placementPoints[placedCount].rotation;
            placedCount++;

            if (placedCount == nargileParts.Count)
            {
                Debug.Log("Nargile tamamland�!");
            }
        }
    }
}
