using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;
using VoiceStickersBot.Core.CommandResults;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.AddStickerHandlers;

public class AddStickerSwitchKeyboardPacksHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.AddSticker;

    private readonly AddStickerSwitchKeyboardPacksArguments commandArguments;

    public AddStickerSwitchKeyboardPacksHandler(AddStickerSwitchKeyboardPacksArguments commandArguments)
    {
        this.commandArguments = commandArguments;
    }

    public Task<ICommandResult> Handle()
    {
        throw new NotImplementedException();
    }
}