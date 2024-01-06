namespace VoiceStickersBot.Core.CommandArguments;

public class UserInfo
{
    public UserState State { get; }

    public string StickerPackId { get; }

    public string StickerName { get; }

    public UserInfo(UserState state, string stickerPackId = "", string stickerName = "")
    {
        State = state;
        StickerPackId = stickerPackId;
        StickerName = stickerName;
    }
}
// AddSticker:
// WaitStickerName (Id)
//     |
//     V
// WaitFile (Id, Name)
//
// CreatePack:
// WaitPackName
