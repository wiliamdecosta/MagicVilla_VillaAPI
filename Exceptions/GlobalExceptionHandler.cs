﻿
using MagicVilla_DB.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MagicVilla_DB.Exceptions
{
    public class GlobalExceptionHandler : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) 
        {  
            _logger = logger; 
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine("Pesan : " + ex.Message);
                System.Diagnostics.Debug.WriteLine("Contoh Exception");
                
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var problem = BaseResponse<string?>.Builder()
                   .Code(StatusCodes.Status400BadRequest)
                   .Message(ex.Message)
                   .Data(null);

                if (ex is InvalidRequestValueException)
                {
                   var exception = ex as InvalidRequestValueException;
                   context.Response.StatusCode = StatusCodes.Status400BadRequest;
                   problem.Errors(exception?.Errors ?? []);
                   problem.Message(exception?.Message ?? "");
                }

                string json = problem.ToJSONString();

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);

            }
        }
    }
}
