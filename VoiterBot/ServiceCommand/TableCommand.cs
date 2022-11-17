using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VoterBot.Data;
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
            var context = new ApplicationDbContext();
            _requestParams.userRepository = new UserRepositoryAsync(context);
            _requestParams.botResponseRepository = new BotResponseRepositoryAsync(context);
        }
        public async Task<Command> GetCommandForMessage(Message message)
        {

            var command = message.Type switch
            {
                MessageType.Text =>
                    await ReturnCommandFromText(message),
                MessageType.Contact =>
                    await ReturnCommandFromContact(message),
                _ => null
            };
            return command;

            
        }

        private async Task<Command> ReturnCommandFromText(Message message)
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

            _requestParams.User = await _requestParams.userRepository.GetById(userId);

            var Button = await GeneretorNextCommandByText(text, userId);
            var command1 = CommandFactory.GetCommand(Button.Uz ?? "");
            if (command1 == null)
            {
                if (string.IsNullOrEmpty(_requestParams.User.FullName))
                {
                    _requestParams.User.FullName = text;
                    command1 = CommandFactory.GetCommand(CommandFactory.CommandWords.FULLNAME);
                }
            }
            command1?.SetRequestParams(_requestParams);
            return command1;
        }

        private async Task<Command> ReturnCommandFromContact(Message message)
        {
            _requestParams.User = await _requestParams.userRepository.GetById(message.Chat.Id);

            _requestParams.User.ContactNumber = message.Contact.PhoneNumber;
            var command = CommandFactory
                .GetCommand(CommandFactory.CommandWords.MODIFY_CONTACT);

            command.SetRequestParams(_requestParams);
            return command;
        }


        public async Task<Command> GetCommandForCallbackQuery(CallbackQuery callbackQuery)
        {
            _requestParams.User = await _requestParams.userRepository.GetById(callbackQuery.From.Id);
            _requestParams.Chat = callbackQuery.Message.Chat;
            _requestParams.MessageId = callbackQuery.Message.MessageId;
            _requestParams.CallbackData = callbackQuery.Data;

            if (_requestParams.CallbackData is null)
                return null;

                if(_requestParams.CallbackData == "VERIFY")
                {
                    var command = CommandFactory
                        .GetCommand(CommandFactory.CommandWords.VERIFY);
                    command.SetRequestParams(_requestParams);
                    return command;
                }
                else
                {
                    var command = CommandFactory.GetCommand(_requestParams.CallbackData);
                    command?.SetRequestParams(_requestParams);
                    return command;
                }
        }


        public async Task<BotResponseText> GeneretorNextCommandByText(string text, long userId)
        {
            return _requestParams.User?.Language switch
            {
                Language.Uz =>
                    await _requestParams.botResponseRepository.FindByCodition(b => b.Uz == text),
                Language.Eng =>
                    await _requestParams.botResponseRepository.FindByCodition(b => b.Eng == text),
                Language.Ru =>
                    await _requestParams.botResponseRepository.FindByCodition(b => b.Ru == text),
                _ =>
                    await _requestParams.botResponseRepository.FindByCodition(b => b.Uz == text)
            };
        }



    }
}
