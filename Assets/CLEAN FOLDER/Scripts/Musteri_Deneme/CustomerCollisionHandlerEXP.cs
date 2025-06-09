using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CustomerCollisionHandlerEXP : MonoBehaviour
{
    private CustomerFSMControllerEXP controller;

    private void Start()
    {
        controller = GetComponent<CustomerFSMControllerEXP>();
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
                controller.Handler.AddState(new EatFoodStateEXP(), controller.Data);
            }
            else
            {
                controller.Data.IsOrderDelivered = false;
                controller.Handler.AddState(new LeaveStateEXP(), controller.Data);
            }

            Destroy(collision.gameObject); // yemeði yok et
        }
    }
}
