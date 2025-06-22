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
    public GameObject DirtyPrefab;
    public Transform MoneyPosition;
    public Transform DirtyPosition; // <-- Burayý ekledim

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
            DirtyPrefab = DirtyPrefab,
            MoneyPosition = MoneyPosition,
            DirtyPosition = DirtyPosition, // <-- Burada atandý
            IsOrderDelivered = false,
            UIController = UIController,
            OrderDatabase = OrderDatabase
        };

        MasaEXP masa = TableTarget.GetComponent<MasaEXP>();
        if (masa != null)
        {
            masa.IsEmpty = false;
            // DirtyPosition'ý masa üzerinden alýyoruz
            DirtyPosition = masa.dirtySpawnPoint;

            if (DirtyPosition == null)
            {
                Debug.LogWarning($"Masa {masa.name} dirtySpawnPoint null!");
            }
            else
            {
                Debug.Log($"Masa {masa.name} dirtySpawnPoint atandý: {DirtyPosition.position}");
            }

            Data.DirtyPosition = DirtyPosition; // ve Data'ya ekliyoruz
        }
        else
        {
            Debug.LogWarning($"TableTarget'da MasaEXP component bulunamadý! TableTarget: {TableTarget?.name ?? "null"}");
        }

        Handler = new StateMachineHandler<CustomerStateDataEXP>();
        Handler.AddState(new MoveToTableStateEXP(), Data);

        isFSMActive = true;
    }

    public void Setup(Transform table, Transform seat, Transform exitTarget, GameObject moneyPrefab, GameObject dirtyPrefab, Transform moneyPosition, Transform dirtyPosition, CustomerUIController uiController)
    {
        TableTarget = table;
        SeatPosition = seat;
        ExitTarget = exitTarget;
        MoneyPrefab = moneyPrefab;
        DirtyPrefab = dirtyPrefab;
        MoneyPosition = moneyPosition;
        DirtyPosition = dirtyPosition;  // <-- Setup parametrelerine eklendi
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
        Data.DirtyPrefab = DirtyPrefab;
        Data.MoneyPosition = MoneyPosition;
        Data.DirtyPosition = DirtyPosition;  // <-- Data’ya da atandý
        Data.IsOrderDelivered = false;
        Data.UIController = UIController;

        MasaEXP masa = TableTarget.GetComponent<MasaEXP>();
        if (masa != null)
        {
            masa.IsEmpty = false;

            if (DirtyPosition == null)
            {
                Debug.LogWarning($"Masa {masa.name} dirtySpawnPoint null!");
            }
            else
            {
                Debug.Log($"Masa {masa.name} dirtySpawnPoint atandý: {DirtyPosition.position}");
            }
        }
        else
        {
            Debug.LogWarning($"TableTarget'da MasaEXP component bulunamadý! TableTarget: {TableTarget?.name ?? "null"}");
        }

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
        return TableTarget != null && SeatPosition != null && ExitTarget != null && MoneyPrefab != null && DirtyPrefab != null && MoneyPosition != null && DirtyPosition != null;
    }
}
