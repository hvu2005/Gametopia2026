


using System.Collections.Generic;

public interface IOnAttack
{
    void ProcessOnAttack(BaseEntity attacker, BaseEntity defender, List<BaseEntity> allAliveEnemies = null);
}