using System.Threading.Tasks;
using System.Xml.Xsl;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using VoterBot.Enums;
using VoterBot.Interface;
using VoterBot.Models;
using VoterBot.Repositories;
using VoterBot.ServiceCommand;
using VoterBot.StaticServices;

namespace VoterBot.Commands
{
    public class ModifyConactCommand : Command
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
            long number ;
            if (!_requestParams.User.ContactNumber.Contains("+"))
            {
                int length = _requestParams.User.ContactNumber.Length;
                _requestParams.User.ContactNumber.Substring(1);
            }
            bool isPhoneNumber = long.TryParse(_requestParams.User.ContactNumber,out number);

            if(!isPhoneNumber)
            {
                var command = CommandFactory.GetCommand(CommandFactory.CommandWords.CONTACT);
                command.SetRequestParams(_requestParams);
                await command.Execute(client,userId);
                return;
            }

            await userRepository.Update(_requestParams.User);

            var saySuccess = await botResponseRepository
                .FindByCodition(b => b.Type == ResponseTextType.Done);

            var saySuccessText = GetTextFromLanguage
                .GetText(_requestParams.User.Language, saySuccess);

            await client.SendTextMessageAsync(
                chatId: userId,
                text: saySuccessText,
                replyMarkup: new ReplyKeyboardRemove()
                );

            var commentResponse = await botResponseRepository
                .FindByCodition(b => b.Type == ResponseTextType.Comment);

            var commentResponseText = GetTextFromLanguage
                .GetText(_requestParams.User.Language, commentResponse);

            await client.SendTextMessageAsync(
                chatId: userId,
                text: commentResponseText
                );
        }
    }
}
