namespace VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;

public class AddStickerSendFileInstructionsArguments : IAddStickerCommandArguments
{
    public CommandType CommandType => CommandType.AddSticker;
    public AddStickerStepName StepName => AddStickerStepName.SendFileInstr;
    
    public Guid StickerPackId { get; }
    public string StickerName { get; }
    public long ChatId { get; }
    
    public AddStickerSendFileInstructionsArguments(Guid stickerPackId, string stickerName, long chatId)
    {
        StickerPackId = stickerPackId;
        StickerName = stickerName;
        ChatId = chatId;
    }
}