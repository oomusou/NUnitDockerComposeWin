using System;
using System.Threading.Tasks;
using System.Xml;
using static DockerLib.DockerUtil;

namespace DockerLib
{
    public static class DockerHelper
    {
        internal const string DatabaseName = "docker";
        private const string Username = "admin";
        private const string Password = "12345";
        private const int NewLineAscii = 10;
        private static char NewLine => Convert.ToChar(NewLineAscii);
        private static string Healthy => "\"healthy\"" + NewLine;
        private static bool IsPostgresHealthy(string command) => Run(command).Equals(Healthy);
        private static void Sleep(int time) => Task.Delay(time).Wait();

        public static Container CreateContainer()
        {
            return new Container(RandomPort)
                .DockerComposeUp()
                .WaitForPostgres();
        }

        internal static void DestroyContainer(Container container)
        {
            DockerComposeDown(container);
        }

        internal static void PruneSystem()
        {
            var command = "docker system prune --force --volumes";
            Run(command);
        }

        public static Container DockerComposeUp(this Container container)
        {
            var command = $"SET POSTGRES_PORT={container.Port}&" +
                          $"SET POSTGRES_DB={DatabaseName}&" +
                          $"SET POSTGRES_USER={Username}&" +
                          $"SET POSTGRES_PASSWORD={Password}&" +
                          $"docker-compose -p {container.ProjectName} up -d";

            Run(command);

            return container;
        }

        public static void DockerComposeDown(this Container container)
        {
            var command = $"SET POSTGRES_PORT={container.Port}&" + 
                          $"SET POSTGRES_DB={DatabaseName}&" + 
                          $"SET POSTGRES_USER={Username}&" + 
                          $"SET POSTGRES_PASSWORD={Password}&" + 
                          $"docker-compose -p {container.ProjectName} down";
            Run(command);
        }

        public static Container WaitForPostgres(this Container container)
        {
            var command = 
                "docker inspect --format=\"{{json .State.Health.Status}}\" " + 
                container.ContainerName;

            while (true)
            {
                if (IsPostgresHealthy(command)) break;

                Sleep(1000);
            }
            
            Sleep(5000);
            return container;
        }
    }
}