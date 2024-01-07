namespace VoiceStickersBot.Core.CommandArguments.CreatePackCommandArguments;

public class CreatePackAddPackArguments : ICreatePackCommandArguments
{
    public CommandType CommandType => CommandType.CreatePack;
    public CreatePackStepName StepName => CreatePackStepName.AddPack;
    public long ChatId { get; }
    public string PackName { get; }
    
    public CreatePackAddPackArguments(long chatId, string packName)
    {
        ChatId = chatId;
        PackName = packName;
    }
}