using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BPOR.Rms.Ms4.FlowGraph;

public static class Extensions
{
    public static string? GetUrl(this IUrlHelper helper, ITransitionResult<MvcActionKey> transitionResult)
        => helper.GetUrl(transitionResult.NodeKey, transitionResult.Context);
    
    public static string? GetUrl(this IUrlHelper helper, MvcActionKey mvcAction, object? parameters)
        => helper.Action(mvcAction.Action, mvcAction.Controller, parameters);

    public static void AddMvcFlow<T, TModel, TContext>(this IServiceCollection serviceCollection)
        where T : class, IMvcFlowGraph<TModel, TContext>
    {
        serviceCollection.AddSingleton<T>();
        serviceCollection.AddSingleton<IMvcFlowGraph<TModel, TContext>>(serviceProvider => serviceProvider.GetRequiredService<T>());
        serviceCollection.AddSingleton<IMvcFlowGraph<TContext>>(serviceProvider => serviceProvider.GetRequiredService<T>());
    }
}