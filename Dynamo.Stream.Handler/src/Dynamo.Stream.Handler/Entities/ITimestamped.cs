namespace Dynamo.Stream.Handler.Entities
{
    public interface ITimestamped
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
