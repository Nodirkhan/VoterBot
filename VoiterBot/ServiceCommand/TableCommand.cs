using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VoterBot.Entities;
using VoterBot.Enums;
using VoterBot.Interface;
using VoterBot.Models;
using VoterBot.Repositories;

namespace VoterBot.ServiceCommand
{
    public class TableCommand
    {
        private static IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        public ITelegramBotClient client;
        private UserRepositoryAsync userRepository;
        private BotResponseRepositoryAsync botResponseRepository;
        private CommandFactory _commandFactory;
        private RequestParams _requestParams;

        public TableCommand()
        {
            //_cache = new MemoryCache(new MemoryCacheOptions());
            _commandFactory = new CommandFactory();
            _requestParams = new RequestParams();
            userRepository = new UserRepositoryAsync();
            botResponseRepository = new BotResponseRepositoryAsync();
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

            bool newMessage = WriteToIMemoryCache(userId, text);
            if (!newMessage)
                return null;


            _requestParams.User = await userRepository.GetById(userId);
            _requestParams.text = text;

            if (_requestParams.text == CommandFactory.CommandWords.START || _requestParams.User == null)
            {
                var command = CommandFactory.GetCommand(CommandFactory.CommandWords.START);
                command.SetRequestParams(_requestParams);
                return command;
            }


            if (!_requestParams.User.IsSubscriber)
            {
                _requestParams.text = CommandFactory.CommandWords.DELETE_MESSAGE;
                var verifyCommand = CommandFactory
                    .GetCommand(CommandFactory.CommandWords.VERIFY);

                verifyCommand.SetRequestParams(_requestParams);
                return verifyCommand;
            }

            var Button = await GeneretorNextCommandByText(text, userId);

            if (Button == null)
            {
                if (string.IsNullOrEmpty(_requestParams.User.FullName))
                {
                    _requestParams.User.FullName = text;
                    var FullNameCommand = CommandFactory.GetCommand(CommandFactory.CommandWords.FULLNAME);
                    FullNameCommand.SetRequestParams(_requestParams);
                    return FullNameCommand;
                }
                else if (string.IsNullOrEmpty(_requestParams.User.ContactNumber))
                {
                    _requestParams.User.ContactNumber = _requestParams.text;
                    var command = CommandFactory
                        .GetCommand(CommandFactory.CommandWords.MODIFY_CONTACT);

                    command.SetRequestParams(_requestParams);
                    return command;
                }
                else
                {
                    var commentCommand = CommandFactory
                       .GetCommand(CommandFactory.CommandWords.ACCEPTED_COMMENT);
                    commentCommand.SetRequestParams(_requestParams);
                    return commentCommand;
                }
            }
            var command1 = CommandFactory.GetCommand(Button.Uz);

            command1?.SetRequestParams(_requestParams);
            return command1;
        }

        private async Task<Command> ReturnCommandFromContact(Message message)
        {
            _requestParams.User = await userRepository.GetById(message.Chat.Id);

            bool newMessage = WriteToIMemoryCache(message.Chat.Id, message.Contact.PhoneNumber);
            if (!newMessage)
                return null;

            _requestParams.User.ContactNumber = message.Contact.PhoneNumber;
            var command = CommandFactory
                .GetCommand(CommandFactory.CommandWords.MODIFY_CONTACT);

            command.SetRequestParams(_requestParams);
            return command;
        }

        public async Task<Command> GetCommandForCallbackQuery(CallbackQuery callbackQuery)
        {
            _requestParams.User = await userRepository.GetById(callbackQuery.From.Id);
            _requestParams.Chat = callbackQuery.Message.Chat;
            _requestParams.MessageId = callbackQuery.Message.MessageId;
            _requestParams.CallbackData = callbackQuery.Data;

            bool newMessage = WriteToIMemoryCache(callbackQuery.From.Id, callbackQuery.Message.MessageId.ToString());
            if (!newMessage)
                return null;

            if (_requestParams.User == null)
            {
                await client.DeleteMessageAsync(
                    _requestParams.Chat,
                    _requestParams.MessageId
                );

                var startCommand = CommandFactory
                    .GetCommand(CommandFactory.CommandWords.START);

                startCommand.SetRequestParams(_requestParams);
                return startCommand;
            }

            if (_requestParams.CallbackData is null)
                return null;

            if (_requestParams.CallbackData == "VERIFY")
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
                    await botResponseRepository.FindByCodition(b => b.Uz == text),
                Language.Eng =>
                    await botResponseRepository.FindByCodition(b => b.Eng == text),
                Language.Ru =>
                    await botResponseRepository.FindByCodition(b => b.Ru == text),
                _ =>
                    await botResponseRepository.FindByCodition(b => b.Uz == text)
            };
        }

        private bool WriteToIMemoryCache(long key, string data)
        {
            string result;

            if (_cache.TryGetValue(key, out result))
            {
                if (data == result)
                {
                    return false;
                }
            }
            _cache.Set(key, data, new MemoryCacheEntryOptions
            {
                Size = 1,
                SlidingExpiration = TimeSpan.FromSeconds(30),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
            });

            return true;
        }

    }
}
