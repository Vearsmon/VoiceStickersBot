namespace VoiceStickersBot.Core;

public class SwitchKeyboardResult : ICommandResult
{
    public bool EnsureSuccess { get; set; }
    public CommandError GetError { get; set; }

    public InlineKeyboardDto InlineKeyboardDto { get; }
    
    public SwitchKeyboardResult(InlineKeyboardDto inlineKeyboardDto)
    {
        this.InlineKeyboardDto = inlineKeyboardDto;
    }
}