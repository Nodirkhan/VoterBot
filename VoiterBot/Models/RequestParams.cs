using Telegram.Bot.Types;
using VoterBot.Repositories;

namespace VoterBot.Models
{
    public class RequestParams
    {
        public Entities.User User { get; set; }
        public string CallbackData { get; set; }
        public int MessageId { get; set; }
        public Chat Chat { get; set; }
        public string text { get; set; }
        public UserRepositoryAsync userRepository { get; set; }
        public BotResponseRepositoryAsync botResponseRepository { get; set; }
    }
}
