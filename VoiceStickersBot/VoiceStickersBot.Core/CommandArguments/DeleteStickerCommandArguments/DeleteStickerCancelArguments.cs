using VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

namespace VoiceStickersBot.Core.CommandArguments.DeleteStickerCommandArguments;

public class DeleteStickerCancelArguments : IDeleteStickerCommandArguments
{
    public CommandType CommandType => CommandType.DeleteSticker;
    public DeleteStickerStepName StepName => DeleteStickerStepName.Cancel;
    
}