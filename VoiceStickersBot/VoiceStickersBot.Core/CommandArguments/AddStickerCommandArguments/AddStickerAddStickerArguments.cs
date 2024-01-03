using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;

public class AddStickerAddStickerArguments : IAddStickerCommandArguments
{
    public CommandType CommandType => CommandType.AddSticker;
    public AddStickerStepName StepName => AddStickerStepName.AddSticker;
    
    public Guid StickerPackId { get; }
    public string StickerName { get; }
    public string FileId { get; }
    public long ChatId { get; }
    
    public AddStickerAddStickerArguments(Guid stickerPackId, string stickerName, string fileId, long chatId)
    {
        StickerPackId = stickerPackId;
        StickerName = stickerName;
        FileId = fileId;
        ChatId = chatId;
    }
}