using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory; // как будто лишнее

namespace VoiceStickersBot.Core.CommandArguments.DeletePackCommandArguments;

public interface IDeletePackCommandArguments : ICommandArguments
{
    public DeletePackStepName StepName { get; }
}