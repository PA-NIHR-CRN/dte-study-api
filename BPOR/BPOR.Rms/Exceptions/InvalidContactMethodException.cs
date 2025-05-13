namespace BPOR.Rms.Exceptions
{
    public class InvalidContactMethodException : Exception
    {
        public InvalidContactMethodException(string contactMethodType)
            : base($"Invalid contact method type: {contactMethodType}")
        {
        }
    }
}
