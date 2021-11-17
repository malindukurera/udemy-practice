﻿using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model;

namespace DAL.ResponseViewModel
{
    public class StudentCourseViewModel
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CreateAt { get; set; }
        public string UpdateAt { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}
