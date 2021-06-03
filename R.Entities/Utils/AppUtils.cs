using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using R.Entities.Extensions;

namespace R.Entities.Utils
{
    public class AppUtils
    {
        public static List<long> AnonymousUniqueIds = new List<long>();

        public static string GetFileExtension(string fileAcceptation)
        {
            if (string.IsNullOrEmpty(fileAcceptation))
            {
                return string.Empty;
            }

            return fileAcceptation.Substring(fileAcceptation.IndexOf("/") + 1);
        }

        public static string ParseFullName(string firstName, string lastName)
        {
            return (firstName + " " + lastName).Trim();
        }

        public static string ParseCountNumber(int count, string suffix, string final = "s")
        {
            return count.ToString() + " " + suffix + (count > 1 ? final : "");
        }

        public static long GenerateUniqueId()
        {
            var timeStamp = DateTime.UtcNow.ToTimeStamp();
            var randValue = new Random().Next(10000);

            return timeStamp + randValue * 1000;
        }

        public static string Sha256Hash(string data)
        {
            using (var sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

                // Convert byte array to a string   
                var builder = new StringBuilder();
                for (var i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool IsEmail(string input)
        {
            return true;
        }

        public static string ParseUserPage(string uniqueName)
        {
            return "/" + uniqueName;
        }
        
        public static string ParseVotePage(long uniqueId)
        {
            return "/vote/" + uniqueId;
        }

        public static string GetFileExtensionFromBase64(string imgBase64)
        {
            //data:image/png;base64,
            return imgBase64.Substring(11, imgBase64.IndexOf(";base64,") - 11);
        }
    }
}
