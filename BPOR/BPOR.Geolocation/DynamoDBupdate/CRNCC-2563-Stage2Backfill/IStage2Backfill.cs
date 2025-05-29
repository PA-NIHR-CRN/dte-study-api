using System;
namespace DynamoDBupdate.CRNCC2563Stage2Backfill
{
    public interface IStage2Backfill
	{
		Task RunAsync();
	}
}

