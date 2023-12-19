namespace VoiceStickersBot.Core;

public enum UserBotState
{
    WaitCommand,
    WaitChoosePackToDeleteSticker,
    WaitChoosePackToDeletePack,
    WaitChoosePackToShow,
    WaitChoosePackToAddSticker,
    WaitChooseSticker,
    WaitConfirm,
    WaitSendName,
    WaitSendSticker
}