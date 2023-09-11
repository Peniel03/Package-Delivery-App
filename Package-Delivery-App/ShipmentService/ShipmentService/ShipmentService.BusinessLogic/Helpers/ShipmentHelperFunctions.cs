using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ShipmentService.BusinessLogic.Helpers
{
    public static class ShipmentHelperFunctions
    {
        private static Random random = new Random();

        public static string TrackingNumberGenerator()
        {
 
        const string upperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
           const string numbers = "0123456789";
           StringBuilder trackingNumber = new StringBuilder();

            // Add two random uppercase letters at the beginning
            trackingNumber.Append(upperCaseLetters[random.Next(0, upperCaseLetters.Length)]);
            trackingNumber.Append(upperCaseLetters[random.Next(0, upperCaseLetters.Length)]);

            // Add eight random numbers in the middle
            for (int i = 0; i < 8; i++)
            {
                trackingNumber.Append(numbers[random.Next(0, numbers.Length)]);
            }

            // Add two random uppercase letters at the end
            trackingNumber.Append(upperCaseLetters[random.Next(0, upperCaseLetters.Length)]);
            trackingNumber.Append(upperCaseLetters[random.Next(0, upperCaseLetters.Length)]);

            return trackingNumber.ToString();

        }
}
}

