using System;
using System.Collections.Generic;

namespace Application.Responses.V1.Studies
{
    public class StudyResponse
    {
        public long StudyId { get; set; }
        public long CpmsId { get; set; }
        public string IsrctnId { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public string WhatImportant { get; set; }
        public IEnumerable<string> HealthConditions { get; set; }
        public string StudyQuestionnaireLink { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public string CreatedById { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public string UpdatedById { get; set; }
    }
}