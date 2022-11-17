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
        
        public override async Task Execute(ITelegramBotClient client, long userId)
        {
            _requestParams.User.Language = _requestParams.CallbackData switch
            {
                "Uz" => Language.Uz,
                "Ru" => Language.Ru,
                "Eng" => Language.Eng,
                _ => Language.Uz
            };

            await _requestParams.userRepository.Update(_requestParams.User);

            await client.DeleteMessageAsync(
                _requestParams.Chat,
                _requestParams.MessageId
                );

            var botData = await _requestParams.botResponseRepository
                .FindByCodition(b => b.Type == ResponseTextType.Done);

            _requestParams.text = GetTextFromLanguage.GetText(_requestParams.User.Language, botData);

            #region Call Other Command
            /// Call to keyboard button

            #endregion

            if (!_requestParams.User.IsSubscriber)
            {
                var command = CommandFactory.GetCommand(CommandWords.SHOW_CHANNELS);

                command.SetRequestParams(_requestParams);
                await command.Execute(client, userId);
            }

        }
    }
}
