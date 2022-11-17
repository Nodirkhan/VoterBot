﻿using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using VoterBot.Entities;
using VoterBot.Enums;
using VoterBot.Interface;
using VoterBot.Models;
using VoterBot.ReplyMarkups;
using VoterBot.Repositories;
using VoterBot.StaticServices;

namespace VoterBot.Commands
{
    
    public class ShowLanguageCommand : Command
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

            var botResponse = await botResponseRepository
                .FindByCodition(b => b.Type == ResponseTextType.Language);

            var text = GetTextFromLanguage.GetText(_requestParams.User.Language, botResponse);

            await client.SendTextMessageAsync(
                chatId: userId,
                text: text,
                replyMarkup: ReplyMarkup.OptionsOfLanguage()
                );

        }
    }
}
