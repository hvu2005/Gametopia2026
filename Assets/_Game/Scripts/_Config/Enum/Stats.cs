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
    public float lucky;
    public float increaseDamage;

    public float counterAttackChance;
    public float dodgeChance;
    public float speed;

    public float suckBlood;

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
            stunChance = a.stunChance + b.stunChance,
            lucky = a.lucky + b.lucky,
            speed = a.speed + b.speed,
            increaseDamage = a.increaseDamage + b.increaseDamage,
            counterAttackChance = a.counterAttackChance + b.counterAttackChance,
            dodgeChance = a.dodgeChance + b.dodgeChance,
            suckBlood = a.suckBlood + b.suckBlood
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
            stunChance = a.stunChance - b.stunChance,
            lucky = a.lucky - b.lucky,
            speed = a.speed - b.speed,
            increaseDamage = a.increaseDamage - b.increaseDamage,
            counterAttackChance = a.counterAttackChance - b.counterAttackChance,
            dodgeChance = a.dodgeChance - b.dodgeChance,
            suckBlood = a.suckBlood - b.suckBlood
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
            stunChance = a.stunChance * multiplier,
            lucky = a.lucky * multiplier,
            speed = a.speed * multiplier,
            increaseDamage = a.increaseDamage * multiplier,
            counterAttackChance = a.counterAttackChance * multiplier,
            dodgeChance = a.dodgeChance * multiplier,
            suckBlood = a.suckBlood * multiplier
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
            stunChance = a.stunChance / divisor,
            lucky = a.lucky / divisor,
            speed = a.speed / divisor,
            increaseDamage = a.increaseDamage / divisor,
            counterAttackChance = a.counterAttackChance / divisor,
            dodgeChance = a.dodgeChance / divisor,
            suckBlood = a.suckBlood / divisor

        };
    }
}