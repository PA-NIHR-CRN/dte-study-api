using System;

namespace Application.Responses.V1.Researchers
{
    public class ResearcherResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}