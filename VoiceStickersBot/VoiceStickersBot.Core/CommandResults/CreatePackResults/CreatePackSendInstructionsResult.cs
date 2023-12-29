namespace VoiceStickersBot.Core.CommandResults.CreatePackResults;

public class CreatePackSendInstructionsResult : ICommandResult
{
    public long ChatId { get; }
    public CreatePackSendInstructionsResult(long chatId)
    {
        ChatId = chatId;
    }
}