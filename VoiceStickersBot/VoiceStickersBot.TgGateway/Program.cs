using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using VoiceStickersBot.TgGateway;

var botClient = new TelegramBotClient("5989359414:AAF5EHNI513b6kNi1In6gjqUBi9HKgsGRrM");

using CancellationTokenSource cts = new();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>()
};

var handler = new TgApiGateway();
botClient.StartReceiving(
    handler.HandleUpdateAsync,
    handler.HandlePollingErrorAsync,
    receiverOptions,
    cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

cts.Cancel();