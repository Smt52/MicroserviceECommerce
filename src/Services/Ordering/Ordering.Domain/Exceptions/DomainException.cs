using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ordering.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException()
        {
        }

        public DomainException(string? message) : base($"Domain Exception: {message} throws for Domain Layer")
        {
        }

        public DomainException(string? message, Exception? innerException) : base($"Domain Exception: {message} throws for Domain Layer and innerException is:{innerException}")
        {
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(new
            {
                Message = Message,
                StackTrace = StackTrace,
            });
        }

        public static DomainException FromJson(string json)
        {
            var data = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            var exception = new DomainException(data?["Message"]);
            return exception;
        }
    }
}