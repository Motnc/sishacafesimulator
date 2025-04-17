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

    private Seat assignedSeat;

    public TableSession Session { get; private set; }

    public void AssignSession(TableSession session)
    {
        Session = session;
        Session.RegisterCustomer(this);
    }

    public void RegisterOrder(string order)
    {
        Session?.RegisterOrder(this, order);
    }

    public void OnOrderDelivered()
    {
        Data.IsOrderDelivered = true;
        Session?.RegisterOrderDelivered(this); // Artýk doðrudan yemeðe geçmiyoruz
    }


    public void LeaveTable()
    {
        Session?.NotifyCustomerLeft(this);

        if (assignedSeat != null)
        {
            assignedSeat.Free(); // Koltuðu boþalt
            assignedSeat = null;
        }
    }

    public void Init(Transform tableTarget, Transform seatPosition, Transform exitPoint, GameObject moneyPrefab, Transform moneyPosition)
    {
        TableTarget = tableTarget;
        SeatPosition = seatPosition;
        ExitPoint = exitPoint;
        MoneyPrefab = moneyPrefab;
        MoneyPosition = moneyPosition;

        Data = new CustomerStateData
        {
            Agent = GetComponent<NavMeshAgent>(),
            Animator = GetComponent<Animator>(),
            TableTarget = TableTarget,
            SeatPosition = SeatPosition,
            ExitTarget = ExitPoint,
            MoneyPrefab = MoneyPrefab,
            MoneyPosition = MoneyPosition,
            IsOrderDelivered = false,
            FSMController = this
        };

        Handler = new StateMachineHandler<CustomerStateData>();
        Handler.AddState(new MoveToTableState(), Data);
    }

    public void StartEating()
    {
        Handler.AddState(new EatFoodState(), Data);
    }


    public void SetAssignedSeat(Seat seat)
    {
        assignedSeat = seat;
    }

    private void OnDestroy()
    {
        LeaveTable(); // NPC yok olunca da koltuk boþaltýlsýn
    }

    private void Update()
    {
        Handler?.UpdateStates();
    }
}
