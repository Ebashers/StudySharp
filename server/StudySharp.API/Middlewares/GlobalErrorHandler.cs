using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StudySharp.Domain.General;

namespace StudySharp.API.Middlewares
{
    public class GlobalErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandler> _logger;
        private readonly IWebHostEnvironment _env;

        public GlobalErrorHandler(
            RequestDelegate next,
            ILogger<GlobalErrorHandler> logger,
            IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var errorMessages = exception.Errors.Select(e => e.ErrorMessage).ToList();
                if (!errorMessages.Any())
                {
                    errorMessages = new List<string>
                    {
                        exception.Message,
                    };
                }

                var result = JsonConvert.SerializeObject(
                    OperationResult.Fail(errorMessages),
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    });
                response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                await response.WriteAsync(result);
            }
            catch (Exception error)
            {
                _logger.LogCritical(error, "Unexpected error occured");
                var response = context.Response;
                response.ContentType = "application/json";

                var errorMessage =
                    _env.IsProduction()
                        ? "Oops... Unexpected error occured"
                        : BuildDeveloperErrors(error);
                var result = JsonConvert.SerializeObject(
                    OperationResult.Fail(errorMessage),
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    });
                response.StatusCode = StatusCodes.Status500InternalServerError;
                await response.WriteAsync(result);
            }
        }

        private static string BuildDeveloperErrors(Exception e)
        {
            var messages = new List<string?>();

            FetchExceptionMessages(e);

            return $"{string.Join(Environment.NewLine, messages)}{Environment.NewLine}{e.StackTrace}";

            void FetchExceptionMessages(Exception? ex)
            {
                while (ex != null)
                {
                    messages.Add($"{ex.GetType().FullName}: {ex.Message}");
                    ex = ex.InnerException;
                }
            }
        }
    }
}
