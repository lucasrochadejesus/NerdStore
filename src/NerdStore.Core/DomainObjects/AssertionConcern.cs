using System.Text.RegularExpressions;

namespace NerdStore.Core.DomainObjects
{
    public class AssertionConcern
    {

        #region Validations Equals or Not
        public static void ValidateIfEquals(object obj1, object obj2, string message)
        {
            if (obj1.Equals(obj2))
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfNotEquals(object obj1, object obj2, string message)
        {
            if (!obj1.Equals(obj2))
            {
                throw new DomainException(message);
            }
        }

        #endregion

        #region Validations Characters

        public static void ValidateCharacters(string value, int max, string message)
        {
            var length = value.Length;
            if (length > max)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateCharacters(string value, int min, int max, string message)
        {
            var length = value.Trim().Length;
            if (length < min || length > max)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateExpression(string pattern, string value, string message)
        {
            var regex = new Regex(pattern);
            if (!regex.IsMatch(value))
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateEmpty(string value, string message)
        {
            if (value == null || value.Trim().Length == 0)
            {
                throw new DomainException(message);
            }
        }


        #endregion

        #region Validations Numbers and Nulls

        public static void ValidateIfZero(object obj1, object obj2, string message)
        {

        }

        public static void ValidateIfNull(object obj1, string message)
        {
            if (obj1 == null)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfLowerMin(object obj1, int num, string message)
        {
            if (num < 1)
            {
                throw new DomainException(message);
            }
        }


        #endregion

    }
}
