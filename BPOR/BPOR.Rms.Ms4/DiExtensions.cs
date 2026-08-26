using BPOR.Rms.Ms4.Validators;
using BPOR.Rms.Ms4.Validators.Overview;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BPOR.Rms.Ms4;

public static class DiExtensions
{
    public static void AddStudyRequest(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<StudyRequestStartViewModelValidator>();    
    }
}