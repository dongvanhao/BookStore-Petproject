using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
        public ValidationException(string message, IEnumerable<string> errors) : base(message)
        {
            Errors = errors;
        }
        public IEnumerable<string> Errors { get; } = new List<string>();
    }
    
    
}
