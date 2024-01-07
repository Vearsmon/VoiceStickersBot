using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.AddStickerResults;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.AddStickerHandlers;

public class AddStickerSendNameInstructionsHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.AddSticker;

    private readonly AddStickerSendNameInstructionsArguments commandArguments;

    public AddStickerSendNameInstructionsHandler(AddStickerSendNameInstructionsArguments commandArguments)
    {
        this.commandArguments = commandArguments;
    }

    public async Task<ICommandResult> Handle()
    {
        return new AddStickerSendNameInstructionsResult(commandArguments.ChatId, commandArguments.StickerPackId);
    }
}