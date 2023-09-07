using System.Text;

namespace ShipmentService.BusinessLogic.Helpers
{
    /// <summary>
    /// helper fuctions class to handle additional operation on the shipment entity
    /// </summary>
    public static class ShipmentHelperFunctions
    {
        private static Random random = new Random();
        /// <summary>
        /// function to generate a tracking number
        /// </summary>
        /// <returns></returns>
        public static string TrackingNumberGenerator()
        {
        const string upperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
           const string numbers = "0123456789";
           StringBuilder trackingNumber = new StringBuilder();
            // AddAsync two random uppercase letters at the beginning
            trackingNumber.Append(upperCaseLetters[random.Next(0, upperCaseLetters.Length)]);
            trackingNumber.Append(upperCaseLetters[random.Next(0, upperCaseLetters.Length)]);
            // AddAsync eight random numbers in the middle
            for (int i = 0; i < 8; i++)
            {
                trackingNumber.Append(numbers[random.Next(0, numbers.Length)]);
            }
            // AddAsync two random uppercase letters at the end
            trackingNumber.Append(upperCaseLetters[random.Next(0, upperCaseLetters.Length)]);
            trackingNumber.Append(upperCaseLetters[random.Next(0, upperCaseLetters.Length)]);
            return trackingNumber.ToString();
        }
    }
}

