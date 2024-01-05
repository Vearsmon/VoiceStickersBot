using Telegram.Bot;
using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.DeletePackResults;

namespace VoiceStickersBot.TgGateway.CommandResultHandlers;

public class DeletePackResultHandler : ICommandResultHandler
{
    public CommandType CommandType => CommandType.DeletePack;
    
    private readonly Dictionary<Type, Func<ITelegramBotClient, ICommandResult, Task>> handlers;

    public DeletePackResultHandler()
    {
        handlers = new Dictionary<Type, Func<ITelegramBotClient, ICommandResult, Task>>()
        {
            {
                typeof(DeletePackSwitchKeyboardPacksResult),
                async (bot, res) => await Handle(bot, (DeletePackDeletePackResult)res)
            },
            {
                typeof(DeletePackDeletePackResult),
                async (bot, res) => await Handle(bot, (DeletePackSwitchKeyboardPacksResult)res)
            },
            {
                typeof(DeletePackConfirmResult),
                async (bot, res) => await Handle(bot, (DeletePackConfirmResult)res)
            }
        };
    }

    public Task HandleResult(ITelegramBotClient bot, ICommandResult result)
    {
        return handlers[result.GetType()](bot, result);
    }
    
    private async Task Handle(ITelegramBotClient bot, DeletePackDeletePackResult result)
    {
        await bot.SendTextMessageAsync(
            result.ChatId,
            "Стикерпак успешно удален.");
    }
    
    private async Task Handle(ITelegramBotClient bot, DeletePackSwitchKeyboardPacksResult result)
    {
        var markup = SwitchKeyboardResultExtensions.GetMarkupFromDto(result.KeyboardDto);

        if (result.BotMessageId is null)
        {
            await bot.SendTextMessageAsync(
                result.ChatId,
                "Выберите набор, который хотите удалить:",
                replyMarkup: markup);
        }
        else
        {
            await bot.EditMessageReplyMarkupAsync(
                inlineMessageId: result.BotMessageId,
                replyMarkup: markup);
        }
    }
    
    private async Task Handle(ITelegramBotClient bot, DeletePackConfirmResult result)
    {
        var markup = SwitchKeyboardResultExtensions.GetMarkupFromDto(result.KeyboardDto);

        await bot.SendTextMessageAsync(
            result.ChatId,
            "Вы точно хотите удалить этот набор?",
            replyMarkup: markup);
    }
}