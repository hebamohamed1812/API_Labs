﻿namespace Tickets.DAL;

public class Ticket
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public Severity? Severity { get; set; }
    public decimal EstimationCost { get; set; }
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }
    public ICollection<Developer> Developers { get; set; } = new HashSet<Developer>();
}

public enum Severity
{
    Low, Medium, High
}
