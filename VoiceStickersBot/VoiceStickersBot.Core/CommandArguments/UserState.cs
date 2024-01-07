namespace VoiceStickersBot.Core.CommandArguments;

public enum UserState
{
    WaitFile,
    WaitPackName,
    WaitStickerName,
    WaitStickerChoice,
    WaitOptionChoice,
    NoWait
}