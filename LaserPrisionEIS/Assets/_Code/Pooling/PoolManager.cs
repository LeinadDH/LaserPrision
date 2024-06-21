using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private LaserIndicator _laserPrefab;

    private void Awake()
    {
        SetupPool();
    }

    private void SetupPool()
    {
        ObjectPooler.SetupPool(_laserPrefab, 50, "Lasers");
    }
}
