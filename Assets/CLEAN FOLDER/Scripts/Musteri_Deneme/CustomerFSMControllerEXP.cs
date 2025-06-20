using UnityEngine;
using UnityEngine.AI;
using BeykozEdu.FSM;

public class CustomerFSMControllerEXP : MonoBehaviour
{
    public CustomerStateDataEXP Data { get; private set; }
    public StateMachineHandler<CustomerStateDataEXP> Handler { get; private set; }

    [Header("Setup")]
    public Transform TableTarget;
    public Transform SeatPosition;
    public Transform ExitTarget;
    public GameObject MoneyPrefab;
    public Transform MoneyPosition;

    [Header("UI")]
    public CustomerUIController UIController;

    [Header("Order")]
    public OrderDatabase OrderDatabase;

    private bool isFSMActive = false;

    private void Start()
    {
        if (!IsSetupValid())
        {
            Debug.LogWarning("CustomerFSMController: Setup eksik! FSM baþlatýlmayacak.");
            return;
        }

        InitializeFSM();
    }

    private void Update()
    {
        if (isFSMActive && Handler != null)
        {
            Handler.UpdateStates();
        }
    }

    private void InitializeFSM()
    {
        Data = new CustomerStateDataEXP
        {
            Agent = GetComponent<NavMeshAgent>(),
            Animator = GetComponent<Animator>(),
            TableTarget = TableTarget,
            SeatPosition = SeatPosition,
            ExitTarget = ExitTarget,
            MoneyPrefab = MoneyPrefab,
            MoneyPosition = MoneyPosition,
            IsOrderDelivered = false,
            UIController = UIController,
            OrderDatabase = OrderDatabase // Burasý önemli
        };


        Handler = new StateMachineHandler<CustomerStateDataEXP>();
        Handler.AddState(new MoveToTableStateEXP(), Data);

        MasaEXP masa = TableTarget.GetComponent<MasaEXP>();
        if (masa != null)
        {
            masa.IsEmpty = false;
        }

        isFSMActive = true;
    }

    public void Setup(Transform table, Transform seat, Transform exitTarget, GameObject moneyPrefab, Transform moneyPosition, CustomerUIController uiController)
    {
        TableTarget = table;
        SeatPosition = seat;
        ExitTarget = exitTarget;
        MoneyPrefab = moneyPrefab;
        MoneyPosition = moneyPosition;
        UIController = uiController;

        if (Data == null)
        {
            Data = new CustomerStateDataEXP
            {
                Agent = GetComponent<NavMeshAgent>(),
                Animator = GetComponent<Animator>()
            };
        }

        Data.TableTarget = TableTarget;
        Data.SeatPosition = SeatPosition;
        Data.ExitTarget = ExitTarget;
        Data.MoneyPrefab = MoneyPrefab;
        Data.MoneyPosition = MoneyPosition;
        Data.IsOrderDelivered = false;
        Data.UIController = UIController;

        if (Handler == null)
        {
            Handler = new StateMachineHandler<CustomerStateDataEXP>();
        }
        else
        {
            Handler.RemoveState();
        }

        Handler.AddState(new MoveToTableStateEXP(), Data);
        isFSMActive = true;
    }

    public void StopFSM()
    {
        isFSMActive = false;
        Debug.Log("FSM devre dýþý býrakýldý!");
    }

    private bool IsSetupValid()
    {
        return TableTarget != null && SeatPosition != null && ExitTarget != null && MoneyPrefab != null && MoneyPosition != null;
    }
}
