using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Security.Cryptography;
using VoterBot.Commands;
using VoterBot.Interface;

namespace VoterBot.ServiceCommand
{
    public class CommandFactory
    {
        private static Dictionary<string, Command> _commands { get; set; }
        public CommandFactory()
        {
            _commands = _commands ?? Register();
        }
        public static Command GetCommand(string key) =>
            _commands.ContainsKey(key) ? _commands[key] : null;
        
        public static Dictionary<string, Command> Register()
        {
            _commands = new Dictionary<string, Command>();
            _commands.Add(CommandWords.START, new StartCommand());

            return _commands;
        }

        public static class CommandWords
        {
            public const string START ="/start";
        }
    }
}
