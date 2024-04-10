using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Expressions;

namespace BPOR.Rms.Controllers;

public class FilterController(ParticipantDbContext context) : Controller
{
    List<Expression<Func<Participant, bool>>> filters = new List<Expression<Func<Participant, bool>>>();

    public IActionResult Index(VolunteerFilterViewModel model)
    {
        return View(model);
    }

    [HttpPost]
    public IActionResult FilterVolunteers(VolunteerFilterViewModel model)
    {
        int volunteerCount = 0;

        FilterBySexRegisteredAtBirth(model.IsSexMale, model.IsSexFemale, model.IsGenderSameAsSexRegisteredAtBirth_Yes, model.IsGenderSameAsSexRegisteredAtBirth_No, model.IsGenderSameAsSexRegisteredAtBirth_PreferNotToSay);
        FilterByEthnicity(model.Ethnicity_Asian, model.Ethnicity_Black, model.Ethnicity_Mixed, model.Ethnicity_Other, model.Ethnicity_White);

        IQueryable<Participant> query = context.Participants.AsQueryable();

        foreach (var filter in filters)
        {
            query = query.Where(filter);
        }

        volunteerCount = query.Count();

        model.VolunteerCount = volunteerCount == 0 ? "-" : volunteerCount.ToString();
        
        return View("Index", model);
    }

    public void FilterBySexRegisteredAtBirth(bool IsSexMale, bool IsSexFemale, bool IsGenderSameAsSexRegisteredAtBirth_Yes, bool IsGenderSameAsSexRegisteredAtBirth_No, bool IsGenderSameAsSexRegisteredAtBirth_PreferNotToSay)
    {
        if (IsSexMale || IsSexFemale)
        {
            if (!IsSexMale)
            {
                filters.Add(p => p.GenderId == 2);
            }
            else if (!IsSexFemale)
            {
                filters.Add(p => p.GenderId == 1);
            }
            else
            {
                filters.Add(p => p.GenderId == 1 || p.GenderId == 2);
            }
        }

        if (IsGenderSameAsSexRegisteredAtBirth_Yes || IsGenderSameAsSexRegisteredAtBirth_No || IsGenderSameAsSexRegisteredAtBirth_PreferNotToSay)
        {
            filters.Add(p =>
                       (IsGenderSameAsSexRegisteredAtBirth_Yes && p.GenderIsSameAsSexRegisteredAtBirth == true) ||
                       (IsGenderSameAsSexRegisteredAtBirth_No && p.GenderIsSameAsSexRegisteredAtBirth == false) ||
                       (IsGenderSameAsSexRegisteredAtBirth_PreferNotToSay && p.GenderIsSameAsSexRegisteredAtBirth == null));
        }     
    }

    public void FilterByEthnicity(bool Ethnicity_Asian, bool Ethnicity_Black, bool Ethnicity_Mixed, bool Ethnicity_Other, bool Ethnicity_White)
    {
        if (Ethnicity_Asian || Ethnicity_Black || Ethnicity_Mixed || Ethnicity_Other || Ethnicity_White)
        {
            filters.Add(p =>
                       (Ethnicity_Asian && p.EthnicGroup == "asian") ||
                       (Ethnicity_Black && p.EthnicGroup == "black") ||
                       (Ethnicity_Mixed && p.EthnicGroup == "mixed") ||
                       (Ethnicity_Other && p.EthnicGroup == "other") ||
                       (Ethnicity_White && p.EthnicGroup == "white"));
        }     
    }
}
