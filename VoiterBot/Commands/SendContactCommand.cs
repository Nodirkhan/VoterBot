using System.Threading.Tasks;
using Telegram.Bot;
using VoterBot.Enums;
using VoterBot.Interface;
using VoterBot.Models;
using VoterBot.ReplyMarkups;
using VoterBot.Repositories;
using VoterBot.StaticServices;

namespace VoterBot.Commands
{
    public class SendContactCommand : Command
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
            var askContact = await botResponseRepository
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
