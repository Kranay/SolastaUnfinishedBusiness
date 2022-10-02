using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using SolastaUnfinishedBusiness.Api.Diagnostics;
using SolastaUnfinishedBusiness.Api.Infrastructure;
using static RuleDefinitions;

namespace SolastaUnfinishedBusiness.Api.Extensions;

#if DEBUG
[TargetType(typeof(EffectDescription))]
#endif
[GeneratedCode("Unfinished Business Extension Generator", "1.0.0")]
internal static class EffectDescriptionExtensions
{
    [NotNull]
    internal static T Create<T>([NotNull] this T entity, T copy)
        where T : EffectDescription
    {
        entity.Copy(copy);
        return entity;
    }

    [NotNull]
    internal static T SetDuration<T>([NotNull] this T entity, DurationType type, int? duration = null)
        where T : EffectDescription
    {
        switch (type)
        {
            case DurationType.Round:
            case DurationType.Minute:
            case DurationType.Hour:
            case DurationType.Day:
                if (duration == null)
                {
                    throw new ArgumentNullException(nameof(duration),
                        $@"A duration value is required for duration type {type}.");
                }

                entity.SetDurationParameter(duration.Value);
                break;

            case DurationType.Instantaneous:
            case DurationType.Dispelled:
            case DurationType.Permanent:
            case DurationType.Irrelevant:
            case DurationType.UntilShortRest:
            case DurationType.UntilLongRest:
            case DurationType.UntilAnyRest:
            case DurationType.Deprecated_Turn:
            case DurationType.HalfClassLevelHours:
            default:
                if (duration != null)
                {
                    throw new SolastaUnfinishedBusinessException(
                        $"A duration value is not expected for duration type {type}");
                }

                entity.SetDurationParameter(0);
                break;
        }

        entity.SetDurationType(type);

        return entity;
    }

    [NotNull]
    internal static T SetRange<T>([NotNull] this T entity, RangeType type, int? range = null)
        where T : EffectDescription
    {
        switch (type)
        {
            case RangeType.RangeHit:
            case RangeType.Distance:
                if (range == null)
                {
                    throw new ArgumentNullException(nameof(range),
                        $@"A range value is required for range type {type}.");
                }

                entity.SetRangeParameter(range.Value);
                break;

            case RangeType.Touch:
                entity.SetRangeParameter(range ?? 0);
                break;

            case RangeType.Self:
            case RangeType.MeleeHit:
            default:
                if (range != null)
                {
                    throw new SolastaUnfinishedBusinessException(
                        $"A duration value is not expected for duration type {type}");
                }

                entity.SetRangeParameter(0);
                break;
        }

        entity.SetRangeType(type);

        return entity;
    }

    [NotNull]
    internal static T AddEffectForms<T>([NotNull] this T entity, params EffectForm[] value)
        where T : EffectDescription
    {
        AddEffectForms(entity, value.AsEnumerable());
        return entity;
    }

    [NotNull]
    internal static T AddEffectForms<T>([NotNull] this T entity, [NotNull] IEnumerable<EffectForm> value)
        where T : EffectDescription
    {
        entity.EffectForms.AddRange(value);
        return entity;
    }

    [CanBeNull]
    internal static DamageForm FindLastDamageForm([NotNull] this EffectDescription entity)
    {
        DamageForm form = null;

        foreach (var effectForm in entity.effectForms.Where(effectForm =>
                     effectForm.FormType == EffectForm.EffectFormType.Damage))
        {
            form = effectForm.DamageForm;
        }

        return form;
    }

    [NotNull]
    internal static T ClearRestrictedCreatureFamilies<T>([NotNull] this T entity)
        where T : EffectDescription
    {
        entity.RestrictedCreatureFamilies.Clear();
        return entity;
    }

    [NotNull]
    internal static EffectDescription Copy(this EffectDescription entity)
    {
        var effectDescription = new EffectDescription();

        effectDescription.Copy(entity);

        return effectDescription;
    }

    [NotNull]
    internal static T SetAnimationMagicEffect<T>([NotNull] this T entity, AnimationDefinitions.AnimationMagicEffect value)
        where T : EffectDescription
    {
        entity.animationMagicEffect = value;
        return entity;
    }

    [NotNull]
    internal static T SetCanBeDispersed<T>([NotNull] this T entity, Boolean value)
        where T : EffectDescription
    {
        entity.canBeDispersed = value;
        return entity;
    }

    [NotNull]
    internal static T SetCanBePlacedOnCharacter<T>([NotNull] this T entity, Boolean value)
        where T : EffectDescription
    {
        entity.canBePlacedOnCharacter = value;
        return entity;
    }

    [NotNull]
    internal static T SetCreatedByCharacter<T>([NotNull] this T entity, Boolean value)
        where T : EffectDescription
    {
        entity.createdByCharacter = value;
        return entity;
    }

    [NotNull]
    internal static T SetDifficultyClassComputation<T>([NotNull] this T entity, EffectDifficultyClassComputation value)
        where T : EffectDescription
    {
        entity.difficultyClassComputation = value;
        return entity;
    }

    [NotNull]
    internal static T SetDisableSavingThrowOnAllies<T>([NotNull] this T entity, Boolean value)
        where T : EffectDescription
    {
        entity.disableSavingThrowOnAllies = value;
        return entity;
    }

    [NotNull]
    internal static T SetDurationParameter<T>([NotNull] this T entity, Int32 value)
        where T : EffectDescription
    {
        entity.DurationParameter = value;
        return entity;
    }

    [NotNull]
    internal static T SetDurationType<T>([NotNull] this T entity, DurationType value)
        where T : EffectDescription
    {
        entity.DurationType = value;
        return entity;
    }

    [NotNull]
    internal static T SetEffectAdvancement<T>([NotNull] this T entity, EffectAdvancement value)
        where T : EffectDescription
    {
        entity.effectAdvancement = value;
        return entity;
    }

    [NotNull]
    internal static T SetEffectAIParameters<T>([NotNull] this T entity, EffectAIParameters value)
        where T : EffectDescription
    {
        entity.effectAIParameters = value;
        return entity;
    }

    [NotNull]
    internal static T SetEffectForms<T>([NotNull] this T entity, params EffectForm[] value)
        where T : EffectDescription
    {
        SetEffectForms(entity, value.AsEnumerable());
        return entity;
    }

    [NotNull]
    internal static T SetEffectForms<T>([NotNull] this T entity, IEnumerable<EffectForm> value)
        where T : EffectDescription
    {
        entity.EffectForms.SetRange(value);
        return entity;
    }

    [NotNull]
    internal static T SetEffectParticleParameters<T>([NotNull] this T entity, IMagicEffect magic)
        where T : EffectDescription
    {
        entity.effectParticleParameters = magic.EffectDescription.EffectParticleParameters;
        return entity;
    }

    [NotNull]
    internal static T SetEffectPoolAmount<T>([NotNull] this T entity, Int32 value)
        where T : EffectDescription
    {
        entity.effectPoolAmount = value;
        return entity;
    }

    [NotNull]
    internal static T SetEmissiveBorder<T>([NotNull] this T entity, EmissiveBorder value)
        where T : EffectDescription
    {
        entity.emissiveBorder = value;
        return entity;
    }

    [NotNull]
    internal static T SetEmissiveParameter<T>([NotNull] this T entity, Int32 value)
        where T : EffectDescription
    {
        entity.emissiveParameter = value;
        return entity;
    }

    [NotNull]
    internal static T SetEndOfEffect<T>([NotNull] this T entity, TurnOccurenceType value)
        where T : EffectDescription
    {
        entity.EndOfEffect = value;
        return entity;
    }

    [NotNull]
    internal static T SetFixedSavingThrowDifficultyClass<T>([NotNull] this T entity, Int32 value)
        where T : EffectDescription
    {
        entity.FixedSavingThrowDifficultyClass = value;
        return entity;
    }

    [NotNull]
    internal static T SetHalfDamageOnAMiss<T>([NotNull] this T entity, Boolean value)
        where T : EffectDescription
    {
        entity.halfDamageOnAMiss = value;
        return entity;
    }

    [NotNull]
    internal static T SetHasLimitedEffectPool<T>([NotNull] this T entity, Boolean value)
        where T : EffectDescription
    {
        entity.hasLimitedEffectPool = value;
        return entity;
    }

    [NotNull]
    internal static T SetHasSavingThrow<T>([NotNull] this T entity, Boolean value)
        where T : EffectDescription
    {
        entity.HasSavingThrow = value;
        return entity;
    }

    [NotNull]
    internal static T SetHasShoveRoll<T>([NotNull] this T entity, Boolean value)
        where T : EffectDescription
    {
        entity.hasShoveRoll = value;
        return entity;
    }

    [NotNull]
    internal static T SetHasVelocity<T>([NotNull] this T entity, Boolean value)
        where T : EffectDescription
    {
        entity.hasVelocity = value;
        return entity;
    }

    [NotNull]
    internal static T SetInviteOptionalAlly<T>([NotNull] this T entity, Boolean value)
        where T : EffectDescription
    {
        entity.inviteOptionalAlly = value;
        return entity;
    }

    [NotNull]
    internal static T SetItemSelectionType<T>([NotNull] this T entity, ActionDefinitions.ItemSelectionType value)
        where T : EffectDescription
    {
        entity.itemSelectionType = value;
        return entity;
    }

    [NotNull]
    internal static T SetOffsetImpactTimeBasedOnDistance<T>([NotNull] this T entity, Boolean value)
        where T : EffectDescription
    {
        entity.offsetImpactTimeBasedOnDistance = value;
        return entity;
    }

    [NotNull]
    internal static T SetOffsetImpactTimeBasedOnDistanceFactor<T>([NotNull] this T entity, Single value)
        where T : EffectDescription
    {
        entity.offsetImpactTimeBasedOnDistanceFactor = value;
        return entity;
    }

    [NotNull]
    internal static T SetOffsetImpactTimePerTarget<T>([NotNull] this T entity, Single value)
        where T : EffectDescription
    {
        entity.offsetImpactTimePerTarget = value;
        return entity;
    }

    [NotNull]
    internal static T SetPoolFilterDiceNumber<T>([NotNull] this T entity, Int32 value)
        where T : EffectDescription
    {
        entity.poolFilterDiceNumber = value;
        return entity;
    }

    [NotNull]
    internal static T SetPoolFilterDieType<T>([NotNull] this T entity, DieType value)
        where T : EffectDescription
    {
        entity.poolFilterDieType = value;
        return entity;
    }

    [NotNull]
    internal static T SetRangeParameter<T>([NotNull] this T entity, Int32 value)
        where T : EffectDescription
    {
        entity.rangeParameter = value;
        return entity;
    }

    [NotNull]
    internal static T SetRangeType<T>([NotNull] this T entity, RangeType value)
        where T : EffectDescription
    {
        entity.RangeType = value;
        return entity;
    }

    [NotNull]
    internal static T SetRecurrentEffect<T>([NotNull] this T entity, RecurrentEffect value)
        where T : EffectDescription
    {
        entity.recurrentEffect = value;
        return entity;
    }

    [NotNull]
    internal static T SetRequiresTargetProximity<T>([NotNull] this T entity, Boolean value)
        where T : EffectDescription
    {
        entity.requiresTargetProximity = value;
        return entity;
    }

    [NotNull]
    internal static T SetRequiresVisibilityForPosition<T>([NotNull] this T entity, Boolean value)
        where T : EffectDescription
    {
        entity.requiresVisibilityForPosition = value;
        return entity;
    }

    [NotNull]
    internal static T SetRetargetActionType<T>([NotNull] this T entity, ActionDefinitions.ActionType value)
        where T : EffectDescription
    {
        entity.retargetActionType = value;
        return entity;
    }

    [NotNull]
    internal static T SetRetargetAfterDeath<T>([NotNull] this T entity, Boolean value)
        where T : EffectDescription
    {
        entity.retargetAfterDeath = value;
        return entity;
    }

    [NotNull]
    internal static T SetSavingThrowAbility<T>([NotNull] this T entity, String value)
        where T : EffectDescription
    {
        entity.SavingThrowAbility = value;
        return entity;
    }

    [NotNull]
    internal static T SetSavingThrowDifficultyAbility<T>([NotNull] this T entity, String value)
        where T : EffectDescription
    {
        entity.SavingThrowDifficultyAbility = value;
        return entity;
    }

    [NotNull]
    internal static T SetSpeedParameter<T>([NotNull] this T entity, Single value)
        where T : EffectDescription
    {
        entity.speedParameter = value;
        return entity;
    }

    [NotNull]
    internal static T SetSpeedType<T>([NotNull] this T entity, SpeedType value)
        where T : EffectDescription
    {
        entity.speedType = value;
        return entity;
    }

    [NotNull]
    internal static T SetTargetConditionAsset<T>([NotNull] this T entity, ConditionDefinition value)
        where T : EffectDescription
    {
        entity.targetConditionAsset = value;
        return entity;
    }

    [NotNull]
    internal static T SetTargetConditionName<T>([NotNull] this T entity, String value)
        where T : EffectDescription
    {
        entity.targetConditionName = value;
        return entity;
    }

    [NotNull]
    internal static T SetTargetExcludeCaster<T>([NotNull] this T entity, Boolean value)
        where T : EffectDescription
    {
        entity.targetExcludeCaster = value;
        return entity;
    }

    [NotNull]
    internal static T SetTargetFilteringMethod<T>([NotNull] this T entity, TargetFilteringMethod value)
        where T : EffectDescription
    {
        entity.targetFilteringMethod = value;
        return entity;
    }

    [NotNull]
    internal static T SetTargetFilteringTag<T>([NotNull] this T entity, TargetFilteringTag value)
        where T : EffectDescription
    {
        entity.targetFilteringTag = value;
        return entity;
    }

    [NotNull]
    internal static T SetTargetParameter<T>([NotNull] this T entity, Int32 value)
        where T : EffectDescription
    {
        entity.targetParameter = value;
        return entity;
    }

    [NotNull]
    internal static T SetTargetParameter2<T>([NotNull] this T entity, Int32 value)
        where T : EffectDescription
    {
        entity.targetParameter2 = value;
        return entity;
    }

    [NotNull]
    internal static T SetTargetProximityDistance<T>([NotNull] this T entity, Int32 value)
        where T : EffectDescription
    {
        entity.targetProximityDistance = value;
        return entity;
    }

    [NotNull]
    internal static T SetTargetSide<T>([NotNull] this T entity, Side value)
        where T : EffectDescription
    {
        entity.TargetSide = value;
        return entity;
    }

    [NotNull]
    internal static T SetTargetType<T>([NotNull] this T entity, TargetType value)
        where T : EffectDescription
    {
        entity.TargetType = value;
        return entity;
    }
}
