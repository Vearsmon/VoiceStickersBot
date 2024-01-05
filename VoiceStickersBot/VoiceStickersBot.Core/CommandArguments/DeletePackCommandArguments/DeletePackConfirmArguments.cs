using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandArguments.DeletePackCommandArguments;

public class DeletePackConfirmArguments : IDeletePackCommandArguments
{
    public CommandType CommandType => CommandType.DeletePack;
    public DeletePackStepName StepName => DeletePackStepName.Confirm;

    public Guid StickerPackId { get; }
    
    public long ChatId { get; }
    
    public DeletePackConfirmArguments(Guid stickerPackId, long chatId)
    {
        StickerPackId = stickerPackId;
        ChatId = chatId;
    }
}