using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandResults.DeletePackResults;

public class DeletePackConfirmResult : DeletePackCommandResultBase
{
    public override long ChatId { get; }
    public InlineKeyboardDto KeyboardDto { get; }
    public int? BotMessageId { get; }
    
    public DeletePackConfirmResult(long chatId, InlineKeyboardDto keyboardDto, int? botMessageId)
    {
        ChatId = chatId;
        KeyboardDto = keyboardDto;
        BotMessageId = botMessageId;
    }
}