using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandResults;

public interface ISwitchKeyboardResult : ICommandResult
{
    public string BotMessageId { get; }
    public InlineKeyboardDto KeyboardDto { get; }

}