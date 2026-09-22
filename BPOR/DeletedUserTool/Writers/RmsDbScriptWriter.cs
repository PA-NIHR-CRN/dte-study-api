namespace DeletedUserTool.Writers;

public class RmsDbScriptWriter : ScriptWriter
{
    public RmsDbScriptWriter(string outputFolderPath)
    : base(File.CreateText(Path.Combine(outputFolderPath, "rms-db-clean.sql")))
    {
    }

    protected override void WriteComment(string comment)
    {
        TextWriter.WriteLine("-- " + comment);
    }

    public void WriteDeleteParticpantIdentifer(int participantIdentifierId)
    {
        TextWriter.WriteLine($"DELETE FROM dte.ParticipantIdentifiers WHERE Id = {participantIdentifierId}");
    }

    public void WriteDeleteParticipant(int participantId)
    {
        TextWriter.WriteLine($"DELETE FROM dte.Participants WHERE Id = {participantId}");
    }

    public void WriteAnonymiseParticipant(int participantId)
    {
        TextWriter.WriteLine($"-- Anonymise Participant: {participantId}");
        TextWriter.WriteLine($"UPDATE dte.Participants SET Email = '', FirstName = '', LastName = '', " +
                             $"GenderId = null, MobileNumber = null, " +
                             $"LandlineNumber = null, DailyLifeImpactId = null, EthnicBackground = null, " +
                             $"NHSNumber = null, IsDeleted = 1 WHERE Id = {participantId}");
        TextWriter.WriteLine($"UPDATE dte.ParticipantAddress SET AddressLine1 = '', AddressLine2 = '', " + 
                             $"AddressLine3 = '', AddressLine4 = '', Postcode = TRIM(SUBSTRING(Postcode, 1, 4)) WHERE Id = {participantId}");
        TextWriter.WriteLine($"DELETE FROM dte.ParticipantHealthCondition WHERE ParticipantId = {participantId}");
        TextWriter.WriteLine($"DELETE FROM dte.ParticipantLocation WHERE ParticipantId = {participantId}");
    }
}