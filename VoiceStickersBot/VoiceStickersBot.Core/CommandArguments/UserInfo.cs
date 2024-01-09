namespace VoiceStickersBot.Core.CommandArguments;

public class UserInfo
{
    public UserState State { get; }
    public string StickerPackId { get; }
    public string StickerName { get; }

    public List<DateTime> RequestTimes { get; } = new();

    public UserInfo(UserState state, string stickerPackId = "", string stickerName = "")
    {
        State = state;
        StickerPackId = stickerPackId;
        StickerName = stickerName;
    }
}