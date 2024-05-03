using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Utilities
{
    public class Validations
    {

        /// <summary>
        /// Validate EmailAddress
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public static Boolean ValidateEmailAddress(object emailAddress)
        {
            //string regexPattern =  @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            //string regexPattern = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";

            string pattern = @"^(([^<>()[\]\\.,;:\s@\""]+"
                       + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                       + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                       + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                       + @"[a-zA-Z]{2,}))$";

            if (emailAddress == null) return true;
            return Regex.Match(emailAddress.ToString(), pattern).Success;
        }

        /// <summary>
        /// Validate URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Boolean ValidateURL(object url)
        {

            if (url == null) return true;

            string testUrl = url.ToString();

            Uri tryuri = null;
            return Uri.TryCreate(testUrl, UriKind.Absolute, out tryuri);

        }


        /// <summary>
        /// Validate Required Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean ValidateRequired(object entity)
        {
            if (entity == null) return false;
            if (entity.ToString().Length == 0) return false;
            return true;
        }

        /// <summary>
        /// Validate Required Entity Guid
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean ValidateRequiredGuid(object entity)
        {
            if (entity == null) return false;

            Guid testGuid;
            if (Guid.TryParse(entity.ToString(), out testGuid) == false) return false;
            if (testGuid == Guid.Empty) return false;

            return true;
        }


        /// <summary>
        /// Validate Length
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static Boolean ValidateLength(object entity, int maxLength)
        {
            if (entity == null) return true;
            if (entity.ToString().Length > maxLength) return false;
            return true;
        }

        /// <summary>
        /// Validate Greater Than Zero
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean ValidateGreaterThanZero(object entity)
        {
            if (entity == null) return false;

            if (IsInteger(entity) == false) return false;

            int test = Convert.ToInt32(entity);
            if (test < 1) return false;

            return true;
        }

        /// <summary>
        /// Validate Decimal Greater Than Zero
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean ValidateDecimalGreaterThanZero(object entity)
        {
            if (entity == null) return false;

            if (IsDecimal(entity) == false) return false;

            decimal test = Convert.ToDecimal(entity);
            if (test < 1) return false;

            return true;
        }

        /// <summary>
        /// Validate Decimal Is Not Zero
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean ValidateDecimalIsNotZero(object entity)
        {
            if (entity == null) return false;

            if (IsDecimal(entity) == false) return false;

            decimal test = Convert.ToDecimal(entity);
            if (test == 0) return false;

            return true;
        }

        /// <summary>
        /// Check if date is a valid format
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static Boolean IsDate(string date)
        {
            DateTime dateTime;
            return DateTime.TryParse(date, out dateTime);
        }

        /// <summary>
        /// IsNumeric
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean IsNumeric(object entity)
        {
            if (entity == null) return false;

            int result;
            return int.TryParse(entity.ToString(), out result);
        }

        /// <summary>
        /// IsDouble
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean IsDouble(object entity)
        {
            if (entity == null) return false;

            string e = entity.ToString();

            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = e.IndexOf(".", i)) != -1)
            {
                i += ".".Length;
                count++;
            }
            if (count > 1) return false;

            e = e.Replace(".", "");

            int result;
            return int.TryParse(e, out result);
        }

        /// <summary>
        /// IsInteger
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean IsInteger(object entity)
        {
            if (entity == null) return false;

            int result;
            return int.TryParse(entity.ToString(), out result);
        }

        /// <summary>
        /// IsDecimal
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean IsDecimal(object entity)
        {
            if (entity == null) return false;

            decimal result;
            return decimal.TryParse(entity.ToString(), out result);
        }

        /// <summary>
        /// Is Date
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean IsDate(object entity)
        {
            if (entity == null) return false;
            return IsDate(entity.ToString());
        }

        /// <summary>
        /// Is Date or Null Date
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean IsDateOrNullDate(object entity)
        {
            if (entity == null) return true;
            return IsDate(entity.ToString());
        }

        /// <summary>
        /// Is Date Greater than default Date
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean IsDateGreaterThanDefaultDate(object entity)
        {
            if (entity == null) return false;
            if (IsDate(entity.ToString()) == false) return false;

            DateTime testDate = Convert.ToDateTime(entity.ToString());
            long test = testDate.Ticks;
            if (test == 0) return false;

            return true;

        }

        /// <summary>
        /// Is Date Greater than default Date
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean IsDateGreaterThanOrEqualToToday(object entity)
        {
            if (entity == null) return false;
            if (IsDate(entity.ToString()) == false) return false;

            DateTime testDate = Convert.ToDateTime(entity.ToString());
            if (testDate < DateTime.Today) return false;

            return true;

        }

        /// <summary>
        /// used to check image extension
        /// </summary>
        /// <param name="imageName">Can be image name, image path</param>
        /// <returns></returns>
        public static bool IsValidImageExtension(string imageName)
        {
            return Regex.IsMatch(imageName, @"(.*?)\.(jpg|jpeg|png|gif|bmp)$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IsValidMultipleEmail(string value)
        {
            Regex regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            string wrongEmailAddress = string.Empty;
            string[] emails = value.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return emails.Where(email => !regex.IsMatch(email)).Aggregate(wrongEmailAddress, (current, email) => current + (email + ",")).TrimEnd(',');
        }
    }
}
