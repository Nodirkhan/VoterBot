using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using VoterBot.Interface;
using VoterBot.Models;
using VoterBot.ReplyMarkups;
using VoterBot.Repositories;
using VoterBot.StaticServices;

namespace VoterBot.Commands
{
    public class SendKeyBoardButton : Command
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
            _requestParams.User = await userRepository.GetById(userId);
            var KeyBoards = await botResponseRepository.GetKeyBoard();
           
            List<string> textKeyBoards = GetTextFromLanguage
                .GetKeyboardText(_requestParams.User.Language, KeyBoards);

            if (string.IsNullOrEmpty(_requestParams.text))
                return;

            await client.SendTextMessageAsync(
            chatId: userId,
               text: _requestParams.text,
               replyMarkup: ReplyMarkup.SendKeyBoardButton(textKeyBoards)
               );
        }
    }
}
