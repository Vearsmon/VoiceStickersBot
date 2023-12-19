using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.CommandResults;

public interface ISwitchKeyboardResult : ICommandResult
{
    public string BotMessageId { get; }
    public InlineKeyboardDto KeyboardDto { get; }

}