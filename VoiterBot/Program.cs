using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using VoterBot;
using VoterBot.Models;

try
{
    var bot = new TelegramBotClient(DefaultValues.TOKEN);

    var me = await bot.GetMeAsync();
    Console.Title = me.Username ?? "My awesome Bot";

    using var cts = new CancellationTokenSource();

    var receiverOptions = new ReceiverOptions()
    {
        AllowedUpdates = Array.Empty<UpdateType>()
    };

    bot.StartReceiving(updateHandler: UpdateHandlers.HandleUpdateAsync,
                       pollingErrorHandler: UpdateHandlers.PollingErrorHandler,
                       receiverOptions: receiverOptions,
                       cancellationToken: cts.Token);

    Console.WriteLine($"Start listening for @{me.Username}");
    Console.ReadLine();

    cts.Cancel();


}
catch (Exception ext)
{

}
