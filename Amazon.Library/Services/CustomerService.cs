using Amazon.Library.Models;
using System.Collections.ObjectModel;

public class CustomerService
{
    private static CustomerService? instance;
    private static object instanceLock = new object();

    private List<Customer> customers;
    public ReadOnlyCollection<Customer> Customers
    {
        get
        {
            return customers.AsReadOnly();
        }
    }

    private int NextId
    {
        get
        {
            if (!Customers.Any())
            {
                return 1;
            }
            return customers.Select(c => c.Id).Max() + 1;
        }
    }
    public Customer? AddorUpdate(Customer? c)
    {
        if (customers == null)
        {
            return null;
        }
        bool isAdd = false;
        if (c.Id == 0)
        {
            isAdd = true;
            c.Id = NextId;
        }
        if (isAdd)
        {
            customers.Add(c);
        }
        if (!isAdd)
        {
            var Customer_toUpdate = customers.FirstOrDefault(customer => customer.Id == c.Id);
            Customer_toUpdate = c;
        }

        return c;
    }
    public Customer GetCustomer(int customerId)
    {
        return customers.FirstOrDefault(c => c.Id == customerId) ?? new Customer();
    }

    private CustomerService()
    {
        customers = new List<Customer>() {
                new Customer { Id = 1, Name = "Customer 1", Address = "1 Address"},
            new Customer { Id = 2, Name = "Customer 2", Address = "2 Address" },
            new Customer { Id = 3, Name = "Customer 3", Address = "3 Address" }
            };
    }
    public static CustomerService Current
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new CustomerService();
                }
            }
            return instance;
        }
    }
    public void Delete(int id)
    {
        if (customers == null)
        {
            return;
        }
        var CustomerToDelete = customers.FirstOrDefault(c => c.Id == id);
        if (CustomerToDelete != null)
        {
            customers.Remove(CustomerToDelete);
        }
    }
}



