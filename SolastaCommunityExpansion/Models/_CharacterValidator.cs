﻿using System.Linq;
using JetBrains.Annotations;
using SolastaCommunityExpansion.Api;
using SolastaCommunityExpansion.Api.Extensions;

namespace SolastaCommunityExpansion.Models;

public delegate bool CharacterValidator(RulesetCharacter character);

public static class CharacterValidators
{
    public static readonly CharacterValidator HasAttacked = character => character.ExecutedAttacks > 0;
    public static readonly CharacterValidator NoArmor = character => !character.IsWearingArmor();
    public static readonly CharacterValidator MediumArmor = character => character.IsWearingMediumArmor();
    public static readonly CharacterValidator NoShield = character => !character.IsWearingShield();
    public static readonly CharacterValidator HasShield = character => character.IsWearingShield();

    public static readonly CharacterValidator EmptyOffhand = character =>
        character.CharacterInventory.InventorySlotsByName[EquipmentDefinitions.SlotTypeOffHand].EquipedItem == null;

    public static readonly CharacterValidator HasPolearm = character =>
    {
        var slotsByName = character.CharacterInventory.InventorySlotsByName;
        return WeaponValidators.IsPolearm(slotsByName[EquipmentDefinitions.SlotTypeMainHand].EquipedItem)
               || WeaponValidators.IsPolearm(slotsByName[EquipmentDefinitions.SlotTypeOffHand].EquipedItem);
    };
    
    public static readonly CharacterValidator HasTwoHandedRangeWeapon = character =>
    {
        var slotsByName = character.CharacterInventory.InventorySlotsByName;
        var equipedItem = slotsByName[EquipmentDefinitions.SlotTypeMainHand].EquipedItem.ItemDefinition;
        return equipedItem.WeaponDescription.WeaponTypeDefinition == DatabaseHelper.WeaponTypeDefinitions.LongbowType
               || equipedItem.WeaponDescription.WeaponTypeDefinition ==
               DatabaseHelper.WeaponTypeDefinitions.ShortbowType
               || equipedItem.WeaponDescription.WeaponTypeDefinition ==
               DatabaseHelper.WeaponTypeDefinitions.LightCrossbowType
               || equipedItem.WeaponDescription.WeaponTypeDefinition ==
               DatabaseHelper.WeaponTypeDefinitions.HeavyCrossbowType;
    };

    public static readonly CharacterValidator MainHandIsMeleeWeapon = character =>
        WeaponValidators.IsMelee(character.GetItemInSlot(EquipmentDefinitions.SlotTypeMainHand));

    public static readonly CharacterValidator FullyUnarmed = character =>
    {
        var slotsByName = character.CharacterInventory.InventorySlotsByName;
        return WeaponValidators.IsUnarmedWeapon(slotsByName[EquipmentDefinitions.SlotTypeMainHand].EquipedItem)
               && WeaponValidators.IsUnarmedWeapon(slotsByName[EquipmentDefinitions.SlotTypeOffHand].EquipedItem);
    };

    public static readonly CharacterValidator HasUnarmedHand = character =>
    {
        var slotsByName = character.CharacterInventory.InventorySlotsByName;

        return WeaponValidators.IsUnarmedWeapon(slotsByName[EquipmentDefinitions.SlotTypeMainHand].EquipedItem)
               || WeaponValidators.IsUnarmedWeapon(slotsByName[EquipmentDefinitions.SlotTypeOffHand].EquipedItem);
    };

    public static readonly CharacterValidator UsedAllMainAttacks = character =>
        character.ExecutedAttacks >= character.GetAttribute(AttributeDefinitions.AttacksNumber).CurrentValue;

    public static readonly CharacterValidator InBattle = _ =>
        ServiceRepository.GetService<IGameLocationBattleService>().IsBattleInProgress;

    public static readonly CharacterValidator LightArmor = character =>
    {
        var equipedItem = character.CharacterInventory.InventorySlotsByName[EquipmentDefinitions.SlotTypeTorso]
            .EquipedItem;
        if (equipedItem == null || !equipedItem.ItemDefinition.IsArmor)
        {
            return false;
        }

        var armorDescription = equipedItem.ItemDefinition.ArmorDescription;
        var element = DatabaseRepository.GetDatabase<ArmorTypeDefinition>().GetElement(armorDescription.ArmorType);
        return DatabaseRepository.GetDatabase<ArmorCategoryDefinition>().GetElement(element.ArmorCategory)
            .IsPhysicalArmor && element.ArmorCategory == EquipmentDefinitions.LightArmorCategory;
    };

    [NotNull]
    public static CharacterValidator HasAnyOfConditions(params ConditionDefinition[] conditions)
    {
        return character => conditions.Any(c => character.HasConditionOfType(c.Name));
    }

    [NotNull]
    public static CharacterValidator HasAnyOfConditions(params string[] conditions)
    {
        return character => conditions.Any(character.HasConditionOfType);
    }

    [NotNull]
    public static CharacterValidator HasBeenGrantedFeature(FeatureDefinition feature)
    {
        return character =>
        {
            Main.Log($"Checking for {feature.Name}", true);
            return character is RulesetCharacterHero hero &&
                   hero.activeFeatures.Any(item => item.Value.Contains(feature));
        };
    }
}
