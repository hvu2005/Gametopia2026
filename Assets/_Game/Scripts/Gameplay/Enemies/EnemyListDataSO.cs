using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemies", menuName = "Gametopia/EnemyLis t", order = 3)]

public class EnemyListDataSO : ScriptableObject
{
    [SerializeField] private List<EnemyDataSO> enemies;
    
    public List<EnemyDataSO> Enemies { get => enemies; }
}