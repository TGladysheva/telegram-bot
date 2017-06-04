﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBot.Domain.Model;
using CookBot.Infrastructure.Databases;

namespace source.App.Commands
{
    class RecipeListCommand : IBotCommand
    {
        public string Name => "/recipeList";

        public string Description => "отобразить список всех рецептов";

        public string Execute(IDatabase<Recipe> db, params string[] arguments)
        {
            var recipesNames = db.GetAllSuitable(_ => true).Select(recipe => recipe.Name).ToArray();
            var result = new StringBuilder();
            for (int i = 0; i < recipesNames.Length; i++)
            {
                result.Append((i + 1) + ". " + recipesNames[i] + "\n");
            }
            return result.ToString();
        }
    }
}