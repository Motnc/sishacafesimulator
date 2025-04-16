using UnityEngine;
using UnityEngine.AI;
using BeykozEdu.FSM.Interfaces;

public class CustomerStateData : IBaseStateData
{
    public NavMeshAgent Agent;

    public Animator Animator;

    public Transform TableTarget;
    public Transform SeatPosition;
    public Transform ExitTarget;
    public Transform MoneyPosition;

    public GameObject MoneyPrefab;

    public string SelectedOrder;

    public bool IsOrderDelivered;

    public System.Action<string> OnOrderSelected;


}