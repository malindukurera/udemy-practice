using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model.Interfaces
{
    public interface ITrackable
    {
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset LastUpdatedAt { get; set; }
        string CreatedBy { get; set; }
        string LastUpdatedBy { get; set; }
    }
}
