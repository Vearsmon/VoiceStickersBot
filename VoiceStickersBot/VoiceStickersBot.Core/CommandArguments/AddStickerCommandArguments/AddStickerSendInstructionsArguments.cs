using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;

public class AddStickerSendInstructionsArguments : IAddStickerCommandArguments
{
    public CommandType CommandType => CommandType.AddSticker;
    public AddStickerStepName StepName => AddStickerStepName.SendInstructions;
    
    public Guid StickerPackId { get; }
    public long ChatId { get; }

    public AddStickerSendInstructionsArguments(Guid stickerPackId, long chatId)
    {
        StickerPackId = stickerPackId;
        ChatId = chatId;
    }
}