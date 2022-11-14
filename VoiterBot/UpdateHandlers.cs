﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VoterBot.ServiceCommand;

namespace VoterBot
{
    public static class UpdateHandlers
    {
        public static Task PollingErrorHandler(
           ITelegramBotClient botClient,
           Exception exception,
           CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram Api Error:\n" +
                $"[{apiRequestException.ErrorCode}]" +
                $"{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(errorMessage);
            return Task.CompletedTask;
        }
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient,
            Update update,
            CancellationToken cancellationToken)
        {

            var handler = update.Type switch
            {
                UpdateType.Message => BotOnMessageReceived(botClient, update.Message!),
                UpdateType.CallbackQuery => BotOnCallBackQueryReceived(botClient, update.CallbackQuery!),
                _ => null
            };
            await handler;
        }
        private static async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message)
        {
            await new SendCommand(botClient)
               .GetText_SetCommand(message);
        }
        private static async Task BotOnCallBackQueryReceived(ITelegramBotClient botclient, CallbackQuery callbackQuery)
        {

        }

    }
}
