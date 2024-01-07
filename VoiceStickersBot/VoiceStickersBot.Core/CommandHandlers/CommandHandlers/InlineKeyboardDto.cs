namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

public record InlineKeyboardDto(List<List<InlineKeyboardButtonDto>> ButtonsRows, List<InlineKeyboardButtonDto> LastButtons);