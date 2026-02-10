using UnityEngine;

[CreateAssetMenu(fileName = "BaseConstructionConfig", menuName = "Configs/BaseConstructionConfig")]
public class BaseConstructionConfig : ScriptableObject
{
    [field: SerializeField] public float TimeConstruction { get; private set; } = 10f;
    [field: SerializeField] public int StageCount { get; private set; } = 3;
    [field: SerializeField] public Base BasePrefab { get; private set; }
    public float TimePerStage => TimeConstruction / StageCount;
}
