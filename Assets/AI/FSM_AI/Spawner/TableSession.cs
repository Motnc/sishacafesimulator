using UnityEngine;
using System.Collections.Generic;

public class TableSession
{
    public TableGroup TableGroup { get; private set; }
    public List<CustomerFSMController> Customers { get; private set; } = new List<CustomerFSMController>();

    private Dictionary<CustomerFSMController, string> orders = new Dictionary<CustomerFSMController, string>();
    private const int MaxCustomers = 4;

    public bool IsGambling => TableGroup.mode == TableMode.Gambling;

    public TableSession(TableGroup tableGroup)
    {
        TableGroup = tableGroup;
    }

    public void RegisterOrder(CustomerFSMController customer, string order)
    {
        if (!orders.ContainsKey(customer))
            orders.Add(customer, order);
        else
            orders[customer] = order;

        Debug.Log($"Customer {customer.name} ordered {order}");
    }

    public void RegisterCustomer(CustomerFSMController customer)
    {
        if (!Customers.Contains(customer) && Customers.Count < MaxCustomers)
        {
            Customers.Add(customer);
        }
    }

    public void NotifyCustomerLeft(CustomerFSMController customer)
    {
        Customers.Remove(customer);
        customer.Data.IsOrderDelivered = false;
    }

    public void RegisterOrderDelivered(CustomerFSMController customerInSession)
    {
        customerInSession.Data.IsOrderDelivered = true;

        if (AreAllOrdersDelivered())
        {
            foreach (var customer in Customers)
            {
                customer.StartEating();
            }
        }
    }

    public bool AreAllOrdersDelivered()
    {
        foreach (var customer in Customers)
        {
            if (!customer.Data.IsOrderDelivered)
                return false;
        }
        return true;
    }

    public bool IsAvailable()
    {
        return Customers.Count < MaxCustomers;
    }

    public bool IsFull()
    {
        return Customers.Count == MaxCustomers;
    }
}
