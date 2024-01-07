using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.DeleteStickerCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.DeleteStickerResults;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.DeleteStickerHandlers;

public class DeleteStickerConfirmHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.DeleteSticker;

    private readonly DeleteStickerConfirmArguments commandArguments;

    public DeleteStickerConfirmHandler(DeleteStickerConfirmArguments commandArguments)
    {
        this.commandArguments = commandArguments;
    }
    
    public async Task<ICommandResult> Handle()
    {
        var line = new List<InlineKeyboardButtonDto>() 
        { 
            new ("Назад", "DP:SwKbdPc:0:Increase:10"),
            new ("Удалить", $"DP:DeletePack:{commandArguments.StickerPackId}")
        };
        var keyboardDto = new InlineKeyboardDto(new List<List<InlineKeyboardButtonDto>>(), line);
        
        return new DeleteStickerConfirmResult(
            commandArguments.ChatId,
            keyboardDto,
            commandArguments.BotMessageId);
    }
}