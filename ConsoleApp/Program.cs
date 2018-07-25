using System;
using ClassLib;
using DockerLib;
using Microsoft.EntityFrameworkCore;
using static DockerLib.DockerUtil;
using static System.Linq.Enumerable;

namespace ConsoleApp
{
    internal static class Program
    {
        private static void Main()
        {
            void RunEachDockerTest(int i) 
                => DockerTest(() => Console.WriteLine(i + " pass"));

            Range(0, 16).ForEach(RunEachDockerTest);
        }

        private static void DockerTest(Action testing)
        {
            Dockery.Migration = RunMigration;
            var containerInfo = Dockery.BeginTest();
            testing();
            Dockery.EndTest(containerInfo);
        }

        private static void RunMigration(this Container container)
        {
            var crmDbContext = new CrmDbContext(container.Port);
            crmDbContext.Database.SetCommandTimeout(300);
            crmDbContext.Database.Migrate();
        }
    }
}