﻿namespace Oms.Schema;

public class UserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    //public string Role { get; set; }
}

public class UserResponse
{
    public int UserNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    //public string Role { get; set; }

    public virtual List<OrderResponse> Orders { get; set; }
}