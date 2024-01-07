using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.DeleteStickerCommandArguments;

public interface IDeleteStickerCommandArguments : ICommandArguments
{
    public DeleteStickerStepName StepName { get; }
}