using System.Threading.Tasks;
using Telegram.Bot;
using VoterBot.Data;
using VoterBot.Entities;
using VoterBot.Enums;
using VoterBot.Interface;
using VoterBot.Models;
using VoterBot.Repositories;
using VoterBot.ServiceCommand;
using VoterBot.StaticServices;

namespace VoterBot.Commands
{
    public class CommentCommand : Command
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
            var app = new ApplicationDbContext();
            _requestParams.User.Comments.Add( _requestParams.text);            
            await userRepository.Update(_requestParams.User);

            var AcceptedCommentResponse = await botResponseRepository
                .FindByCodition(b =>b.Type == ResponseTextType.AcceptedComment);

            _requestParams.text = GetTextFromLanguage
                .GetText(_requestParams.User.Language, AcceptedCommentResponse) +
                _requestParams.User.VoterNumber.ToString();


            var command = CommandFactory
                .GetCommand(CommandFactory.CommandWords.KEYBOARDBUTTON);

            command.SetRequestParams(_requestParams);
            await command.Execute(client,userId);
        }
    }
}
