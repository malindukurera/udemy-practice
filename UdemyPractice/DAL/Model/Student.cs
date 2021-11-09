using System;
using DAL.Model.Interfaces;

namespace DAL.Model
{
    public class Student : ISoftDeletable, ITrackable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastUpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
