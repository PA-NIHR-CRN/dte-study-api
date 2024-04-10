using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;
using Microsoft.AspNetCore.Mvc;

namespace BPOR.Rms.Controllers;

public class FilterController(ParticipantDbContext context) : Controller
{
    public IActionResult Index(VolunteerFilterViewModel model)
    {
        model.SexRegisteredAtBirth = context.Genders.Where(x => x.Id != 3 && !x.IsDeleted)
                                            .Select(x => new SelectListItem
                                            {
                                                Value = x.Id.ToString(),
                                                Text = x.Description,
                                                Selected = false
                                            })
                                            .ToList();

        model.VolunteerCount = "-";

        return View(model);
    }

    public int FilterVolunteers(VolunteerFilterViewModel model)
    {
        int volunteerCount = 0;

        return volunteerCount;
    }
}
