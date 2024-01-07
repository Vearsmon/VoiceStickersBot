using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;

public class AddStickerSendNameInstructionsArguments : IAddStickerCommandArguments
{
    public CommandType CommandType => CommandType.AddSticker;
    public AddStickerStepName StepName => AddStickerStepName.SendNameInstr;
    
    public Guid StickerPackId { get; }
    public long ChatId { get; }

    public AddStickerSendNameInstructionsArguments(Guid stickerPackId, long chatId)
    {
        StickerPackId = stickerPackId;
        ChatId = chatId;
    }
}