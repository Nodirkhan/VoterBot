using System.Threading.Tasks;
using Telegram.Bot;
using VoterBot.Interface;

namespace VoterBot.ServiceCommand
{
    public class ManageCommand
    {
        private Command _command;
        public void SetCommand(Command command) => _command = command;

        public async Task Invoke(ITelegramBotClient client, long userId)
        {
            await _command?.Execute(client, userId);
        }
    }
}
