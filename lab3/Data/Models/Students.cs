using Microsoft.AspNetCore.Identity;

namespace lab3.Data.Models;

public class Student : IdentityUser
{
    public int Degree { get; set; }
}
