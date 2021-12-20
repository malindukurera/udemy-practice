using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model.Interfaces;

namespace DAL.Model
{
    public class Course : ISoftDeletable, ITrackable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Credit { get; set; }
        public string  ImageUrl { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastUpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }

        public ICollection<CourseStudent> CourseStudents { get; set; }
    }
}
