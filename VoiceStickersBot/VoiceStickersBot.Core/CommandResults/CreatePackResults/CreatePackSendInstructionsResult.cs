namespace VoiceStickersBot.Core.CommandResults.CreatePackResults;

public class CreatePackSendInstructionsResult : CreatePackCommandResultBase
{
    public override long ChatId { get; }

    public CreatePackSendInstructionsResult(long chatId)
    {
        ChatId = chatId;
    }
}