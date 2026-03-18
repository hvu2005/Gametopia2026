

using System.Collections.Generic;

public interface IPreAttack
{
    void ProcessPreAttack(BaseEntity attacker, BaseEntity defender, List<BaseEntity> allAliveEnemies = null);
}