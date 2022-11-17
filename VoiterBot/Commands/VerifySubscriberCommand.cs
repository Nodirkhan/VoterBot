using System.Threading.Tasks;
using Telegram.Bot;
using VoterBot.Entities;
using VoterBot.Enums;
using VoterBot.Interface;
using VoterBot.Repositories;
using VoterBot.StaticServices;

namespace VoterBot.Commands
{
    public class VerifySubscriberCommand : Command
    {

        public override async Task Execute(ITelegramBotClient client, long userId)
        {
            foreach (var channel in DefaultList.Channels)
            {
                var subscriber = await client
                    .GetChatMemberAsync("@" + channel.Link, userId);

                if(subscriber.Status.ToString() == "Left")
                {
                    var askUnsubscriber = await _requestParams.botResponseRepository
                        .FindByCodition(b => b.Type == ResponseTextType.UnSubscribe);
                    
                    _requestParams.User.IsSubscriber = false;

                    await _requestParams.userRepository.Update(_requestParams.User);

                    var unsubscriberText = GetTextFromLanguage
                        .GetText(_requestParams.User.Language, askUnsubscriber);

                    await client.SendTextMessageAsync(
                        chatId: userId,
                        text: unsubscriberText
                        );

                    return;
                }
            }
            _requestParams.User.IsSubscriber = true;
            await _requestParams.userRepository.Update(_requestParams.User);

            ///Call FIO
            var askFullName = await _requestParams.botResponseRepository
                .FindByCodition(b => b.Type == ResponseTextType.FullName);

            var fullNameText = GetTextFromLanguage
                .GetText(_requestParams.User.Language, askFullName);

            await client.DeleteMessageAsync(
                chatId: _requestParams.Chat,
                messageId: _requestParams.MessageId
                );

            await client.SendTextMessageAsync(
                        chatId: userId,
                        text: fullNameText
                        );

        }
    }
}
