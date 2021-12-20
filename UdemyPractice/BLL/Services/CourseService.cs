using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Request;
using DAL.Model;
using DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Utility.Exceptions;

namespace BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;

        public CourseService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _uow = unitOfWork;
            _configuration = configuration;
        }

        public async Task<List<Course>> GetAllAsync()
        {
            var list = await _uow.CourseRepository.GetList();

            var enumerable = list.Select(c =>
            {
                c.ImageUrl = _configuration.GetSection("MediaServer:ImageAccessUrl").Value + c.ImageUrl;
                return c;
            }).ToList();

            return enumerable;
        }

        public async Task<Course> GetByAsync(string code)
        {
            var course = await _uow.CourseRepository.FindSingleAsync(x => x.Code == code);
            
            if (course == null)
            {
                throw new ApplicationValidationException("course not found");
            }
            
            course.ImageUrl = _configuration.GetSection("MediaServer:ImageAccessUrl").Value + course.ImageUrl;

            return course;
        }

        public async Task<Course> AddAsync(CourseInsertRequestViewModel request)
        {
            var course = new Course();
            course.Code = request.Code;
            course.Name = request.Name;
            course.Credit = request.Credit;
            course.ImageUrl = await ForImageUploadAsync(request.CourseImage);

            await _uow.CourseRepository.CreateAsync(course);

            if (await _uow.CourseRepository.SaveCompletedAsync())
            {
                course.ImageUrl = _configuration.GetSection("MediaServer:LocalImageStorage").Value + course.ImageUrl;
                return course;
            }

            throw new ApplicationValidationException("Error inserting course");
        }

        private async Task<string> ForImageUploadAsync(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName) ?? ".png";
            var filename = Guid.NewGuid().ToString() + ext;

            var imagePath = _configuration.GetSection("MediaServer:LocalImageStorage").Value;
            var path = Path.Combine(Directory.GetCurrentDirectory(), imagePath, filename).ToLower();
            await using var bits = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(bits);
            bits.Close();
            return filename;
        }

        public async Task<Course> UpdateAsync(string code, CourseInsertRequestViewModel request)
        {
            var dept = await _uow.CourseRepository.FindSingleAsync(x => x.Code == code);
            if (dept == null)
            {
                throw new ApplicationValidationException("course not found");
            }

            if (!String.IsNullOrEmpty(request.Code))
            {
                var existAlreadyCode = await _uow.CourseRepository.FindSingleAsync(x => x.Code == request.Code);
                if (existAlreadyCode != null)
                {
                    throw new ApplicationValidationException("Your updated Code already present in our system!");
                }

                dept.Code = request.Code;
            }

            if (!String.IsNullOrEmpty(request.Name))
            {
                var existAlreadyName = await _uow.CourseRepository.FindSingleAsync(x => x.Name == request.Name);
                if (existAlreadyName != null)
                {
                    throw new ApplicationValidationException("Your updated Name already present in our system!");
                }

                dept.Name = request.Name;
            }

            _uow.CourseRepository.Update(dept);
            if (await _uow.CourseRepository.SaveCompletedAsync())
            {
                return dept;
            }

            throw new ApplicationValidationException("Some problem for update data");
        }

        public async Task<Course> DeleteAsync(string code)
        {
            var course = await _uow.CourseRepository.FindSingleAsync(x=>x.Code == code);
            if (course == null)
            {
                throw new ApplicationValidationException("course not found");
            }

            _uow.CourseRepository.Delete(course);

            if (await _uow.CourseRepository.SaveCompletedAsync())
            {
                return course;
            }

            throw new ApplicationValidationException("Some problem for delete data");
        }

        public async Task<bool> IsCodeExists(string code)
        {
            var dept = await _uow.CourseRepository.FindSingleAsync(x => x.Code == code);
            if (dept == null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsNameExists(string name)
        {
            var dept = await _uow.CourseRepository.FindSingleAsync(x => x.Name == name);
            if (dept == null)
            {
                return true;
            }

            return false;
        }
        public async Task<bool> IsIdExists(int id)
        {
            var dept = await _uow.CourseRepository.FindSingleAsync(x => x.Id == id);
            if (dept == null)
            {
                return true;
            }

            return false;
        }
    }
}
