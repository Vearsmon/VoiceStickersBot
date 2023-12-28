/*using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.CommandsFactory;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.CommandsObsolete.CommandsFactoryObsolete;

public class SwitchKeyboardCommandFactoryObsolete : CommandFactoryObsoleteBaseObsolete
{
    public override IReadOnlyList<string> CommandPrefixes { get; } = new[] {"pageleft", "pageright"};

    public override ICommandObsolete CreateCommand(RequestContextObsolete requestContext)
    {
        var pageFrom = int.Parse(requestContext.CommandText.Split(':')[1]);
        //В команду лучше добавить список всех единиц (паков или стикеров),
        //чтото обобщенное чтобы можно было использовать один метод для паков и стикеров
        return new SwitchKeyboardCommandObsolete(requestContext, pageFrom, requestContext.CommandText, 10);
    }
}*/