using System.Threading.Tasks;
using Telegram.Bot.Types;
using VoterBot.Entities;
using VoterBot.Enums;
using VoterBot.Interface;
using VoterBot.Models;
using VoterBot.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace VoterBot.ServiceCommand
{
    public class TableCommand
    {
        private CommandFactory _commandFactory;
        private RequestParams _requestParams;
        public TableCommand()
        {
            _commandFactory = new CommandFactory();
            _requestParams = new RequestParams();
            _requestParams.Context = new Data.ApplicationDbContext();
        }
        public async Task<Command> GetCommandForMessage(Message message)
        {
            _requestParams.Chat = message.Chat;
            var userId = message.Chat.Id;
            var text = message.Text;
            if (text == CommandFactory.CommandWords.START)
            {
                var command = CommandFactory.GetCommand(text);
                command.SetRequestParams(_requestParams);
                return command;
            }
            var Button = await GeneretorNextCommandByText(text, userId);
            var command1 = CommandFactory.GetCommand(Button.Uz);
            command1?.SetRequestParams(_requestParams);
            return command1;
        }

        public async Task<BotResponseText> GeneretorNextCommandByText(string text, long userId)
        {
            var userRepository = new UserRepositoryAsync(_requestParams.Context);

            var botResponseRepository = new BotResponseRepositoryAsync(_requestParams.Context);

            var user = await userRepository.GetById(userId);

            return user?.Language switch
            {
                Language.Uz =>
                    await botResponseRepository.FindByCodition(b => b.Uz == text),
                Language.Eng =>
                    await botResponseRepository.FindByCodition(b => b.Uz == text),
                Language.Ru =>
                    await botResponseRepository.FindByCodition(b => b.Uz == text),
                _ =>
                    await botResponseRepository.FindByCodition(b => b.Uz == text)
            };
        }



    }
}
