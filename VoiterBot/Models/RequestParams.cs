using Telegram.Bot.Types;
using VoterBot.Data;
using VoterBot.Enums;

namespace VoterBot.Models
{
    public class RequestParams
    {
        public string CallbackData { get; set; }
        public ApplicationDbContext Context { get; set; }
        public int MessageId { get; set; }
        public Chat Chat { get; set; }
        public Language language { get; set; } = Language.Uz;
        public string text { get; set; }
    }
}
