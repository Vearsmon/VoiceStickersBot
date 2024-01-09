namespace VoiceStickersBot.Core.CommandArguments.DeletePackCommandArguments;

public class DeletePackDeletePackArguments : IDeletePackCommandArguments
{
    public CommandType CommandType => CommandType.DeletePack;
    public DeletePackStepName StepName => DeletePackStepName.DeletePack;
    
    public Guid StickerPackId { get; }
    public string ChatType { get; }
    public long ChatId { get; }
    
    public DeletePackDeletePackArguments(Guid stickerPackId, string chatType, long chatId)
    {
        StickerPackId = stickerPackId;
        ChatType = chatType;
        ChatId = chatId;
    }
}