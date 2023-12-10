using VoiceStickersBot.Infra.VSBApplication.Log;

var log = new ConsoleLog();

try
{
    Foo2();
}
catch (Exception ex)
{
    log.Log(LogLevel.Error, ex, "Test {0} {1}", 1, "fucks");
}

void Foo2()
{
    Foo1();
}

void Foo1()
{
    Foo();
}

void Foo()
{
    throw new NotImplementedException("foo");
}