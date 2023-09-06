using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.BusinessLogic.Exceptions
{
    /// <summary>
    /// The not found exception 
    /// </summary>
    public class NotFoundException:Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NotFoundException"/>
        /// </summary>
        public NotFoundException()
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="NotFoundException"/>
        /// </summary>
        /// <param name="message">The message  passed in the exception</param>
        public NotFoundException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="NotFoundException"/>
        /// </summary>
        /// <param name="message">The message passed in the exception <param>
        /// <param name="exception"></param>
        public NotFoundException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
