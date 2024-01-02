using VoiceStickersBot.Core.CommandHandlers.CommandHandlers.ShowAllHandlers;

namespace VoiceStickersBot.Core.CommandResults;

public interface ISwitchKeyboardResult : ICommandResult
{
    public string BotMessageId { get; }
    public InlineKeyboardDto KeyboardDto { get; }

}