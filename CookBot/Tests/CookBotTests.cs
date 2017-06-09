﻿using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using source.App;
using source.App.Commands;
using source.Infrastructure.Databases;
using source.Domain.Model;

namespace Tests
{
    [TestFixture]
    class CookBotTests
    {
        IBotCommand fakeCommand;
        string fakeCommandName = "/fakeCommand";
        List<IBotCommand> commandsList;
        IDatabase<Recipe> database;
        CookBot cookBot;

        [SetUp]
        public void SetUp()
        {
            fakeCommand = A.Fake<IBotCommand>();
            commandsList = new List<IBotCommand>();
            commandsList.Add(fakeCommand);

            A.CallTo(() => fakeCommand.Name).Returns(fakeCommandName);
            database = A.Fake<IDatabase<Recipe>>();
            cookBot = new CookBot(database, commandsList);
        }

        [Test]
        public void should_find_and_execute_command()
        {
            var expectedResult = "expectedResult";

            A.CallTo(() => fakeCommand.Execute(A<IDatabase<Recipe>>.Ignored, 
                A<string[]>.Ignored)).Returns(expectedResult);

            
            var commandResult = cookBot.HandleCommand(fakeCommandName);
            Assert.AreEqual(commandResult, "expectedResult");
        }

        [Test]
        public void should_not_find_and_execute_command()
        {
            cookBot.HandleCommand("Non-existent command");
            A.CallTo(() => fakeCommand.Execute(A<IDatabase<Recipe>>.Ignored,
                A<string[]>.Ignored)).MustNotHaveHappened();
        }

        [Test]
        public void should_parse_arguments()
        {
            var arguments = new string[] { "arg1", "arg2" };
            cookBot.HandleCommand("/fakeCommand arg1 arg2");
            A.CallTo(() => fakeCommand.Execute(A<IDatabase<Recipe>>.Ignored, arguments))
                .WhenArgumentsMatch((IDatabase<Recipe> db, string[] str) => str[0] == "arg1" && str[1] == "arg2")
                .MustHaveHappened();
        }
    }
}
