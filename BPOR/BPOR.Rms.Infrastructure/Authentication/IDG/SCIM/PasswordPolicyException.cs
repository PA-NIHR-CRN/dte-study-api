using System;
using System.Runtime.Serialization;

namespace BPOR.Rms.Infrastructure.Authentication.IDG.SCIM
{
    [Serializable]
    public class PasswordPolicyException : Exception
    {
        public PasswordPolicyException()
        {
        }

        public PasswordPolicyException(string message) : base(message)
        {
        }

        public PasswordPolicyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PasswordPolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}