using System.Threading.Tasks;
using Telegram.Bot;
using VoterBot.Entities;
using VoterBot.Interface;
using VoterBot.Models;
using VoterBot.Repositories;

namespace VoterBot.Commands
{
    
    public class ShowLanguageCommand : Command
    {
        private UserRepositoryAsync _userRepository;
        private BotResponseRepositoryAsync _botResponseRepository;

        public override void SetRequestParams(RequestParams requestParams)
        {
            _requestParams = requestParams;
            _botResponseRepository = new BotResponseRepositoryAsync(_requestParams.Context);
            _userRepository = new UserRepositoryAsync(_requestParams.Context);
        }
        public override async Task Execute(ITelegramBotClient client, long userId)
        {
            var user = await _userRepository.GetById(userId);

        }
    }
}
