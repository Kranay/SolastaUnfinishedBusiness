﻿using System.Linq;
using SolastaUnfinishedBusiness.Api.LanguageExtensions;
using SolastaUnfinishedBusiness.Api.ModKit;
using SolastaUnfinishedBusiness.Models;

namespace SolastaUnfinishedBusiness.Displays;

internal static class SubclassesDisplay
{
    internal static void DisplaySubclasses()
    {
        UI.Label();

        UI.ActionButton(Gui.Localize("ModUi/&DocsSubclasses").Bold().Khaki(),
            () => UpdateContext.OpenDocumentation("SubClasses.md"), UI.Width(150f));

        UI.Label();

        var toggle = Main.Settings.AllowAlliesToPerceiveRangerGloomStalkerInNaturalDarkness;
        if (UI.Toggle(Gui.Localize("ModUi/&AllowAlliesToPerceiveRangerGloomStalkerInNaturalDarkness"), ref toggle,
                UI.AutoWidth()))
        {
            Main.Settings.AllowAlliesToPerceiveRangerGloomStalkerInNaturalDarkness = toggle;
        }

        toggle = Main.Settings.EnableBardHealingBalladOnLongRest;
        if (UI.Toggle(Gui.Localize("ModUi/&EnableBardHealingBalladOnLongRest"), ref toggle, UI.AutoWidth()))
        {
            Main.Settings.EnableBardHealingBalladOnLongRest = toggle;
            CharacterContext.SwitchBardHealingBalladOnLongRest();
        }

        toggle = Main.Settings.RemoveSchoolRestrictionsFromShadowCaster;
        if (UI.Toggle(Gui.Localize("ModUi/&RemoveSchoolRestrictionsFromShadowCaster"), ref toggle, UI.AutoWidth()))
        {
            Main.Settings.RemoveSchoolRestrictionsFromShadowCaster = toggle;
            SrdAndHouseRulesContext.SwitchSchoolRestrictionsFromShadowCaster();
        }

        toggle = Main.Settings.RemoveSchoolRestrictionsFromSpellBlade;
        if (UI.Toggle(Gui.Localize("ModUi/&RemoveSchoolRestrictionsFromSpellBlade"), ref toggle, UI.AutoWidth()))
        {
            Main.Settings.RemoveSchoolRestrictionsFromSpellBlade = toggle;
            SrdAndHouseRulesContext.SwitchSchoolRestrictionsFromSpellBlade();
        }

        UI.Label();

        using (UI.HorizontalScope())
        {
            toggle = Main.Settings.DisplayKlassToggle.All(x => x.Value);
            if (UI.Toggle(Gui.Localize("ModUi/&ExpandAll"), ref toggle, UI.Width(ModUi.PixelsPerColumn)))
            {
                foreach (var key in Main.Settings.DisplayKlassToggle.Keys.ToHashSet())
                {
                    Main.Settings.DisplayKlassToggle[key] = toggle;
                }
            }

            toggle = SubclassesContext.IsAllSetSelected();
            if (UI.Toggle(Gui.Localize("ModUi/&SelectAll"), ref toggle, UI.Width(ModUi.PixelsPerColumn)))
            {
                SubclassesContext.SelectAllSet(toggle);
            }

            toggle = SubclassesContext.IsTabletopSetSelected();
            if (UI.Toggle(Gui.Localize("ModUi/&SelectTabletop"), ref toggle, UI.Width(ModUi.PixelsPerColumn)))
            {
                SubclassesContext.SelectTabletopSet(toggle);
            }
        }

        UI.Div();

        foreach (var kvp in SubclassesContext.Klasses)
        {
            var displayName = kvp.Key;
            var klassName = kvp.Value.Item1;
            var klassDefinition = kvp.Value.Item2;
            var subclassListContext = SubclassesContext.KlassListContextTab[klassDefinition];
            var displayToggle = Main.Settings.DisplayKlassToggle[klassName];
            var sliderPos = Main.Settings.KlassListSliderPosition[klassName];
            var subclassEnabled = Main.Settings.KlassListSubclassEnabled[klassName];

            ModUi.DisplayDefinitions(
                displayName.Khaki(),
                subclassListContext.Switch,
                subclassListContext.AllSubClasses,
                subclassEnabled,
                ref displayToggle,
                ref sliderPos);

            Main.Settings.DisplayKlassToggle[klassName] = displayToggle;
            Main.Settings.KlassListSliderPosition[klassName] = sliderPos;
        }
    }
}
