using System.Linq;
using Application.Models.Studies;
using Application.Responses.V1.Studies;
using Application.Studies.V1.Commands.Studies;
using Dte.Study.Management.Api.Client.Request.Studies;

namespace Application.Mappings.Studies
{
    public class StudyMapper
    {
        public static StudyInfoResponse MapTo(Dte.Study.Management.Api.Client.Responses.StudyInfo.StudyInfoResponse source)
        {
            return new StudyInfoResponse
            {
                Title = source.Title,
                ShortName = source.ShortName,
                WhatImportant = source.WhatImportant,
                HealthConditions = source.HealthConditions,
                StudyQuestionnaireLink = source.StudyQuestionnaireLink
            };
        }

        public static StudyResponse MapTo(Dte.Study.Management.Api.Client.Responses.Studies.StudyResponse source)
        {
            return new StudyResponse
            {
                Status = source.Status,
                Title = source.Title,
                CpmsId = source.CpmsId,
                IsrctnId = source.IsrctnId,
                ShortName = source.ShortName,
                StudyId = source.StudyId,
                WhatImportant= source.WhatImportant,
                HealthConditions = source.HealthConditions,
                CreatedAtUtc = source.CreatedAtUtc,
                CreatedById = source.CreatedById,
                StudyQuestionnaireLink = source.StudyQuestionnaireLink,
                UpdatedAtUtc = source.UpdatedAtUtc,
                UpdatedById = source.UpdatedById
            };
        }

        public static StudyRoleResponse MapTo(Dte.Study.Management.Api.Client.Responses.Studies.StudyRoleResponse source)
        {
            return new StudyRoleResponse
            {
                Role = source.Role,
                Item = MapTo(source.Item)
            };
        }

        public static CreatePreScreenerQuestionnaireRequest MapTo(CreatePreScreenerQuestionnaireCommand source)
        {
            return new CreatePreScreenerQuestionnaireRequest
            {
                StudyId = source.StudyId,
                UserId = source.ResearcherId,
                Version = source.Version,
                Questions = source.PreScreenerQuestions.Select(MapTo)
            };
        }
        
        public static CreatePreScreenerQuestionRequest MapTo(PreScreenerQuestionModel source)
        {
            return new CreatePreScreenerQuestionRequest
            {
                Explanation = source.Explanation,
                Reference = source.Reference,
                Sequence = source.Sequence,
                AnswerOptions = source.AnswerOptions,
                QuestionText = source.QuestionText,
                AnswerOptionType = source.AnswerOptionType,
                ValidAnswerOptions = source.ValidAnswerOptions
            };
        }
    }
}