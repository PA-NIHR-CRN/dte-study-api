using System.Collections.Immutable;
using System.Linq.Expressions;

namespace BPOR.Rms.Models.VolunteerStudyInformation.Metadata;

public class RadioProperty<TModel, TProperty> : Property<TModel, TProperty>
{
    public ImmutableArray<MultiSelectOption<TProperty>> RadioOptions { get; }

    public RadioProperty(PropertyOptions<TModel, TProperty> options, IEnumerable<MultiSelectOption<TProperty>> radioRadioOptions) 
        : base(options)
    {
        RadioOptions = radioRadioOptions.ToImmutableArray();
    }
}