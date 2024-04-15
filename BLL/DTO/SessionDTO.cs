using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class SessionDTO
    {
        public int SessionId { get; set; }

        public string UserId { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }

        public SessionDTO() { }
        public SessionDTO(int sessionId, string userId, DateTime sessionStart, DateTime sessionEnd)
        {
            SessionId = sessionId;
            UserId = userId;
            SessionStart = sessionStart;
            SessionEnd = sessionEnd;
        }
    }
}
