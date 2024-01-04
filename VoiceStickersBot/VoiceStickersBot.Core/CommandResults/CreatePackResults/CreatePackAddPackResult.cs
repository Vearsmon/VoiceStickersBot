namespace VoiceStickersBot.Core.CommandResults.CreatePackResults;

public class CreatePackAddPackResult : CreatePackCommandResultBase
{
    public override long ChatId { get; }

    public CreatePackAddPackResult(long chatId)
    {
        ChatId = chatId;
    }
}