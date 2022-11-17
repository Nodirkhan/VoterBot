using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using VoterBot.Interface;

namespace VoterBot.ServiceCommand
{
    public class SendCommand
    {
        private Command _command;
        private readonly ManageCommand _manager;
        private readonly TableCommand _tableCommand;
        private readonly ITelegramBotClient _client;

        public SendCommand(ITelegramBotClient client)
        {
            _client = client;
            _manager = new ManageCommand();
            _tableCommand = new TableCommand();
        }
        public async Task GetText_SetCommand(Message message)
        {
            _command = await _tableCommand.GetCommandForMessage(message);
            if (_command == null)
                return;

            _manager.SetCommand(_command);
            await _manager.Invoke(_client, message.Chat.Id);
        }

        public async Task GetCallbackQuery_SetCommand(CallbackQuery query)
        {
            _tableCommand.client = _client;
            _command = await _tableCommand.GetCommandForCallbackQuery(query);
            if (_command == null) 
                return;

            _manager.SetCommand(_command);
            await _manager.Invoke(_client, query.From.Id);
        }
    }
}
