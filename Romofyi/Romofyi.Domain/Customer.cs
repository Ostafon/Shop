using System;
using System.Collections.Generic;

public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
    public string PasswordHash { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }

    public Customer()
    {
        Orders = new HashSet<Order>();
    }
}