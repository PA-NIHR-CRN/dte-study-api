
namespace Dynamo.Stream.Ingestor.Harness
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var ingestor = new Functions();

            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();
            await ingestor.IngestParticipants();

        }
    }
}
