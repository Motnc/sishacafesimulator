using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CustomerCollisionHandler : MonoBehaviour
{
    private CustomerFSMController controller;
    public Transform OrderPos;

    private void Start()
    {
        controller = GetComponent<CustomerFSMController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var food = collision.gameObject.GetComponent<FoodItem>();
        if (food != null)
        {
            var expectedOrder = controller.Data.SelectedOrder;

            if (expectedOrder == food.FoodName)
            {
                controller.Data.IsOrderDelivered = true;
                controller.Handler.AddState(new EatFoodState(), controller.Data);
            }
            else
            {
                controller.Data.IsOrderDelivered = false;
                controller.Handler.AddState(new LeaveState(), controller.Data);
            }

            collision.transform.position = OrderPos.position;
        }
    }
}
