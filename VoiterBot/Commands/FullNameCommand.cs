using System.Threading.Tasks;
using Telegram.Bot;
using VoterBot.Interface;
using VoterBot.Models;
using VoterBot.Repositories;
using VoterBot.ServiceCommand;

namespace VoterBot.Commands
{
    public class FullNameCommand : Command
    {
        private UserRepositoryAsync userRepository;
        private BotResponseRepositoryAsync botResponseRepository;

        public override void SetRequestParams(RequestParams requestParams)
        {
            _requestParams = requestParams;
            userRepository = new UserRepositoryAsync();
            botResponseRepository = new BotResponseRepositoryAsync();
        }
        public override async Task Execute(ITelegramBotClient client, long userId)
        {
            await userRepository.Update(_requestParams.User);

            ///Call SendContactCommand
            ///
            var command = CommandFactory.GetCommand(CommandFactory.CommandWords.CONTACT);
            command.SetRequestParams(_requestParams);
            await command.Execute(client, userId);

        }
    }
}
