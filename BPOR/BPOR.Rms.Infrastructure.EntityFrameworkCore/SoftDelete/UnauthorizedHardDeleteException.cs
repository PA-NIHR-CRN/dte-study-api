namespace BPOR.Rms.Infrastructure.EntityFrameworkCore
{
    public class UnauthorizedHardDeleteException : UnauthorizedAccessException
    {
        public UnauthorizedHardDeleteException(string? message) : base(message)
        {
        }
    }
}
