using System.Collections.Generic;

[System.Serializable]
public struct Stats
{
    public float hp;
    public float physicalDamage;
    public float def;
    public float criticalChance;
    public float criticalDamage;
    public float lifeSteal;
    public float fortune;
    public float magicDamage;
    public float poisonous;
    public float stunChance;

    /// <summary>
    /// Chuyển Stats thành Dictionary để dùng trong Modifier Pipeline.
    /// Key = StatType enum, Value = giá trị tương ứng.
    /// </summary>
    public Dictionary<StatType, float> ToDictionary() => new()
    {
        { StatType.HP,              hp },
        { StatType.PhysicalDamage,  physicalDamage },
        { StatType.Defense,         def },
        { StatType.CriticalChance,  criticalChance },
        { StatType.CriticalDamage,  criticalDamage },
        { StatType.LifeSteal,       lifeSteal },
        { StatType.Fortune,         fortune },
        { StatType.MagicDamage,     magicDamage },
        { StatType.Poisonous,       poisonous },
        { StatType.StunChance,      stunChance },
    };

    /// <summary>
    /// Tạo Stats từ Dictionary sau khi Modifier Pipeline đã tính xong.
    /// Key nào không có trong dict sẽ nhận giá trị mặc định 0.
    /// </summary>
    public static Stats FromDictionary(Dictionary<StatType, float> dict)
    {
        dict.TryGetValue(StatType.HP,             out float hp);
        dict.TryGetValue(StatType.PhysicalDamage, out float phys);
        dict.TryGetValue(StatType.Defense,         out float def);
        dict.TryGetValue(StatType.CriticalChance,  out float cc);
        dict.TryGetValue(StatType.CriticalDamage,  out float cd);
        dict.TryGetValue(StatType.LifeSteal,       out float ls);
        dict.TryGetValue(StatType.Fortune,         out float fort);
        dict.TryGetValue(StatType.MagicDamage,     out float mag);
        dict.TryGetValue(StatType.Poisonous,       out float poi);
        dict.TryGetValue(StatType.StunChance,      out float stun);

        return new Stats
        {
            hp             = hp,
            physicalDamage = phys,
            def            = def,
            criticalChance = cc,
            criticalDamage = cd,
            lifeSteal      = ls,
            fortune        = fort,
            magicDamage    = mag,
            poisonous      = poi,
            stunChance     = stun
        };
    }

    public static Stats operator +(Stats a, Stats b)
    {
        return new Stats
        {
            hp = a.hp + b.hp,
            physicalDamage = a.physicalDamage + b.physicalDamage,
            def = a.def + b.def,
            criticalChance = a.criticalChance + b.criticalChance,
            criticalDamage = a.criticalDamage + b.criticalDamage,
            lifeSteal = a.lifeSteal + b.lifeSteal,
            fortune = a.fortune + b.fortune,
            magicDamage = a.magicDamage + b.magicDamage,
            poisonous = a.poisonous + b.poisonous,
            stunChance = a.stunChance + b.stunChance
        };
    }

    public static Stats operator -(Stats a, Stats b)
    {
        return new Stats
        {
            hp = a.hp - b.hp,
            physicalDamage = a.physicalDamage - b.physicalDamage,
            def = a.def - b.def,
            criticalChance = a.criticalChance - b.criticalChance,
            criticalDamage = a.criticalDamage - b.criticalDamage,
            lifeSteal = a.lifeSteal - b.lifeSteal,
            fortune = a.fortune - b.fortune,
            magicDamage = a.magicDamage - b.magicDamage,
            poisonous = a.poisonous - b.poisonous,
            stunChance = a.stunChance - b.stunChance
        };
    }

    public static Stats operator *(Stats a, float multiplier)
    {
        return new Stats
        {
            hp = a.hp * multiplier,
            physicalDamage = a.physicalDamage * multiplier,
            def = a.def * multiplier,
            criticalChance = a.criticalChance * multiplier,
            criticalDamage = a.criticalDamage * multiplier,
            lifeSteal = a.lifeSteal * multiplier,
            fortune = a.fortune * multiplier,
            magicDamage = a.magicDamage * multiplier,
            poisonous = a.poisonous * multiplier,
            stunChance = a.stunChance * multiplier
        };
    }

    public static Stats operator /(Stats a, float divisor)
    {
        return new Stats
        {
            hp = a.hp / divisor,
            physicalDamage = a.physicalDamage / divisor,
            def = a.def / divisor,
            criticalChance = a.criticalChance / divisor,
            criticalDamage = a.criticalDamage / divisor,
            lifeSteal = a.lifeSteal / divisor,
            fortune = a.fortune / divisor,
            magicDamage = a.magicDamage / divisor,
            poisonous = a.poisonous / divisor,
            stunChance = a.stunChance / divisor
        };
    }

}