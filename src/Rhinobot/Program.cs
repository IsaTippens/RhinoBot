using System;
using System.Threading;
using System.Threading.Tasks;
using RhinoBot;


namespace RhinoBot
{
    public class Program
    {
        static void Main(string[] args) 
        {
            string tokenID = args[0];
            Environment.SetEnvironmentVariable("TOKEN_ID", tokenID, EnvironmentVariableTarget.Process);
            new RhinoBot().Run().GetAwaiter().GetResult();
        }

    }
}
