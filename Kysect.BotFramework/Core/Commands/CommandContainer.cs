﻿using System.Collections.Generic;
using System.Linq;
using Kysect.BotFramework.Core.BotMedia;

namespace Kysect.BotFramework.Core.Commands
{
    public class CommandContainer
    {
        public string CommandName { get; private set; }
        public List<string> Arguments { get; }
        public List<IBotMediaFile> MediaFiles { get; }

        public CommandContainer(string commandName, List<string> arguments, List<IBotMediaFile> mediaFiles)
        {
            CommandName = commandName;
            Arguments = arguments;
            MediaFiles = mediaFiles;
        }

        public bool StartsWithPrefix(char prefix) => prefix == '\0' || CommandName.FirstOrDefault() == prefix;

        public CommandContainer RemovePrefix(char prefix)
        {
            if (CommandName.FirstOrDefault() == prefix)
            {
                CommandName = CommandName.Remove(0, 1);
            }

            return this;
        }

        public override string ToString() =>
            $"[CommandArgumentContainer CommandName:{CommandName}; Arguments:{string.Join(",", Arguments)}]";
    }
}