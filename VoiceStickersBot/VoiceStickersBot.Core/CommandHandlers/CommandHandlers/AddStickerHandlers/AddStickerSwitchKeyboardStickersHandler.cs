using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;
using VoiceStickersBot.Core.CommandResults;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.AddStickerHandlers;

public class AddStickerSwitchKeyboardStickersHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.AddSticker;

    private readonly AddStickerSwitchKeyboardStickersArguments commandArguments;

    public AddStickerSwitchKeyboardStickersHandler(AddStickerSwitchKeyboardStickersArguments commandArguments)
    {
        this.commandArguments = commandArguments;
    }

    public Task<ICommandResult> Handle()
    {
        throw new NotImplementedException();
    }
}