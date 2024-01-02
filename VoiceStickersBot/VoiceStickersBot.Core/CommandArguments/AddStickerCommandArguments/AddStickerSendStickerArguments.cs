using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;

public class AddStickerSendStickerArguments : IAddStickerCommandArguments
{
    public CommandType CommandType => CommandType.AddSticker;
    public AddStickerStepName StepName => AddStickerStepName.SendSticker;
    
    public Guid StickerPackId { get; }
    public Guid StickerId { get; }
    public long ChatId { get; }
    
    public AddStickerSendStickerArguments(Guid stickerPackId, Guid stickerId, long chatId)
    {
        StickerPackId = stickerPackId;
        StickerId = stickerId;
        ChatId = chatId;
    }
}