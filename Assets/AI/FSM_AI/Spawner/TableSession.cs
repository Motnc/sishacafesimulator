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
        // Sipari� kayd� yap�labilir
    }

    public void NotifyCustomerLeft(CustomerFSMController customer)
    {
        Customers.Remove(customer);
        // M��teri masadan ayr�ld���nda yap�lacak i�lemler
    }

    public void RegisterOrderDelivered(CustomerFSMController customerInSession)
    {
        // Sipari� teslim edildi�inde bu fonksiyon �a�r�lacak
        customerInSession.Data.IsOrderDelivered = true;

        // T�m sipari�ler teslim olduysa, m��teriye yeme�e ba�lama zaman� ver
        if (AreAllOrdersDelivered())
        {
            foreach (var customer in Customers)
            {
                customer.StartEating();  // Burada yeme�e ba�lat�yoruz
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


