namespace VoiceStickersBot.Core.Commands.SwitchKeyboard;

public record InlineKeyboardDto(List<InlineKeyboardButtonDto> Buttons, List<InlineKeyboardButtonDto> LastButtons);