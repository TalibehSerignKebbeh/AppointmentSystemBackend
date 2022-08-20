using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appointmentApi.Models.data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using appointmentApi.Models;
using appointmentApi.Models.Entities;
using Models;


namespace Services;

    public class UserServices : IUserServices
{
    private readonly AppointmentDbContext _context;
    private readonly IMapper _mapper;

    public UserServices(AppointmentDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public  User Login(AuthModel authModel){
        var user =   _context.users.SingleOrDefault(u => u.username == authModel.loginKey && u.password == authModel.password);
        if (user is null)
            {
                throw new Exception("username or password is incorrect");

            }
            // if (user.role == "patient") return _mapper.Map<GetUser>(user);
            // if (user.role == "doctor") return _mapper.Map<Doctor>(user);

            // return _mapper.Map<GetUser>(user);
            return user;
    }

}

public interface IUserServices
{
    User Login(AuthModel authModel);
    // Task<ActionResult<Patient>> registerPatient(Patient patient);
    // Task<ActionResult<UserDto>> register(UserDto user);
    // Task<ActionResult<Doctor>> registerDoctor(Doctor doctor);
    // Task<ActionResult<IEnumerable<User>>> getUser();
    // Task<ActionResult<IEnumerable<Doctor>>> getDoctors();
    // Task<ActionResult<IEnumerable<GetPatientDoctor>>> getPatientDcotor();
    // Task<ActionResult<IEnumerable<GetPatient>>> getPatients();
    // Task<ActionResult<IEnumerable<User>>> GetUser(int id);
    // Task<ActionResult<IEnumerable<User>>> UpdatePatient(int patId, User apatient);
    // Task<ActionResult<IEnumerable<Admin>>> PutAdmin(int id, UpdateAdmin updatedAdmin);
    // Task<ActionResult> DeletePatient(int id);
    // bool UserExist(int id);

}
