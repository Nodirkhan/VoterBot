using System.Threading.Tasks;
using Telegram.Bot;
using VoterBot.Enums;
using VoterBot.Interface;
using VoterBot.StaticServices;

namespace VoterBot.Commands
{
    public class ModifyConactCommand : Command
    {
        public override async Task Execute(ITelegramBotClient client, long userId)
        {
            await _requestParams.userRepository.Update(_requestParams.User);

            var saySuccess = await _requestParams.botResponseRepository
                .FindByCodition(b => b.Type == ResponseTextType.Done);

            var saySuccessText = GetTextFromLanguage
                .GetText(_requestParams.User.Language, saySuccess);

            await client.SendTextMessageAsync(
                chatId: userId,
                text: saySuccessText
                );
        }
    }
}
