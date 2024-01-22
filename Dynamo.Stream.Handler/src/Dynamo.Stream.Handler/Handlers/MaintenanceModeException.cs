using System.Runtime.Serialization;

namespace Dynamo.Stream.Handler.Handlers
{
    [Serializable]
    internal class MaintenanceModeException : Exception
    {
        public MaintenanceModeException() : base("System is in maintenance mode.")
        {
        }

        public MaintenanceModeException(string? message) : base(message)
        {
        }

        public MaintenanceModeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MaintenanceModeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}