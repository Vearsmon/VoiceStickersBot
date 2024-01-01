namespace VoiceStickersBot.Core;

//TODO: удалить в пиздищу
[Obsolete("to remove")]
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