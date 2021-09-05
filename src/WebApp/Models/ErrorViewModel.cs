using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace WebApp.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
