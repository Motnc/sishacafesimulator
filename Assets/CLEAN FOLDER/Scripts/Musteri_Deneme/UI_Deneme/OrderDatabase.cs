using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "OrderDatabase", menuName = "Game/Order Database")]
public class OrderDatabase : ScriptableObject
{
    public List<OrderData> orders;
}
