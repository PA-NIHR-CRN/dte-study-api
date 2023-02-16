using System.Collections.Generic;

namespace Application.Models.Studies
{
    public class PreScreenerQuestionModel
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