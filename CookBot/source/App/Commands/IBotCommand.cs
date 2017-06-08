﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using source.Domain.Model;
using source.Infrastructure.Databases;

namespace source.App.Commands
{
    public enum BotCommandResult
    {
        Good,
        Bad
    }

    public interface IBotCommand
    {
        string Name { get; }
        string Description { get; }
        string Result { get; }
        BotCommandResult Execute(IDatabase<Recipe> db, string[] arguments);
    }
}
