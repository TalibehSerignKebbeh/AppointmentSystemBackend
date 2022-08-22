using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using appointmentApi.Models;
using appointmentApi.Models.Entities;
using Models;

namespace appointmentApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Patient, User>();
            CreateMap<Doctor, User>();
            CreateMap<Admin, User>();

            CreateMap<User, Patient>();
            CreateMap<User, Doctor>();
            CreateMap<User, Admin>();
            CreateMap<User, GetPatient>();
            CreateMap<User, GetDoctor>();
            CreateMap<GetDoctor, User>();

            CreateMap<User, GetPatientDoctor>();


            CreateMap<Appointment, AppointmentModel>();
            CreateMap<AppointmentModel, Appointment>();

            CreateMap<UpdateAdmin, User>();

            CreateMap<AddUser, User>();
            CreateMap<AddDoctor, User>();


            CreateMap<User, GetAppDoctor>();
            CreateMap<GetAppDoctor, User>();


            CreateMap<User, GetAppPatient>();
            CreateMap<GetAppPatient, User>();

            CreateMap<RegisterPatient, User>();

            CreateMap<AddHospital, Hospital>();

        }
        
    }
}