using BPOR.Rms.Ms4.FlowGraph;
using BPOR.Rms.Ms4.Models;
using BPOR.Rms.Ms4.Repositories;
using BPOR.Rms.Ms4.Settings;
using BPOR.Rms.Ms4.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BPOR.Rms.Ms4;

public static class DiExtensions
{
    public static void AddStudyRequest(this IServiceCollection services)
    {
        services.AddOptions<StudyCreationSettings>().BindConfiguration("StudyCreation");
        
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddScoped(typeof(IMvcFlowHelper<>), typeof(MvcFlowHelper<>));
        services.AddScoped(typeof(IMvcFlowHelper<,>), typeof(MvcFlowHelper<,>));
        services.AddMvcFlow<StudyRequestEditFlow, StudyRequestViewModel, StudyRequestEditContext>();
        services.AddScoped<IStudyDraftRepository, StudyDraftRepository>();
        services.AddValidatorsFromAssemblyContaining<StudyRequestStartViewModelValidator>();    
    }
}