using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
// using MySql.Data.EntityFrameworkCore;
using appointmentApi.Models.Entities;


namespace appointmentApi.Models.data
{
    public class AppointmentDbContext : DbContext
    {
        public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options) : base(options)
        {}

        public DbSet<User> users {get; set;}

        public DbSet<Appointment> appoinments {get; set;}
        public DbSet<Hospital> hospitals {get; set;}



    }
}