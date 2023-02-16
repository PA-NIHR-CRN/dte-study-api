using System.Collections.Generic;

namespace StudyApi.Requests.Studies
{
    public class CreatePreScreenerQuestionnaireRequest
    {
        public long StudyId { get; set; }
        public int Version { get; set; }
        public IEnumerable<CreatePreScreenerQuestionRequest> Questions { get; set; }   
    }

    public class CreatePreScreenerQuestionRequest
    {
        public string QuestionText { get; set; }
        public string Explanation { get; set; }
        public string Reference { get; set; }
        public string AnswerOptionType { get; set; }
        public List<string> AnswerOptions { get; set; }
        public List<string> ValidAnswerOptions { get; set; }
        public int Sequence { get; set; }
    }
}