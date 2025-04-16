using UnityEngine;
using UnityEngine.AI;
using BeykozEdu.FSM;

public class CustomerFSMController : MonoBehaviour
{
    public CustomerStateData Data { get; private set; }
    public StateMachineHandler<CustomerStateData> Handler { get; private set; }

    [Header("Setup")]
    public Transform TableTarget;
    public Transform SeatPosition;
    public Transform ExitPoint;
    public GameObject MoneyPrefab;
    public Transform MoneyPosition;

    private void Start()
    {
        Data = new CustomerStateData
        {
            Agent = GetComponent<NavMeshAgent>(),
            Animator = GetComponent<Animator>(),
            TableTarget = TableTarget,
            SeatPosition = SeatPosition,
            ExitTarget = ExitPoint,
            MoneyPrefab = MoneyPrefab,
            MoneyPosition = MoneyPosition,
            IsOrderDelivered = false
        };

        Handler = new StateMachineHandler<CustomerStateData>();
        Handler.AddState(new MoveToTableState(), Data);
    }

    private void Update()
    {
        Handler.UpdateStates();
    }
}