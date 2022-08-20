using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetModels;

public class GetPatientAppointment
{
    public string? reason { get; set; }
    public DateTime appDate { get; set; }
    public string? time { get; set; }
}