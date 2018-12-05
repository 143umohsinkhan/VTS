using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Utility
{
    /// <summary>
    /// Extension methods
    /// </summary>
    public static class Helper
    {
        public static bool VTSEquals(this string value, string compareWith)
        {
            if (value.IsNull()) throw new NullReferenceException();
            return value.ToLower().Equals(compareWith.ToLower());
        }
        public static bool IsNull(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }

        public static string ToStringSafe(this object value)
        {
            if (value == null) return string.Empty;
            return Convert.ToString(value);
        }

        public static string GetName(this string Email)
        {
            if (Email.IsNull()) throw new NullReferenceException();
            return Email.Split('@').First();
        }

        public static int ToGenderConversion(this string gender)
        {
            return gender.ToLower().Equals("male") ? 1 : 0;
        }

        public static long Tolong(this object value)
        {
            if (value == null) return 0;
            long output = 0;
            return long.TryParse(value.ToStringSafe(), out output) ? Convert.ToInt64(value) : 0;
        }
    }

    public class VTSConstants
    {
        public const string UploadFileFolderName = "UploadedFiles";
        public const string Error = "Something went wrong, Please try again later";
        public const string Admin = "Admin";
        public const string User = "User";
        public static string UserID = "UserID";
        public static string QuestionKey = "QuestionKey";
    }

    public class CryptoEngine
    {
        const string key = "brayn@ayanhn8-sq";
        public static string Encrypt(string input)
        {
            try
            {
                byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public static string Decrypt(string input)
        {
            try
            {
                byte[] inputArray = Convert.FromBase64String(input);
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
