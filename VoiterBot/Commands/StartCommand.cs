using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using VoterBot.Entities;
using VoterBot.Enums;
using VoterBot.Interface;
using VoterBot.Models;
using VoterBot.Repositories;
using VoterBot.ServiceCommand;
using VoterBot.StaticServices;

namespace VoterBot.Commands
{
    public class StartCommand : Command
    {
        private UserRepositoryAsync userRepository;
        private BotResponseRepositoryAsync botResponseRepository;
        private User User { get; set; }

        public override void SetRequestParams(RequestParams requestParams)
        {
            User = new User();
            _requestParams = requestParams;
            userRepository = new UserRepositoryAsync();
            botResponseRepository = new BotResponseRepositoryAsync();
        }
        public override async Task Execute(ITelegramBotClient client, long userId)
        {
            _requestParams.User = await userRepository.GetById(userId);

            var helloButton = await botResponseRepository
                .FindByCodition(b => b.Type == ResponseTextType.Hello);

            if(_requestParams.User == null)
            {
                User.UserId = userId;
                User.Username = _requestParams.Chat.Username;
                User.Language = Language.Uz;
                await userRepository.CreateUserAsync(User);

                var helloButtonText = GetTextFromLanguage.GetText(User.Language, helloButton);
                
                await client.SendTextMessageAsync(
                    chatId: userId,
                    text:helloButtonText,
                    replyMarkup: new ReplyKeyboardRemove()
                    );

                var command = CommandFactory
                    .GetCommand(CommandFactory.CommandWords.SHOW_LANGUAGES);

                _requestParams.User = User;
                command.SetRequestParams(_requestParams);
                await command.Execute(client, userId);
            }
            else
            {
                _requestParams.text = GetTextFromLanguage.GetText(_requestParams.User.Language, helloButton);

                var command = CommandFactory
                    .GetCommand(CommandFactory.CommandWords.SHOW_LANGUAGES);

                command.SetRequestParams(_requestParams);
                await command.Execute(client, userId);
            }
        }


    }
}
