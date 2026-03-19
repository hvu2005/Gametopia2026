



using UnityEngine;
[CreateAssetMenu(fileName = "EnemyData", menuName = "Gametopia/EnemyData", order = 2)]

public class EnemyDataSO : ScriptableObject
{
    [SerializeField] private Stats stats;
    [SerializeField] private Sprite sprite;

    public Stats Stats => stats;
    public Sprite Sprite => sprite;

    public int Direction;
}