namespace DYNAMO.STREAM.HANDLER.Entities
{
    public interface ITimestamped
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
