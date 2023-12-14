namespace Dynamo.Stream.Ingestor.Services
{
    public class StreamHandlerLambdaSettings
    {
        public string FunctionName { get; set; } =
                    "arn:aws:lambda:eu-west-2:841171564302:function:crnccd-lambda-dev-dte-participant-stream";
    }
}