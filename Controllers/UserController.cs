using System.Net;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using appointmentApi.Models.data;
using appointmentApi.Models;
using appointmentApi.Models.Entities;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Security.AccessControl;
using System.Security.Authentication;
using System.Security.Permissions;
using System.Security.Policy;
using System.Net.Security;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Models;

using ResponseMessages;

namespace appointmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       private readonly AppointmentDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;
        public UserController
        (
            AppointmentDbContext context, 
            IMapper mapper, 
            IWebHostEnvironment hostEnvironment,
             IConfiguration configuration
        )
        {
            _context = context;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;

        }  
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.users.ToListAsync();
        }
        
        [HttpGet("doctors")]
        public async Task<ActionResult<IEnumerable<GetDoctor>>> GetDoctors()
        {
            return await _context.users.Where(u =>u.role == Roles.doctor).Select(d=>_mapper.Map<GetDoctor>(d)).ToListAsync();
        }
        
        [HttpGet("patientDoctors")]
        public async Task<ActionResult<IEnumerable<GetPatientDoctor>>> GetPatientDocs(){
           var doctors = await _context.users.Where(u => u.role == Roles.doctor).Select(d => _mapper.Map<GetPatientDoctor>(d)).ToListAsync();
            doctors.ForEach(doctor => doctor.hospital = _context.hospitals.Find(doctor.hospitalRefId));
            return doctors;
        }
        
        [HttpGet("patients")]
        public async Task<ActionResult<IEnumerable<GetPatient>>> GetPatients()
        {
            return await _context.users.Where(u =>u.role == Roles.patient).Select(p=>_mapper.Map<GetPatient>(p)).ToListAsync();
        }
         [HttpPost("login")]
        public  async Task<ActionResult<User>> Login( AuthModel auth)
        {
            // var user = _services.Login(auth);
            var user = await _context.users.FirstOrDefaultAsync(u => u.username == auth.loginKey && u.password == auth.password  && !u.isDeleted);
             if (user != null)
             {
                if (user.role == Roles.patient ) return Ok(_mapper.Map<GetPatient>(user));
                if (user.role == Roles.doctor) return Ok(_mapper.Map<GetDoctor>(user));
                return Ok(_mapper.Map<Admin>(user));
             }
             
            return NotFound("Invalid credentials");
        }

        [HttpPost("addAdmin")]
        public async Task<ActionResult<Admin>> AddAdmin([FromForm] Admin admin)
        {
            var auser = await _context.users.FirstOrDefaultAsync(u => u.username == admin.username);
            if (auser != null)
            {
                return BadRequest(new { message = "username already exist" });
            }
            User user = _mapper.Map<User>(admin);
            user.dateRegistered = DateTime.Now;
            user.lastModified = DateTime.Now;
            user.role = Roles.admin;
            user.acceptTerms = true;
            user.isActive = true;
            user.isApproved = true;
            if(admin.ImageFile != null) user.imageName = await SaveImage(admin.ImageFile);
            _context.users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<Admin>(user));

        }
         [HttpPost("registerpatient")]
        public async Task<ActionResult> register([FromForm] RegisterPatient patient)
        {
            var auser = _context.users.FirstOrDefault(u=>u.username == patient.username);
            if (auser != null)
            {
                 return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Username already exists!" });

            } 
            if (patient.password != patient.confirmpassword)
            {
                return BadRequest(new {message = "password and confrim password must match"});
            }
            User user = _mapper.Map<User>(patient);
            user.role = Roles.patient;
            user.isApproved = true;
            user.isActive = true;
            user.dateRegistered = DateTime.Now;
            user.lastModified = DateTime.Now;
            if (patient.ImageFile != null) user.imageName = await SaveImage(patient.ImageFile);
            _context.users.Add(user);
            await _context.SaveChangesAsync();
            // return Ok(_mapper.Map<Patient>(user));
            return Ok(new {message= "register successfull"});
        }
        
         [HttpPost("registerdoctor")]
        public async Task<ActionResult> register([FromForm] AddDoctor doctor)
        {
            var auser = _context.users.FirstOrDefault(u=>u.username == doctor.username);
            if (auser != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User already exists!" });

            } 
             if (doctor.password != doctor.confirmpassword)
            {
                return BadRequest(new {message = "password and confrim password must match"});
            }
            User user = _mapper.Map<User>(doctor);
            user.role = Roles.doctor;
            user.dateRegistered = DateTime.Now;
            user.lastModified = DateTime.Now;
            user.isApproved = false;
            if (doctor.ImageFile != null)
            {
                user.imageName = await SaveImage(doctor.ImageFile);
            }
            _context.users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new {message= "register successfull"});
        }
         

        [HttpGet("getuser")]
        public async Task<ActionResult<IEnumerable<User>>> GetUser(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user is null)
            {
                return NotFound("user does not exist");
            }
            if (user.role == Roles.patient) return Ok(_mapper.Map<Patient>(user));
           if (user.role == Roles.doctor) return Ok(_mapper.Map<Doctor>(user));
            
            return Ok(_mapper.Map<Admin>(user));
        }

          [HttpPut("updatedoctor")]
        public async Task<ActionResult<IEnumerable<User>>> UpdateDoc(int userId,[FromForm] UpdateDoctor doctor)
        {
             if (userId != doctor.userId)
             {
                 return BadRequest("data miss match");
             }
          var user = await _context.users.FindAsync(userId);

          if (user is null)
          {
            return NotFound("user does not exist");
            
          }
          if(doctor.ImageFile != null) 
          {
                if(!String.IsNullOrEmpty(user.imageName)) DeleteImage(user.imageName);
                user.imageName = await SaveImage(doctor.ImageFile);
            }
          user.fullName = doctor.fullName;
          user.username = doctor.username;
          user.password = doctor.password;
          user.email = doctor.email;
          user.telephone = doctor.telephone;
          user.gender = doctor.gender;
          user.dept = doctor.dept;
          user.specialisation = doctor.specialisation;
            user.hospitalRefId = doctor.hospitalRefId;
            user.address = doctor.address;
            user.lastModified = DateTime.Now;
            _context.users.Update(user);
          try
          {
            await _context.SaveChangesAsync();
            
          }
          catch (DbUpdateConcurrencyException)
          {
             throw;
          }
          return Ok(user);
        }
        
          [HttpPut("updateuser")]
        public async Task<ActionResult<IEnumerable<Admin>>> PutAdmin([FromForm] int id, UpdateAdmin updatedAdmin)
        {
          if (id != updatedAdmin.userId)
          {
                return BadRequest("unmatch user infor");
            }
          
          var user = await _context.users.FirstOrDefaultAsync(u=>u.userId == updatedAdmin.userId);
          if (user == null)
          {
            return NotFound("your account does not exist");
            
          }
            if (await _context.users.FirstOrDefaultAsync(u => u.userId != updatedAdmin.userId && u.username == updatedAdmin.username && !u.isDeleted) != null)
            {
                return BadRequest("user name already exist");
            }
            user.email = updatedAdmin.email;
            user.fullName = updatedAdmin.fullName;
            user.username = updatedAdmin.username;
            user.password = updatedAdmin.password;
            user.telephone = updatedAdmin.telephone;
            user.gender = updatedAdmin.gender;
            user.role = updatedAdmin.role;
            user.isApproved = updatedAdmin.isApproved;
            user.address = updatedAdmin.address;
            user.lastModified = DateTime.Now;
           if (updatedAdmin.ImageFile != null)
           {
                if(!String.IsNullOrEmpty(user.imageName)) DeleteImage(user.imageName);
                user.imageName = await SaveImage(updatedAdmin.ImageFile);
            }
            _context.users.Update(user);
            // _context.Entry(user).State = EntityState.Modified;
          try
          {
            await _context.SaveChangesAsync();
            
          }
          catch (DbUpdateConcurrencyException)
          {
             throw;
          }
            Admin admin = _mapper.Map<Admin>(user);
            return Ok(admin);
        }
     
       [HttpPatch("setavailability")]
       public async Task<IActionResult> SetAvailability(int doctorRefId){
            var doctor = await _context.users.FindAsync(doctorRefId);
            if (doctor != null)
            {
                doctor.isActive = !doctor.isActive;
                _context.users.Update(doctor);
                await _context.SaveChangesAsync();
                return Ok(_mapper.Map<GetDoctor>(doctor));
            }
            return NotFound("User does not exist");
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatient(int id)
        {
            var user = _context.users.FirstOrDefault(x=>x.userId == id);
            if (user is  null)
            {
             return NotFound("user does not exist");
                
            }
            user.dateDeleted = DateTime.Now;
            user.isDeleted = true;
            _context.users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new  { status ="sucess", message = "user sucessfully deleted"});
        }
        private bool UserExist(int id)
        {
            return _context.users.Any(x=>x.userId == id);
        }
         private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
         [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
