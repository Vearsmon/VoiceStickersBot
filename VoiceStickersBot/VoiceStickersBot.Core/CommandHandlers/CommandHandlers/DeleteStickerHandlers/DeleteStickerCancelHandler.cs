using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.DeleteStickerCommandArguments;
using VoiceStickersBot.Core.CommandResults;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.DeleteStickerHandlers;

public class DeleteStickerCancelHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.DeleteSticker;
    
    private readonly DeleteStickerCancelArguments commandArguments;

    public DeleteStickerCancelHandler(DeleteStickerCancelArguments commandArguments)
    {
        this.commandArguments = commandArguments;
    }
    
    public Task<ICommandResult> Handle()
    {
        throw new NotImplementedException();
    }
}