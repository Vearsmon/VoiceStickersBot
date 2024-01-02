using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;
using VoiceStickersBot.Core.CommandResults;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.AddStickerHandlers;

public class AddStickerCancelHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.AddSticker;

    private readonly AddStickerCancelArguments commandArguments;

    public AddStickerCancelHandler(AddStickerCancelArguments commandArguments)
    {
        this.commandArguments = commandArguments;
    }

    public Task<ICommandResult> Handle()
    {
        throw new NotImplementedException();
    }
}