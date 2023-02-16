using System.Collections.Generic;

namespace StudyApi.Requests.Studies
{
    public class UpdateStudyRequest
    {
        public string WhatImportant { get; set; }
        public IEnumerable<string> HealthConditions { get; set; }
    }

}