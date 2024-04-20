using UnityEngine;

public class TowerModel
{
    public GameObject Bullet;
    public Vector3 Position;
    public int Range;
    public int Damage;
    public float ReloadTime;
    public float LastTimeFired = 0;

    public TowerModel(Vector3 position, int range, int damage, float reloadTime)
    {
        Position = position;
        Range = range;
        Damage = damage;
        ReloadTime = reloadTime;
    }
}