using System.Threading.Tasks;
using Telegram.Bot;
using VoterBot.Models;

namespace VoterBot.Interface
{
    public abstract class Command
    {
        public RequestParams _requestParams { get; set; }
        public abstract Task Execute(ITelegramBotClient client, long userId);
        public virtual void SetRequestParams(RequestParams requestParams)
        {
            _requestParams = requestParams;
        }
    }
}
