namespace VoiceStickersBot.Core.CommandArguments.CreatePackCommandArguments;

public class CreatePackCancelArguments : ICreatePackCommandArguments
{
    public CommandType CommandType => CommandType.CreatePack;
    public CreatePackStepName StepName => CreatePackStepName.Cancel;
}