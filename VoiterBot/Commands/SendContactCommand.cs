using System.Threading.Tasks;
using Telegram.Bot;
using VoterBot.Enums;
using VoterBot.Interface;
using VoterBot.ReplyMarkups;
using VoterBot.StaticServices;

namespace VoterBot.Commands
{
    public class SendContactCommand : Command
    {
        public override async Task Execute(ITelegramBotClient client, long userId)
        {
            var askContact = await _requestParams.botResponseRepository
                .FindByCodition(b => b.Type == ResponseTextType.Contact);

            var askContactText = GetTextFromLanguage
                .GetText(_requestParams.User.Language, askContact);

            await client.SendTextMessageAsync(
                chatId: userId,
                text: askContactText,
                replyMarkup: ReplyMarkup.SendContact()
                );
        }

    }
}
