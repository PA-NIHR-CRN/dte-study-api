using System.Collections.Generic;

namespace StudyApi.Requests.Users
{
    public class SaveAccessWhiteListRequest
    {
        public List<string> Emails { get; set; }
    }
}