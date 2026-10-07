using BPOR.Rms.PipelineTrackerIngest.TypeConvertors;
using CsvHelper.Configuration;

namespace BPOR.Rms.PipelineTrackerIngest;

public sealed class PipelineTrackerCsvMap : ClassMap<PipelineTrackerRow>
{
    public PipelineTrackerCsvMap()
    {
        Map(m => m.VsId).Name("VS ID");
        Map(m => m.StudyShortName).Name("Study Short Name");
        Map(m => m.CpmsId).Name("CPMS ID").TypeConverter<CpmsIdConverter>();
        Map(m => m.StudyStatus).Name("BPoR Study Status").TypeConverter<StudyStatusTypeConvertor>();
        Map(m => m.MainContactName).Name("Main Contact Name");
        Map(m => m.MainContactEmail).Name("Main Contact Email");
        Map(m => m.ManagingSpeciality).Name("Managing Specialty");
        Map(m => m.StudyDesign).Name("Study Design");
        Map(m => m.CommOrNonComm).Name("Comm / \r\nNon-Comm");
        Map(m => m.RecruitmentStartDate).Name("Recruitment Start Date");
        Map(m => m.RecruitmentEndDate).Name("Recruitment End Date");
        Map(m => m.OverallRecruitmentTarget).Name("Overall recruitment target");
        Map(m => m.AddedToRegistryPipelineAt).Name("Date/time study added to BPOR registry pipeline");
        Map(m => m.NihrFundingStatus).Name("Is the study either NIHR-funded, or NIHR Portfolio-adopted?")
            .TypeConverter<NihrFundingStatusTypeConvertor>();
        Map(m => m.RejectedWithdrawnReasonType).Name("Reason for Withdrawal/Rejection")
            .TypeConverter<RejectedWithdrawnReasonTypeConvertor>();
    }
}