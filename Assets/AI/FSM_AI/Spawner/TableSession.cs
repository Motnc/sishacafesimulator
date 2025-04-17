using System.Collections.Generic;
using UnityEngine;

public class TableSession
{
    public TableGroup TableGroup { get; private set; }
    public List<CustomerFSMController> Customers { get; private set; } = new List<CustomerFSMController>();

    public TableSession(TableGroup tableGroup)
    {
        TableGroup = tableGroup;
    }

    public void RegisterCustomer(CustomerFSMController customer)
    {
        if (!Customers.Contains(customer))
        {
            Customers.Add(customer);
        }
    }

    public void RegisterOrder(CustomerFSMController customer, string order)
    {
        // Sipariþ kaydý yapýlabilir
    }

    public void NotifyCustomerLeft(CustomerFSMController customer)
    {
        Customers.Remove(customer);
        // Müþteri masadan ayrýldýðýnda yapýlacak iþlemler
    }

    public void RegisterOrderDelivered(CustomerFSMController customerInSession)
    {
        // Sipariþ teslim edildiðinde bu fonksiyon çaðrýlacak
        customerInSession.Data.IsOrderDelivered = true;

        // Tüm sipariþler teslim olduysa, müþteriye yemeðe baþlama zamaný ver
        if (AreAllOrdersDelivered())
        {
            foreach (var customer in Customers)
            {
                customer.StartEating();  // Burada yemeðe baþlatýyoruz
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
}


