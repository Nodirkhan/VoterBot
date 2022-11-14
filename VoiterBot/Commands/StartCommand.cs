using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using VoterBot.Entities;
using VoterBot.Interface;
using VoterBot.Models;
using VoterBot.Repositories;

namespace VoterBot.Commands
{
    public class StartCommand : Command
    {
        private User User { get; set; }

        private UserRepositoryAsync _userRepository;
        private BotResponseRepositoryAsync _botResponseRepository;

        public override void SetRequestParams(RequestParams requestParams)
        {
            User = new User();
            _requestParams = requestParams;
            _botResponseRepository = new BotResponseRepositoryAsync(_requestParams.Context);
            _userRepository = new UserRepositoryAsync(_requestParams.Context);

        }
        public override async Task Execute(ITelegramBotClient client, long userId)
        {
            var user = await _userRepository.GetById(userId);
            if(user == null)
            {
                User.UserId = userId;
                User.Username = _requestParams.Chat.Username;

                await _userRepository.CreateUserAsync(User);

                await client.SendTextMessageAsync(
                chatId: userId,
                text: "royxatdan otildi"
                );
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
