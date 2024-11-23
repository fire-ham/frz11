using Dalamud.Game.Command;
using Dalamud.Logging;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using System;
using System.Threading;

namespace FreezeGamePlugin
{
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "Freeze Game Plugin";

        private const string CommandName = "/freezegame";

        private readonly ICommandManager commandManager;

        public Plugin(ICommandManager commandManager)
        {
            this.commandManager = commandManager;

            // register command
            this.commandManager.AddHandler(CommandName, new CommandInfo(OnFreezeGame)
            {
                HelpMessage = "Freezes the game for the amount of time specified in seconds, up to 60. Defaults to 0.5."
            });

           // PluginLog.Information("Plugin initialized successfully.");
        }

        public void Dispose()
        {
            //unregister commandr
            this.commandManager.RemoveHandler(CommandName);

           // PluginLog.Information("Plugin disposed successfully.");
        }

        private void OnFreezeGame(string command, string args)
        {
            if (!float.TryParse(args, out var time))
                time = 0.5f;

            time = Math.Min(time, 60);

           // PluginLog.Information($"{Name}: Freezing game for {time} seconds.");

            Thread.Sleep((int)(time * 1000)); // Simulate game freeze

           // PluginLog.Information("Game resumed.");
        }
    }
}
