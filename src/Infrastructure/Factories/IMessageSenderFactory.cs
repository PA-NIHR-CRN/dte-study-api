using Application.Contracts;

namespace Infrastructure.Factories
{
    public interface IMessageSenderFactory
    {
        IMessageSender Build(string source);
    }
}