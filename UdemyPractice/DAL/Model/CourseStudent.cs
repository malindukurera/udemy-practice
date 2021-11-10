using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model.Interfaces;

namespace DAL.Model
{
    public class CourseStudent : ISoftDeletable, ITrackable
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastUpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
