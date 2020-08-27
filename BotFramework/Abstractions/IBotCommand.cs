﻿using System.Threading.Tasks;
using Tef.BotFramework.Common;

namespace Tef.BotFramework.Abstractions
{
    public interface IBotCommand
    {
        string CommandName { get; }

        string Description { get; }

        string[] Args { get; }

        bool CanExecute(CommandArgumentContainer args);

        Task<Result<string>> ExecuteAsync(CommandArgumentContainer args);
    }

    public static class BotCommandExtensions
    {
        public static Result<string> Execute(this IBotCommand command, CommandArgumentContainer args)
        {
            Task<Result<string>> task = command.ExecuteAsync(args);
            task.RunSynchronously();
            return task.Result;
        }
    }
}