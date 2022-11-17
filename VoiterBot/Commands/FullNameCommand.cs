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
        private UserRepositoryAsync _userRepository;
        private BotResponseRepositoryAsync _botResponse;

        public override async Task Execute(ITelegramBotClient client, long userId)
        {
            await _userRepository.Update(_requestParams.User);

            ///Call SendContactCommand
            ///
            var command = CommandFactory.GetCommand(CommandFactory.CommandWords.CONTACT);
            command.SetRequestParams(_requestParams);
            await command.Execute(client, userId);

        }
    }
}
