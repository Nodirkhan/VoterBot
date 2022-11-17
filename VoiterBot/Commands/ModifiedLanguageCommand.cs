using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Telegram.Bot;
using VoterBot.Enums;
using VoterBot.Interface;
using VoterBot.Models;
using VoterBot.Repositories;
using VoterBot.ServiceCommand;
using VoterBot.StaticServices;
using static VoterBot.ServiceCommand.CommandFactory;

namespace VoterBot.Commands
{
    public class ModifiedLanguageCommand : Command
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
            _requestParams.User.Language = _requestParams.CallbackData switch
            {
                "Uz" => Language.Uz,
                "Ru" => Language.Ru,
                "Eng" => Language.Eng,
                _ => Language.Uz
            };

            await userRepository.Update(_requestParams.User);

            var botData = await botResponseRepository
                .FindByCodition(b => b.Type == ResponseTextType.Done);

            _requestParams.text = GetTextFromLanguage.GetText(_requestParams.User.Language, botData);

                await client.EditMessageTextAsync(
                    chatId:userId,
                    messageId: _requestParams.MessageId,
                    text: _requestParams.text
                    );

            if (!_requestParams.User.IsSubscriber)
            {
                var command = CommandFactory.GetCommand(CommandWords.SHOW_CHANNELS);

                command.SetRequestParams(_requestParams);
                await command.Execute(client, userId);
            }
            else
            {
                _requestParams.text = string.Empty;
                var command = CommandFactory
                    .GetCommand(CommandFactory.CommandWords.KEYBOARDBUTTON);

                command.SetRequestParams(_requestParams);
                await command.Execute(client, userId);
            }



        }
    }
}
