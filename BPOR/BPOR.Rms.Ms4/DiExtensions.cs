using BPOR.Rms.Ms4.Repositories;
using BPOR.Rms.Ms4.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BPOR.Rms.Ms4;

public static class DiExtensions
{
    public static void AddStudyRequest(this IServiceCollection services)
    {
        services.AddScoped<IStudyDraftRepository, StudyDraftRepository>();
        services.AddValidatorsFromAssemblyContaining<StudyRequestStartViewModelValidator>();    
    }
}