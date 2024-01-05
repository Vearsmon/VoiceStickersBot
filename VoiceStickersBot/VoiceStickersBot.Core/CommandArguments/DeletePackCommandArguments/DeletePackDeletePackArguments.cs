using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.DeletePackCommandArguments;

public class DeletePackDeletePackArguments : IDeletePackCommandArguments
{
    public CommandType CommandType => CommandType.DeletePack;
    public DeletePackStepName StepName => DeletePackStepName.DeletePack;
    
    public Guid StickerPackId { get; }
    public long ChatId { get; }
    
    public DeletePackDeletePackArguments(Guid stickerPackId, long chatId)
    {
        StickerPackId = stickerPackId;
        ChatId = chatId;
    }
}