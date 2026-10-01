using BPOR.Domain.Entities;
using BPOR.Rms.Abstractions.Enums;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Study;
using BPOR.Rms.Ms4;
using BPOR.Rms.Ms4.FlowGraph;
using BPOR.Rms.Ms4.Repositories;
using BPOR.Rms.Startup;
using BPOR.Rms.VolunteerInformation.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NIHR.Infrastructure.Paging;
using UserRole = BPOR.Domain.Enums.UserRole;

namespace BPOR.Rms.Controllers;

public class StudyController(
    ParticipantDbContext context,
    IPaginationService paginationService,
    ICurrentUserProvider<User> currentUserProvider,
    ILogger<StudyController> logger,
    IStudyDraftRepository studyDraftRepository
) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string? searchTerm, bool hasBeenReset = false,
        CancellationToken token = default)
    {
        if (hasBeenReset)
        {
            TempData["HasBeenReset"] = true;
            return RedirectToAction(nameof(Index));
        }

        bool userHasResearcherRole = currentUserProvider.User.HasRole(Domain.Enums.UserRole.Researcher);

        var studiesQuery = context.Studies.AsQueryable();

        if (userHasResearcherRole)
        {
            string userEmail = currentUserProvider?.User?.ContactEmail ?? string.Empty;
            studiesQuery = studiesQuery.Where(s => s.EmailAddress == userEmail);
        }

        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = searchTerm.Trim();
            var isParsedInt = int.TryParse(searchTerm, out var searchInt);
            studiesQuery = studiesQuery.Where(s => (isParsedInt && s.Id == searchInt)
                                                   || (isParsedInt && s.CpmsId == searchInt)
                                                   // TODO investigate full text search
                                                   || s.StudyName.Contains(searchTerm));
        }

        var deferredStudiesPage = studiesQuery
            .AsStudyListModel()
            .OrderByDescending(s => s.Id)
            .DeferredPage(paginationService);

        var viewModel = new StudiesViewModel
        {
            Studies = await deferredStudiesPage.ValueAsync(token),
            HasSearched = Request.Query.ContainsKey(nameof(searchTerm)),
            SearchTerm = searchTerm ?? string.Empty,
        };

        return View(viewModel);
    }


    // GET: Study/Details/5
    [HttpGet("Study/{id:int}", Order = 0)]
    [HttpGet("Study/Details/{id:int}", Order = 1)] // Legacy route
    public async Task<IActionResult> Details([FromServices] IVipRepository repository, int? id,
        CancellationToken cancellationToken)
    {
        if (id == null)
        {
            return NotFound();
        }

        var study = await context.Studies
            .Include(s => s.StudyStatus)
            .Where(s => s.Id == id)
            .AsStudyDetailsViewModel()
            .FirstOrDefaultAsync(cancellationToken);

        if (study == null)
        {
            logger.LogWarning("[HttpGet]Details called with non-existent study: {StudyId}", id);
            return NotFound();
        }
        
        var isAdmin = currentUserProvider.User.HasRole(UserRole.Admin);
        var isResearcher = currentUserProvider.User.HasRole(UserRole.Researcher);
        var isIdentifiable = study.Study.IsRecruitingIdentifiableParticipants;
        var updateRecruitmentAction = isIdentifiable ? "UpdateRecruited" : "UpdateAnonymousRecruited";
        var updateRecruitmentButtonText = isIdentifiable ? "Add enrolments" : "Update recruitment total";

        var canUpdateRecruitmentTotal = isIdentifiable
            ? study.HasCampaigns
            : isAdmin || (study.HasCampaigns && isResearcher);

        var vsiStatus = await repository.GetVipStatus(id.Value, cancellationToken);
        ViewData["VsiStatus"] = vsiStatus;
        
        study.ActionLinks = new();

        if (isAdmin)
        {
            switch (vsiStatus)
            {
                case null:
                    study.ActionLinks.Add(new ActionLink
                    {
                        Text = "Create volunteer study information page",
                        Url = Url.Action("Start", "VolunteerInformationStart", new { studyId = id.Value })
                    });
                    break;
                case VsiStatus.Draft:
                    study.ActionLinks.Add(new ActionLink
                    {
                        Text = "Resume volunteer study information page",
                        Url = Url.Action("Start", "VolunteerInformationStart", new { studyId = id.Value })
                    });
                    break;
                case VsiStatus.Active:
                    study.ActionLinks.Add(new ActionLink
                    {
                        Text = "Preview volunteer study information page",
                        Url = Url.Action("PreviewVip", "VolunteerInformationPage", new { studyId = id.Value }),
                        Target = HyperlinkTarget.Blank
                    });
                    break;
            }
        }

        if (canUpdateRecruitmentTotal)
        {
            study.ActionLinks.Add(new ActionLink
            {
                Text = updateRecruitmentButtonText,
                Url = Url.Action(updateRecruitmentAction, "Volunteer", new { studyId = id })
            });
        }

        if (isAdmin)
        {
            study.ActionLinks.AddRange(
            [
                new ActionLink
                {
                    Text = "Find volunteers",
                    Url = Url.Action("Index", "Filter", new { studyId = id })
                },
                new ActionLink
                {
                    Text = "Send an email",
                    Url = Url.Action("Index", "ResearcherEmail", new { studyId = id })
                }
            ]);
        }

        return View(study);
    }

    // GET: Study/Create
    public async Task<IActionResult> Create(CancellationToken cancellationToken = default)
    {
        var study = new Study();
        var studyId = await studyDraftRepository.CreateDraftStudyAsync(study, cancellationToken);
        
        var isAdmin = currentUserProvider.User.HasRole(UserRole.Admin);

        var uri = Url.GetUrl(StudyRequestEditFlow.EthicsApproval, new StudyRequestEditContext
        {
            StudyId = studyId,
            FlowType = isAdmin ? StudyRequestEditFlowType.AdminCreate : StudyRequestEditFlowType.ResearcherCreate
        });

        return Redirect(uri);
    }
    
    // success
    public IActionResult AddStudySuccess(AddStudySuccessViewModel viewModel)
    {
        return View(viewModel);
    }
    
    public async Task<IActionResult> SendIntroductoryEmail(int id)
    {
        var studyModel = await context.Studies
            .Where(s => s.Id == id)
            .AsStudyDetailsViewModel()
            .FirstOrDefaultAsync();

        if (studyModel == null)
        {
            logger.LogWarning("[HttpGet]Edit called with non-existent study: {StudyId}", id);
            return NotFound();
        }

        if (!studyModel.Study.IsEligibilityCriteriaComplete)
        {
            return BadRequest(ModelState);
        }

        return View(studyModel);
    }
}