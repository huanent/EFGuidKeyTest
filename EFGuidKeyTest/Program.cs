using System;
using System.Threading.Tasks;

namespace EFGuidKeyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new AppDbContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            for (int i = 0; i < 100; i++)
            {
                if (i / 2 == 0)
                {
                    context.User.Add(new User
                    {
                        Sequence = i,
                    });
                }
                else
                {
                    context.User.Add(new User
                    {
                        Sequence = i,
                        //如果希望手动生成则使用SequenceGuidGenerator来生成
                        Id = SequenceGuidGenerator.SqlServerKey()
                    });
                }
                Task.Delay(500).Wait();
                context.SaveChanges();
            }
            Console.WriteLine("生成序列完成");
            Console.ReadKey();
        }
    }
}
