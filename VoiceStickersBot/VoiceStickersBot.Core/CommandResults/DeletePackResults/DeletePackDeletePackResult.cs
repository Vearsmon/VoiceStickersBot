namespace VoiceStickersBot.Core.CommandResults.DeletePackResults;

public class DeletePackDeletePackResult : DeletePackCommandResultBase
{
    public override long ChatId { get; }
    
    public DeletePackDeletePackResult(long chatId)
    {
        ChatId = chatId;
    }
}