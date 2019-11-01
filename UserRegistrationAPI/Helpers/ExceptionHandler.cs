using System;
using System.Globalization;

namespace UserRegistrationAPI.Helpers
{
    
        public class ExceptionHandler : Exception
    {
        public ExceptionHandler() : base() {}

        public ExceptionHandler(string message) : base(message) { }

        public ExceptionHandler(string message, params object[] args) 
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}