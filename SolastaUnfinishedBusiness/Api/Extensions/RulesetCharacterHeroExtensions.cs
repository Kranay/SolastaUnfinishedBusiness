﻿using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SolastaUnfinishedBusiness.Api.Extensions;

internal static class RulesetCharacterHeroExtensions
{
    [NotNull]
    internal static List<(string, T)> GetTaggedFeaturesByType<T>([NotNull] this RulesetCharacterHero hero) where T : class
    {
        var list = new List<(string, T)>();

        foreach (var pair in hero.ActiveFeatures)
        {
            list.AddRange(GetTaggedFeatures<T>(pair.Key, pair.Value));
        }

        return list;
    }

    [NotNull]
    private static IEnumerable<(string, T)> GetTaggedFeatures<T>(
        string tag,
        [NotNull] IEnumerable<FeatureDefinition> features)
        where T : class
    {
        var list = new List<(string, T)>();
        foreach (var feature in features)
        {
            switch (feature)
            {
                case FeatureDefinitionFeatureSet { Mode: FeatureDefinitionFeatureSet.FeatureSetMode.Union } set:
                    list.AddRange(GetTaggedFeatures<T>(tag, set.FeatureSet));
                    break;

                case T typedFeature:
                    list.Add((tag, typedFeature));
                    break;
            }
        }

        return list;
    }

    internal static bool IsWearingLightArmor([NotNull] this RulesetCharacterHero hero)
    {
        var equipedItem = hero.characterInventory.InventorySlotsByName[EquipmentDefinitions.SlotTypeTorso].EquipedItem;

        if (equipedItem == null || !equipedItem.ItemDefinition.IsArmor)
        {
            return false;
        }

        var armorDescription = equipedItem.ItemDefinition.ArmorDescription;
        var element = DatabaseRepository.GetDatabase<ArmorTypeDefinition>().GetElement(armorDescription.ArmorType);

        return DatabaseRepository.GetDatabase<ArmorCategoryDefinition>()
                   .GetElement(element.ArmorCategory).IsPhysicalArmor
               && element.ArmorCategory == EquipmentDefinitions.LightArmorCategory;
    }

    internal static bool IsWearingMediumArmor([NotNull] this RulesetCharacterHero hero)
    {
        var equipedItem = hero.characterInventory.InventorySlotsByName[EquipmentDefinitions.SlotTypeTorso].EquipedItem;

        if (equipedItem == null || !equipedItem.ItemDefinition.IsArmor)
        {
            return false;
        }

        var armorDescription = equipedItem.ItemDefinition.ArmorDescription;
        var element = DatabaseRepository.GetDatabase<ArmorTypeDefinition>().GetElement(armorDescription.ArmorType);

        return DatabaseRepository.GetDatabase<ArmorCategoryDefinition>()
                   .GetElement(element.ArmorCategory).IsPhysicalArmor
               && element.ArmorCategory == EquipmentDefinitions.MediumArmorCategory;
    }

    internal static bool IsWieldingTwoHandedWeapon([NotNull] this RulesetCharacterHero hero)
    {
        var equipedItem = hero.characterInventory.InventorySlotsByName[EquipmentDefinitions.SlotTypeMainHand]
            .EquipedItem;

        if (equipedItem != null && equipedItem.ItemDefinition.IsWeapon)
        {
            return equipedItem.ItemDefinition.activeTags.Contains("TwoHanded");
        }

        return false;
    }

    internal static int GetClassLevel(this RulesetCharacterHero hero, CharacterClassDefinition classDefinition)
    {
        return classDefinition != null && hero.ClassesAndLevels.TryGetValue(classDefinition, out var classLevel)
            ? classLevel
            : 0;
    }

    internal static int GetClassLevel(this RulesetCharacterHero hero, string className)
    {
        return hero.GetClassLevel(DatabaseRepository.GetDatabase<CharacterClassDefinition>()
            .FirstOrDefault(x => x.Name == className));
    }

    internal static RulesetItem GetMainWeapon(this RulesetCharacterHero hero)
    {
        var slotsByName = hero.CharacterInventory.InventorySlotsByName;
        return slotsByName[EquipmentDefinitions.SlotTypeMainHand].EquipedItem;
    }
}
