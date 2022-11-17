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
    public class ShowChannelsCommand : Command
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
            var botResponse = await botResponseRepository
                .FindByCodition(u => u.Type == ResponseTextType.Subscribe);

            var textforSend = GetTextFromLanguage.GetText(_requestParams.User.Language, botResponse);

            var checkerButton = await botResponseRepository
                .FindByCodition(u => u.Type == ResponseTextType.Verify);

            var buttontext = GetTextFromLanguage.GetText(_requestParams.User.Language, checkerButton);

            await client.SendTextMessageAsync(
                chatId: userId,
                text: textforSend,
                replyMarkup: ReplyMarkup.OptionsOfChannel(buttontext)
                );
        }
    }
}
