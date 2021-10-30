using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Utility.Exceptions;
using Utility.Models;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _env);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, 
            Exception exception, IWebHostEnvironment env)
        {
            var code = HttpStatusCode.InternalServerError;
            var error = new ApiErrorResponse(){StatusCode = (int) code};

            if (_env.IsDevelopment())
            {
                error.Details = exception.StackTrace;
            }
            else
            {
                error.Details = exception.Message;
            }

            switch (exception)
            {
                case ApplicationValidationException e:
                    error.Message = e.Message;
                    error.StatusCode = (int) HttpStatusCode.UnprocessableEntity;
                    break;
                default:
                    error.Message = "Something went wrong in our system";
                    break;
            }

            var result = JsonConvert.SerializeObject(error);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.StatusCode;

            await context.Response.WriteAsync(result);
        }
    }
}
