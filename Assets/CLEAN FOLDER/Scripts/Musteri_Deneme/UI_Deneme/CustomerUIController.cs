using UnityEngine;
using UnityEngine.UI;

public class CustomerUIController : MonoBehaviour
{
    [SerializeField] private Image orderImage;
    [SerializeField] private Image emotionImage;

    public void SetOrderSprite(Sprite sprite)
    {
        Debug.Log("SetOrderSprite çaðrýldý: " + sprite?.name);
        orderImage.sprite = sprite;
        orderImage.gameObject.SetActive(sprite != null);
    }
    public void SetEmotionSprite(Sprite sprite)
    {
        emotionImage.sprite = sprite;
        emotionImage.gameObject.SetActive(sprite != null);
    }
    public void ClearEmotion()
    {
        emotionImage.sprite = null;
        emotionImage.gameObject.SetActive(false);
    }
    public void DeleteOrderSprite()
    {
        SetOrderSprite(null);
        orderImage.gameObject.SetActive(false);
    }
}
