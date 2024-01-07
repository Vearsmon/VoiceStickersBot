namespace VoiceStickersBot.Core.CommandArguments.DeletePackCommandArguments;

public class DeletePackConfirmArguments : IDeletePackCommandArguments
{
    public CommandType CommandType => CommandType.DeletePack;
    public DeletePackStepName StepName => DeletePackStepName.Confirm;

    public Guid StickerPackId { get; }
    public long ChatId { get; }
    public int? BotMessageId { get; }
    
    public DeletePackConfirmArguments(Guid stickerPackId, long chatId, int? botMessageId)
    {
        StickerPackId = stickerPackId;
        ChatId = chatId;
        BotMessageId = botMessageId;
    }
}