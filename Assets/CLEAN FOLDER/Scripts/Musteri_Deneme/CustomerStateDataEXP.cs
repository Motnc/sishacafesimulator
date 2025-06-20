using UnityEngine;
using UnityEngine.AI;
using BeykozEdu.FSM.Interfaces;

public class CustomerStateDataEXP : IBaseStateData
{
    public NavMeshAgent Agent;
    public Animator Animator;
    public Transform TableTarget;
    public Transform SeatPosition;
    public Transform ExitTarget;   // MASA’YA ÖZEL ÇIKIÞ NOKTASI
    public Transform MoneyPosition;
    public GameObject MoneyPrefab;
    public string SelectedOrder;
    public bool IsOrderDelivered;
    public System.Action<string> OnOrderSelected;

    public CustomerUIController UIController;

    public OrderDatabase OrderDatabase;
}
