using Microsoft.AspNetCore.Mvc;

namespace Api.Models
{
    public class CustomProblemDetails : ProblemDetails
    {
        public string[]? Errors { get; set; }
    }
}