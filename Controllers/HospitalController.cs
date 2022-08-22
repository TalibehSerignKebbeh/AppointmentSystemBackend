using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using appointmentApi.Models.data;
using Microsoft.EntityFrameworkCore;
using appointmentApi.Models;
using appointmentApi.Models.Entities;
using Models;
using AutoMapper;

namespace appointmentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    
    public class HospitalController : ControllerBase
    {
    private readonly AppointmentDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;
        public HospitalController
        (
           AppointmentDbContext context, IMapper mapper, IWebHostEnvironment hostEnvironment
        )
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerator<Hospital>>> GetAll()
        {
            var hospitals = await _context.hospitals.ToListAsync();

            foreach (var hospital in hospitals)
            {
                hospital.appointments = await  _context.appoinments.Where(ap => ap.hospitalRefId == hospital.hospitalId).ToListAsync();
                hospital.doctors = await _context.users.Where(u=>u.hospitalRefId == hospital.hospitalId).Select(u =>  _mapper.Map<GetDoctor>(u)).ToListAsync();

            }
            return Ok(hospitals);

        }
      
        [HttpPost("add")]
        public async Task<ActionResult> PostHospital([FromForm] AddHospital hospital)
        {
            if (hospital.ImageFile != null)
            {
                hospital.imageName = await SaveImage(hospital.ImageFile);
            }
            _context.hospitals.Add(_mapper.Map<Hospital>(hospital));
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<ActionResult<Hospital>> UpdateHospital([FromForm] UpdateHospital hospital )
        {
            var ahospital = await _context.hospitals.FindAsync(hospital.hospitalId);
            if(ahospital == null) return NotFound();
            if (hospital.ImageFile is not null)
            {
                if(!String.IsNullOrEmpty(hospital.imageName)) DeleteImage(hospital.imageName);
                hospital.imageName = await SaveImage(hospital.ImageFile);
            }
            ahospital.name = hospital.name;
            ahospital.category = hospital.category;
            ahospital.region = hospital.region;
            ahospital.district = hospital.district;
            ahospital.location = hospital.location;
            ahospital.mapKey = hospital.mapKey;
            ahospital.imageName = hospital.imageName;
            _context.hospitals.Update(ahospital);
            await _context.SaveChangesAsync();
            return Ok();
        }
        
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteHospital(int hospitalId)
        {
            var hospital = await _context.hospitals.FindAsync(hospitalId);
            if(hospital is null) return BadRequest();
            hospital.isDeleted = true;
            _context.hospitals.Update(hospital);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Hospital deleted successfully" });
        }

          [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string tempName = Path.GetFileName(imageFile.FileName);
            
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
