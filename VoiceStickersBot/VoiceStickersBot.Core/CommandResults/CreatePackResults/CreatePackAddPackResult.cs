namespace VoiceStickersBot.Core.CommandResults.CreatePackResults;

public class CreatePackAddPackResult : ICommandResult
{
    public long ChatId { get; }

    public CreatePackAddPackResult(long chatId)
    {
        ChatId = chatId;
    }
}