namespace VoiceStickersBot.Core.CommandArguments.CreatePackCommandArguments;

public class CreatePackSendInstructionsArguments : ICreatePackCommandArguments
{
    public CommandType CommandType => CommandType.CreatePack;
    public CreatePackStepName StepName => CreatePackStepName.SendInstructions;
    public long ChatId { get; }
    
    public CreatePackSendInstructionsArguments(long chatId)
    {
        ChatId = chatId;
    }
}