using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using VoterBot.Entities;
using VoterBot.Enums;
using VoterBot.Interface;
using VoterBot.Models;
using VoterBot.Repositories;
using VoterBot.ServiceCommand;

namespace VoterBot.Commands
{
    public class StartCommand : Command
    {
        private User User { get; set; }

        public override void SetRequestParams(RequestParams requestParams)
        {
            User = new User();
            _requestParams = requestParams;
        }
        public override async Task Execute(ITelegramBotClient client, long userId)
        {
            _requestParams.User = await _requestParams.userRepository.GetById(userId);
            if(_requestParams.User == null)
            {
                User.UserId = userId;
                User.Username = _requestParams.Chat.Username;
                User.Language = Language.Uz;
                await _requestParams.userRepository.CreateUserAsync(User);

                var command = CommandFactory
                    .GetCommand(CommandFactory.CommandWords.SHOW_LANGUAGES);

                command.SetRequestParams(_requestParams);
                await command.Execute(client, userId);
            }
            else
            {
                await client.SendTextMessageAsync(
                chatId: userId,
                text: "Allaqachon borsiz"
                );
            }
        }


    }
}
