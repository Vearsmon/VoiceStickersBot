using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ninject;
using VoiceStickersBot.Infra.VsbDatabaseCluster;
using VoiceStickersBot.Infra.VsbDatabaseClusterProvider;

var container = new StandardKernel();
container.Bind<IVsbDatabaseCluster>().To<PostgresVsbDatabaseCluster>();
container.Bind<IVsbDatabaseOptionsProvider>().To<PostgresVsbDatabaseOptionsProvider>();


var cluster = container.Get<IVsbDatabaseCluster>();


Console.WriteLine(nameof(IVsbDatabaseCluster.GetSchemaCreator));

// var container = new StandardKernel();
// container.Bind<IVsbDatabaseCluster>().To<PostgresVsbDatabaseCluster>();
// container.Bind<IVsbDatabaseOptionsProvider>().To<PostgresVsbDatabaseOptionsProvider>();
//
//
// var cluster = container.Get<IVsbDatabaseCluster>();
// using var t = cluster.GetTable<User>();
// using var t2 = cluster.GetTable<User>();
//
// [Table("Users")]
// public class User
// {
//     [Key] public int Id { get; set; }
//
//     public string? Name { get; set; }
//     public int Age { get; set; }
//
//     public override string ToString()
//     {
//         return $"Id: {Id}; Name: {Name}; Age: {Age}";
//     }
// }