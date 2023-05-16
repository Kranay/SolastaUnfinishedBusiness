﻿using System.Collections.Generic;
using JetBrains.Annotations;

namespace SolastaUnfinishedBusiness.CustomInterfaces;

public interface IModifyAdditionalDamage
{
    [UsedImplicitly]
    public FeatureDefinitionAdditionalDamage ModifyAdditionalDamage(
        GameLocationBattleManager gameLocationBattleManager,
        FeatureDefinitionAdditionalDamage additionalDamage,
        GameLocationCharacter attacker,
        GameLocationCharacter defender,
        ActionModifier attackModifier,
        RulesetAttackMode attackMode,
        bool rangedAttack,
        RuleDefinitions.AdvantageType advantageType,
        List<EffectForm> actualEffectForms,
        RulesetEffect rulesetEffect,
        bool criticalHit,
        bool firstTarget);
}
