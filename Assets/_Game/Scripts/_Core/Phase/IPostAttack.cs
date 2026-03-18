


using System.Collections.Generic;

public interface IPostAttack
{
    void ProcessPostAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null);
}