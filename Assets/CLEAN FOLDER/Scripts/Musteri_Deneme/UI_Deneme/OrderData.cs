using UnityEngine;

[CreateAssetMenu(fileName = "NewOrderData", menuName = "Game/Order Data")]
public class OrderData : ScriptableObject
{
    public string orderName;
    public Sprite orderSprite;

    [Header("Bekleme Sprite'larý")]
    public Sprite waitStage1; // 0–15 saniye
    public Sprite waitStage2; // 15–30 saniye
    public Sprite waitStage3; // 30+ saniye
}
