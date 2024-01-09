namespace VoiceStickersBot.Core.CommandResults.DeletePackResults;

public class DeletePackDeletePackResult : DeletePackCommandResultBase
{
    public override long ChatId { get; }
    public string ChatType { get; }
    
    public DeletePackDeletePackResult(long chatId, string chatType)
    {
        ChatId = chatId;
        ChatType = chatType;
    }
}