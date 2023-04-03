using lab3.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lab3.Data.Context;

public class StudentsContext : IdentityDbContext<Student>
{
    public StudentsContext(DbContextOptions<StudentsContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Student>().ToTable("Students");
    }
}
