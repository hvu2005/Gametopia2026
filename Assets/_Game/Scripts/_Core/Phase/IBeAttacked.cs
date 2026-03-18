

using System.Collections.Generic;

public interface IBeAttacked
{
    void ProcessBeAttacked(BaseEntity attacker, BaseEntity defender, List<BaseEntity> allAliveEnemies = null);
}