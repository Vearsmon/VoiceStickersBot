namespace VoiceStickersBot.Core.Commands.SwitchKeyboard;

public class SwitchKeyboardResult : ICommandResult
{
    public SwitchKeyboardResult(InlineKeyboardDto inlineKeyboardDto)
    {
        this.InlineKeyboardDto = inlineKeyboardDto;
    }

    public InlineKeyboardDto InlineKeyboardDto { get; }
    public bool EnsureSuccess { get; set; }
    public CommandError GetError { get; set; }
}