﻿using ModKit;
using SolastaCommunityExpansion.Models;

namespace SolastaCommunityExpansion.Viewers.Displays
{
    internal static class ToolsDisplay
    {
        private static bool enableDebugCamera;
        private static bool enableDebugOverlay;

        internal static void DisplayTools()
        {
            bool toggle;
            int intValue;

            UI.Label("");
            UI.Label("Custom dungeons:".yellow());
            UI.Label("");

            toggle = Main.Settings.EnableDungeonLevelBypass;
            if (UI.Toggle("Overrides required min / max level", ref toggle))
            {
                Main.Settings.EnableDungeonLevelBypass = toggle;
            }

            UI.Label("");

            intValue = Main.Settings.UserDungeonsPartySize;
            if (UI.Slider("Overrides party size".white(), ref intValue, Settings.MIN_PARTY_SIZE, Settings.MAX_PARTY_SIZE, Settings.GAME_PARTY_SIZE, "", UI.AutoWidth()))
            {
                Main.Settings.UserDungeonsPartySize = intValue;
            }

            UI.Label("");
            UI.Label("Debug:".yellow());
            UI.Label("");

            toggle = Main.Settings.EnableCheatMenuDuringGameplay;
            if (UI.Toggle("Enables the cheats menu", ref toggle, UI.AutoWidth()))
            {
                Main.Settings.EnableCheatMenuDuringGameplay = toggle;
            }

            if (UI.Toggle("Enables the debug camera", ref enableDebugCamera, UI.AutoWidth()))
            {
                IViewService viewService = ServiceRepository.GetService<IViewService>();
                ICameraService cameraService = ServiceRepository.GetService<ICameraService>();

                if (viewService == null || cameraService == null)
                {
                    enableDebugCamera = false;
                }
                else
                {
                    cameraService.DebugCameraEnabled = enableDebugCamera;
                }
            }

            if (UI.Toggle("Enables the debug overlay", ref enableDebugOverlay, UI.AutoWidth()))
            {
                ServiceRepository.GetService<IDebugOverlayService>().ToggleActivation();
            }

            UI.Label("");
            UI.Label("Experience:".yellow());
            UI.Label("");

            toggle = Main.Settings.NoExperienceOnLevelUp;
            if (UI.Toggle("No experience is required to level up", ref toggle, UI.AutoWidth()))
            {
                Main.Settings.NoExperienceOnLevelUp = toggle;
            }

            UI.Label("");

            intValue = Main.Settings.ExperienceModifier;
            if (UI.Slider("Multiplies the experience gained by ".white() + "[%]".red(), ref intValue, 50, 200, 100, "", UI.Width(100)))
            {
                Main.Settings.ExperienceModifier = intValue;
            }

            UI.Label("");
            UI.Label("Faction Relations:".yellow());
            UI.Label("");

            bool flip = true;
            var gameService = ServiceRepository.GetService<IGameService>();
            var gameFactionService = ServiceRepository.GetService<IGameFactionService>();

            if (gameFactionService != null && gameService?.Game?.GameCampaign?.CampaignDefinitionName?.Contains("UserCampaign") == false)
            {
                foreach (FactionDefinition faction in gameFactionService.RegisteredFactions)
                {
                    if (faction.BuiltIn)
                    {
                        // These are things like monster factions, generally set to a specific relation and can't be changed.
                        continue;
                    }

                    if (faction.GuiPresentation.Hidden)
                    {
                        // These are things like Silent Whispers and Church Of Einar that are not fully implemented factions.
                        continue;
                    }

                    string title = faction.FormatTitle();

                    if (flip)
                    {
                        title = title.yellow();
                    }
                    else
                    {
                        title = title.white();
                    }

                    intValue = gameFactionService.FactionRelations[faction.Name];

                    if (UI.Slider("                              " + title, ref intValue, faction.MinRelationCap, faction.MaxRelationCap, 0, "", UI.AutoWidth()))
                    {
                        SetFactionRelationsContext.SetFactionRelation(faction.Name, intValue);
                    }

                    flip = !flip;
                }
            }
            else
            {
                UI.Label("Load an official campaign game to modify faction relations...".red());
            }

            UI.Label("");
        }
    }
}
