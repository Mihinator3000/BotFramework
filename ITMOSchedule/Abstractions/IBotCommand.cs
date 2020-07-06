﻿using BotFramework.Common;

namespace BotFramework.Abstractions
{
    public interface IBotCommand
    {
        string CommandName { get; }

        string Description { get; }

        string[] Args { get; }

        bool CanExecute(CommandArgumentContainer args);

        Result Execute(CommandArgumentContainer args);
    }
}