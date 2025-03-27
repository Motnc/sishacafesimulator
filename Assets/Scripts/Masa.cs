using UnityEngine;

public class Masa : MonoBehaviour
{
    public bool isGambling;

    private Highlight highlight;
    public Color gamblingColor = Color.green;
    public Color normalColor = Color.red;

    private bool canToggle = false;

    private void Start()
    {
        highlight = GetComponent<Highlight>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canToggle = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canToggle = false;
        }
    }

    private void Update()
    {
        if (canToggle && Input.GetKeyDown(KeyCode.C))
        {
            isGambling = !isGambling;
            ModeTable();
        }
    }

    private void ModeTable()
    {
        if (isGambling)
        {
            highlight.color = gamblingColor;
            highlight.ToggleHighlight(true);
        }
        else
        {
            highlight.color = normalColor;
            highlight.ToggleHighlight(true);
        }
    }
}
