using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.BusinessLogic.Exceptions
{
    /// <summary>
    /// the already exist exception 
    /// </summary>
    public class AlreadyExistException:Exception 
    {

        /// <summary>
        /// Initializes a new instance of <see cref="NotFoundException"/>
        /// </summary>
        public AlreadyExistException()
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="NotFoundException"/>
        /// </summary>
        /// <param name="message">The message  passed in the exception</param>
        public AlreadyExistException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="NotFoundException"/>
        /// </summary>
        /// <param name="message">The message passed in the exception <param>
        /// <param name="exception"></param>
        public AlreadyExistException(string message, Exception exception) : base(message, exception)
        {

        }

    }
}
