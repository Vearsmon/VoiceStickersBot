using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ninject;
using SkbKontur.Cassandra.TimeBasedUuid;
using VoiceStickersBot.Infra.ObjectStorageCluster;
using VoiceStickersBot.Infra.VsbDatabaseCluster;
using VoiceStickersBot.Infra.VsbDatabaseClusterProvider;


var container = new StandardKernel();
container.Bind<IVsbDatabaseCluster>().To<PostgresVsbDatabaseCluster>();
container.Bind<IVsbDatabaseOptionsProvider>().To<PostgresVsbDatabaseOptionsProvider>();
container.Bind<IObjectStorageClient>().To<ObjectStorageClient>();

var a = new ObjectStorageClient();
var b = await ObjectStorageExtensions.GetStorageBuckets(a.objectStorageClient).ConfigureAwait(false);
// var c = a.GetObjectFromStorage(new ObjectLocation(b[0].BucketName, "09 Код удаления (320 kbps).mp3")).Result;
// var path = @"C:\Users\Ilya\Desktop\123.mp3";
// File.WriteAllBytes(path, c);
var path = @"C:\Users\Ilya\Desktop\Без123ымянный.png";
var byteArray = File.ReadAllBytes(path);
var d = await a.PutObjectInStorage(b[0].BucketName, TimeGuid.NewGuid(Timestamp.Now).ToGuid(), "image/png", byteArray).ConfigureAwait(false);



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