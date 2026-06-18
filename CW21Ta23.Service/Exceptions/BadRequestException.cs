namespace CW21Ta23.Service.Exceptions;

public class BadRequestException : BaseAppException
{
  
        public BadRequestException(string message)
            : base(message, 400)
        {
        }

        public static BadRequestException Required(string fieldName)
        {
            return new BadRequestException($"{fieldName} is required.");
        }

        public static BadRequestException TooLong(string fieldName, int maxLength)
        {
            return new BadRequestException($"{fieldName} cannot exceed {maxLength} characters.");
        }
    }
