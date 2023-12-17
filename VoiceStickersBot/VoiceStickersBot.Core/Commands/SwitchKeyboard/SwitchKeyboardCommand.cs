﻿namespace VoiceStickersBot.Core.Commands.SwitchKeyboard;

public class SwitchKeyboardCommand : ICommand
{
    public PageChangeType PageChangeType { get; }
    public int? PageFrom { get; }
    public int StickersOnPage { get; }
    public string KeyboardCapture { get; }
    public Type CommandType => typeof(SwitchKeyboardCommand);
    public UserBotState UserBotState { get; }


    public SwitchKeyboardCommand(UserBotState userBotState, int? pageFrom, string commandText, int stickersOnPage, string keyboardCapture="")
    {
        var pageDirection = commandText.Split(':').First();
        
        if (pageDirection == "pageleft")
            PageChangeType = PageChangeType.Decrease;
        else if (pageDirection == "pageright")
            PageChangeType = PageChangeType.Increase;

        PageFrom = pageFrom;
        StickersOnPage = stickersOnPage;
        UserBotState = userBotState;
        KeyboardCapture = keyboardCapture;
    }
}