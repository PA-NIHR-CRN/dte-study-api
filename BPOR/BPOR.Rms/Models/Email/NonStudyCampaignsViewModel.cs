using System.Linq.Expressions;
using BPOR.Domain.Entities;
using BPOR.Rms.Models.Study;
using Microsoft.AspNetCore.Mvc;
using Campaign = BPOR.Rms.Models.Study.Campaign;

namespace BPOR.Rms.Models.Email;

public class NonStudyCampaignsViewModel() : Controller
{
    public IEnumerable<Campaign>? Campaigns { get; set; }
}
