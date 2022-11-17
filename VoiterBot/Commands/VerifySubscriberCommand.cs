using System.Threading.Tasks;
using Telegram.Bot;
using VoterBot.Entities;
using VoterBot.Enums;
using VoterBot.Interface;
using VoterBot.Models;
using VoterBot.ReplyMarkups;
using VoterBot.Repositories;
using VoterBot.ServiceCommand;
using VoterBot.StaticServices;

namespace VoterBot.Commands
{
    public class VerifySubscriberCommand : Command
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
            foreach (var channel in DefaultList.Channels)
            {
                var subscriber = await client
                    .GetChatMemberAsync("@" + channel.Link, userId);

                if (subscriber.Status.ToString() == "Left")
                {
                    var askUnsubscriber = await botResponseRepository
                        .FindByCodition(b => b.Type == ResponseTextType.UnSubscribe);

                    _requestParams.User.IsSubscriber = false;

                    await userRepository.Update(_requestParams.User);

                    var unsubscriberText = GetTextFromLanguage
                        .GetText(_requestParams.User.Language, askUnsubscriber);

                    var checkerButton = await botResponseRepository
                          .FindByCodition(u => u.Type == ResponseTextType.Verify);

                    var buttontext = GetTextFromLanguage.GetText(_requestParams.User.Language, checkerButton);

                    await client.SendTextMessageAsync(
                        chatId: userId,
                        text: unsubscriberText,
                        replyMarkup: ReplyMarkup.OptionsOfChannel(buttontext)
                        );

                    return;
                }
            }

            _requestParams.User.IsSubscriber = true;
            await userRepository.Update(_requestParams.User);

            ///Call FIO


            if (_requestParams.text != CommandFactory.CommandWords.DELETE_MESSAGE)
            {
                var botData = await botResponseRepository
                .FindByCodition(b => b.Type == ResponseTextType.Done);

                _requestParams.text = GetTextFromLanguage.GetText(_requestParams.User.Language, botData);

                await client.EditMessageTextAsync(
                    chatId: userId,
                    messageId: _requestParams.MessageId,
                    text: _requestParams.text
                    );
            }
            if (string.IsNullOrEmpty(_requestParams.User.FullName))
            {
                var askFullName = await botResponseRepository
                .FindByCodition(b => b.Type == ResponseTextType.FullName);

                var fullNameText = GetTextFromLanguage
                    .GetText(_requestParams.User.Language, askFullName);

                await client.SendTextMessageAsync(
                           chatId: userId,
                           text: fullNameText
                           );
            }
            else if (string.IsNullOrEmpty(_requestParams.User.ContactNumber))
            {
                var askContactCommand = CommandFactory
                    .GetCommand(CommandFactory.CommandWords.CONTACT);

                askContactCommand.SetRequestParams(_requestParams);
                await askContactCommand.Execute(client, userId);
            }
        }
    }
}
