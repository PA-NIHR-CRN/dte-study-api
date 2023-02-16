using System.Collections.Generic;

namespace Application.Responses.V1.Studies
{
    public class StudyInfoResponse
    {
        public string Title { get; set; }
        public string ShortName { get; set; }
        public string WhatImportant { get; set; }

        public IEnumerable<string> HealthConditions { get; set; }
        public string StudyQuestionnaireLink { get; set; }
    }
}