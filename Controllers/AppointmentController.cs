using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using appointmentApi.Models.data;
using appointmentApi.Models;
using Models;
using AutoMapper;


namespace appointmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController :ControllerBase
    {
        private readonly AppointmentDbContext _context;
        private readonly IMapper _mapper;

        public AppointmentController(AppointmentDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<IEnumerable<Appointment>> GetAll(){
         var appointments =  await _context.appoinments.ToListAsync();
        
         foreach(Appointment app in appointments){
                app.doctor = _mapper.Map<GetAppDoctor>(_context.users.Find(app.doctorRefId));
                app.patient = _mapper.Map<GetAppPatient>(_context.users.Find(app.patientRefId));
                app.hospital = _context.hospitals.Find(app.hospitalRefId);
            }
            return appointments;
        }

        [HttpPost("bookappointment")]
        public async Task<ActionResult<AppointmentModel>> AddPointment(int userId, AppointmentModel appointmentModel)
        {
           if (appointmentModel.patientRefId != userId)
           {
                return BadRequest("Data miss match");
            }
        if (appointmentModel.appDate.Date < DateTime.Now.Date)
        {
                // return BadRequest("choose proper date");
                throw new DataException("Data cannot be less than today's data");

            }
            Appointment appointment = _mapper.Map<Appointment>(appointmentModel);
            var doctor = await _context.users.FindAsync(appointment.doctorRefId);
            appointment.doctor = _mapper.Map<GetAppDoctor>(doctor);
            _context.appoinments.Add(appointment);
            await _context.SaveChangesAsync();
            return Ok(appointment);
      
        }
        [HttpGet("PatientAppointments")]
        public async Task<ActionResult<GetPatientApp>> GetPatientApps( int userid)
        {
           var appointments = await _context.appoinments.Where(a=> a.patientRefId == userid && !a.isDeleted).ToListAsync();
            appointments.ForEach(app => app.doctor = _mapper.Map<GetAppDoctor>(_context.users.Find(app.doctorRefId)));
            if (appointments is not null)
            {
              return Ok(appointments);
            }
            return NoContent();
        }
        [HttpGet("doctorsapps")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentForDoctor(int doctorRefId)
        {
            
          var appoinments = await _context.appoinments.Where(a=>!a.isCancell && a.doctorRefId == doctorRefId && !a.isDeleted).ToListAsync();
            appoinments.ForEach(app => app.patient = _mapper.Map<GetAppPatient>(_context.users.Find(app.patientRefId)));
            return appoinments;
        }
        
        [HttpPost("doctorapps")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetDoctorApps(int doctorRefId)
        {
            var doctor = await _context.users.FirstOrDefaultAsync(d=>d.userId == doctorRefId);
            if (doctor is null)
            {
                return NotFound($"Doctor with {doctorRefId} is not found");

            }
            var appoinments = await _context.appoinments.Where(a=>!a.isCancell && a.doctorRefId == doctorRefId && !a.isDeleted).ToListAsync();
            appoinments.ForEach(app => app.patient = _mapper.Map<GetAppPatient>(_context.users.Find(app.patientRefId)));
            return appoinments;
        }

        [HttpPut("update")]
        public async Task<ActionResult<IEnumerable<Appointment>>> UpdateAppointment(int id, Appointment appointment)
        { 
            if (id != appointment.appId)
            {
                return BadRequest("unmatch data");
            }
            var theAppointment = await _context.appoinments.FindAsync(id);
            if (theAppointment == null)
            {
                return NotFound("appointment not found in the database");
            }
            theAppointment.reason = appointment.reason;
            theAppointment.appDate = appointment.appDate;
            theAppointment.doctorRefId = appointment.doctorRefId;
            theAppointment.patientRefId = appointment.patientRefId;
            theAppointment.isComplete = appointment.isComplete;

            _context.appoinments.Update(theAppointment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DBConcurrencyException ex)
            {
                 throw(ex);
            }
            return Ok(new {message="appointment successfuly updated"});
        }
        [HttpDelete("deleteApp")]
        public async Task<IActionResult> deleteApp(int appId){
         var appointment = await _context.appoinments.FirstOrDefaultAsync(app=>app.appId ==appId);
         if (appointment is null)
         {
            return BadRequest("appointment does not exists");
            
         }
            appointment.isDeleted = true;
             _context.appoinments.Update(appointment);
            await _context.SaveChangesAsync();
         return Ok(new {message="successfuly deleted"});

        }
        [HttpPatch("rejectOrAccept")]
        public async Task<IActionResult> MarkApp(int appId){
            var app = await _context.appoinments.FindAsync(appId);
            if (app is not null)
            {
                app.isAccepted = !app.isAccepted;
                // app.isCancell = false;
                _context.appoinments.Update(app);
                await _context.SaveChangesAsync();
            return Ok(app);
            }
            return NotFound("Appointment does not exist in current context");
        }
        
        [HttpPatch("rejectapp")]
        public async Task<IActionResult> Reject(int appId){
            var app = await _context.appoinments.FindAsync(appId);
            if (app is not null)
            {
                app.isCancell = true;
                _context.appoinments.Update(app);
                await _context.SaveChangesAsync();
            return Ok(app);
            }
            return NotFound("Appointment does not exist in current context");
        }
        
        // public GetDoctor AppDoctor(int doctorRefId)
        // {
        //     var user = _context.users.Find(doctorRefId);
        //     return _mapper.Map<GetDoctor>(user);
            
        // }
        // [HttpPut]
        // [Route("refer")]
        // public async Task<ActionResult<IEnumerable<Appointment>>> ReferAppointment([FromBody]int id, Appointment appointment, User doctor)
        // { 
        //     var curDoctor = _context.users.FirstOrDefault(d=>d.userId == doctor.userId && doctor.role == "doctor");
        //     if (id != appointment.appId)
        //     {
        //         return BadRequest("unmatch data");
                
        //     }
        //     if (curDoctor is null)
        //     {
        //         return BadRequest("either your account is not approved or it does not exist");
        //     }
        //     var theAppointment = _context.appoinments.Find(id);
        //     if (theAppointment is null)
        //     {
        //         return NotFound("appointment not found in the database");
        //     }
        //     theAppointment.reason = appointment.reason;
        //     theAppointment.appDate = appointment.appDate;
        //     theAppointment.isComplete = appointment.isComplete;
        //     theAppointment.doctorRefId = appointment.doctorRefId;
        //     theAppointment.patientRefId = appointment.patientRefId;
        //     theAppointment.isComplete = appointment.isComplete;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DBConcurrencyException ex)
        //     {
        //          throw(ex);
        //     }
        //     return NoContent();


        // }

    }
}