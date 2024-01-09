namespace VoiceStickersBot.Core.CommandArguments.CreatePackCommandArguments;

public class CreatePackAddPackArguments : ICreatePackCommandArguments
{
    public CommandType CommandType => CommandType.CreatePack;
    public CreatePackStepName StepName => CreatePackStepName.AddPack;
    
    public string PackName { get; }
    public long ChatId { get; }
    
    public CreatePackAddPackArguments(string packName, long chatId)
    {
        PackName = packName;
        ChatId = chatId;
    }
}