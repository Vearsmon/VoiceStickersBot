using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandResults.DeletePackResults;

public class DeletePackConfirmResult : DeletePackCommandResultBase
{
    public override long ChatId { get; }
    public InlineKeyboardDto KeyboardDto { get; }
    
    public DeletePackConfirmResult(long chatId, InlineKeyboardDto keyboardDto)
    {
        ChatId = chatId;
        KeyboardDto = keyboardDto;
    }
}