using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.StartResults;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class StartResultHandler : ICommandResultHandler
{
    public CommandType CommandType => CommandType.Start;
    
    private readonly Dictionary<Type, Func<ITelegramBotClient, Dictionary<long, UserInfo>, ICommandResult, Task>> handlers;

    public StartResultHandler()
    {
        handlers = new Dictionary<Type, Func<ITelegramBotClient, Dictionary<long, UserInfo>, ICommandResult, Task>>()
        {
            {
                typeof(StartStartResult),
                async (bot, infos, res) => await Handle(bot, infos, (StartStartResult)res)
            }
        };
    }
    
    public Task HandleResult(
        ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        ICommandResult result)
    {
        return handlers[result.GetType()](bot, userInfos, result);
    }

    private async Task Handle(ITelegramBotClient bot,
        Dictionary<long, UserInfo> userInfos,
        StartStartResult result)
    {
        userInfos[result.ChatId] = new UserInfo(UserState.NoWait);

        if (!Enum.TryParse(result.ChatType, out ChatType chatType))
            throw new ArgumentException($"Wrong ChatType: {result.ChatType}");

        var keyboard = chatType == ChatType.Private ? Keyboards.DialogKeyboard : Keyboards.GroupKeyboard;
        
        await bot.SendTextMessageAsync(
            result.ChatId,
            "Для взаимодействия с ботом используйте кнопки ниже.\n" +
            "Для начала создайте или импортируйте стикерпак.\n" +
            "Создание стикерпака происходит в лс с ботом.\n" +
            "В группах отправляйте сообщения ответом боту.",
            replyMarkup: keyboard);
    }
}