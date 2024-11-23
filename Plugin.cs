using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System;
using System.Threading;

namespace FreezeGamePlugin
{
    public class Plugin : IDalamudPlugin
    {
        public string Name => "Freeze Game Plugin";

        private const string CommandName = "/freezegame";

        [PluginService]
        public static DalamudPluginInterface PluginInterface { get; private set; } = null!;

        [PluginService]
        public static CommandManager CommandManager { get; private set; } = null!;

        public void Initialize()
        {
            CommandManager.AddHandler(CommandName, new CommandInfo(OnFreezeGame)
            {
                HelpMessage = "Freezes the game for the amount of time specified in seconds, up to 60. Defaults to 0.5."
            });
        }

        public void Dispose()
        {
            CommandManager.RemoveHandler(CommandName);
        }

        private void OnFreezeGame(string command, string args)
        {
            if (!float.TryParse(args, out var time))
            {
                time = 0.5f;
            }

            time = Math.Min(time, 60);

            PluginLog.Log($"Freezing game for {time} seconds.");
            Thread.Sleep((int)(time * 1000)); // Simulate freeze
            PluginLog.Log("Game resumed.");
        }
    }
}
