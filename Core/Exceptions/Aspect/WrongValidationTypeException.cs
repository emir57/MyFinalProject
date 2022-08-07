using Core.Utilities.Messages;
using System;

namespace Core.Exceptions.Aspect
{
    public class WrongValidationTypeException : Exception
    {
        public WrongValidationTypeException() : base(AspectMessages.WrongValidationType)
        {
        }

        public WrongValidationTypeException(string message) : base(message)
        {
        }

        public WrongValidationTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public static void IfNotEqual(Type obja, Type objb)
        {
            ifNotEqual(obja, objb);
        }
        public static void IfNotEqual(Type obja, object objb)
        {
            ifNotEqual(obja, objb.GetType());
        }
        public static void IfNotEqual(object obja, Type objb)
        {
            ifNotEqual(obja.GetType(), objb);
        }
        public static void IfNotEqual(object obja, object objb)
        {
            ifNotEqual(obja.GetType(), objb.GetType());
        }
        protected static void ifNotEqual(Type obja, Type objb)
        {
            if (obja != objb)
                throw new WrongValidationTypeException();
        }
    }
}
