using Core.Utilities.Messages;
using System;

namespace Core.Exceptions.Aspect
{
    public class WrongLoggingTypeException : Exception
    {
        public WrongLoggingTypeException() : base(AspectMessages.WrongLoggingType)
        {
        }

        public WrongLoggingTypeException(string message) : base(message)
        {
        }

        public WrongLoggingTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public static void IfNotEqual(Type obja, Type objb)
        {
            if (obja != objb)
                throw new WrongLoggingTypeException();
        }
        public static void IfNotEqual(Type obja, object objb)
        {
            if (obja != objb.GetType())
                throw new WrongLoggingTypeException();
        }
        public static void IfNotEqual(object obja, Type objb)
        {
            if (obja.GetType() != objb)
                throw new WrongLoggingTypeException();
        }
        public static void IfNotEqual(object obja, object objb)
        {
            if (obja.GetType() != objb.GetType())
                throw new WrongLoggingTypeException();
        }
    }
}
