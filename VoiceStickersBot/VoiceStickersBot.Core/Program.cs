using VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

var m = new MainCommandHandler(new List<ICommandHandlerFactory> { new SwitchKeyboardCommandHandlerFactory() });
var t = m.Handle(new SwitchKeyboardCommand(1, 10, "pageright"));
var c = t as SwitchKeyboardResult;
foreach (var k in c.InlineKeyboardDto.Buttons) Console.WriteLine(k.ButtonText);

// var container = new StandardKernel();
//
// container.Bind<IVsbDatabaseCluster>().To<PostgresVsbDatabaseCluster>();
// container.Bind<IVsbDatabaseOptionsProvider>().To<PostgresVsbDatabaseOptionsProvider>();
//
// var t = container.Get<SchemaConfiguratorCore>();
// t.Configure();
//
// var cl = container.Get<IVsbDatabaseCluster>();
// using var db = cl.GetTable<UserEntity>();
// await db.PerformCreateRequestAsync(new UserEntity(){Id="123", Text = "123"}, new CancellationToken());
//
// Console.WriteLine((await db.PerformReadonlyRequestAsync(x => x.Where(entity => entity.Id == "123")
//     , new CancellationToken()))[0].Id);