﻿using System.Collections.Generic;
using BotFramework.Abstractions;
using BotFramework.Common;

namespace BotFramework.BotFramework.CommandControllers
{
    public class CommandsList
    {
        public readonly Dictionary<string, IBotCommand> Commands = new Dictionary<string, IBotCommand>();

        public void AddCommand(IBotCommand command)
        {
            Commands.TryAdd(command.CommandName, command);
        }

        public Result<IBotCommand> GetCommand(string commandName)
        {
            return Commands.TryGetValue(commandName, out IBotCommand command)
                ? new Result<IBotCommand>(true, command)
                : new Result<IBotCommand>(false, $"command {commandName} not founded", null);
        }
    }
}