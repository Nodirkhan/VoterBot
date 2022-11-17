using System.Threading.Tasks;
using Telegram.Bot;
using VoterBot.Interface;
using VoterBot.ServiceCommand;

namespace VoterBot.Commands
{
    public class PersonalNumberCommand : Command
    {
        
        public override async Task Execute(ITelegramBotClient client, long userId)
        {
            _requestParams.text = _requestParams.User.FullName + ": " + _requestParams.User.VoterNumber;

            var command = CommandFactory.GetCommand(CommandFactory.CommandWords.KEYBOARDBUTTON);
            command.SetRequestParams(_requestParams);
            await command.Execute(client, userId);
        }
    }
}
