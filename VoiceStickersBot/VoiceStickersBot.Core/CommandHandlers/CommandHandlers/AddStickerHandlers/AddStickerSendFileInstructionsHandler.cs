using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.AddStickerResults;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.AddStickerHandlers;

public class AddStickerSendFileInstructionsHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.AddSticker;

    private readonly AddStickerSendFileInstructionsArguments commandArguments;

    public AddStickerSendFileInstructionsHandler(AddStickerSendFileInstructionsArguments commandArguments)
    {
        this.commandArguments = commandArguments;
    }

    public async Task<ICommandResult> Handle()
    {
        return new AddStickerSendFileInstructionsResult(
            commandArguments.ChatId,
            commandArguments.StickerPackId,
            commandArguments.StickerName);
    }
}