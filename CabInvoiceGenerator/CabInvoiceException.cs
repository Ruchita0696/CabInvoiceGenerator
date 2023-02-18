using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    public class CabInvoiceException : Exception
    {
        ExceptionType type;
        public enum ExceptionType //enum for exception type
        {
            INVALID_RIDE_TYPE,
            INVALID_DISTANCE,
            INVALID_TIME,
            INVALID_USER_ID,
            NULL_RIDES
        }
        /// <summary>
        /// Paramet constructor for setting exception type and throwing exception.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        public CabInvoiceException(ExceptionType type, string message) : base(message)
        {
            this.type = type;
        }
    }
}
