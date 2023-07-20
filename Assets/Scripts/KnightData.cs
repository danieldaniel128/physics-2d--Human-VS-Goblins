using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class KnightData : EntetyData
{
    //public Health Health { get; set; }
    public float KnightDamage;
    public float KnightHealth;
    public float KnightSpeed;

    public KnightData(float moveSpeed,float hp, float damage) : base(moveSpeed)
    {
        KnightSpeed = moveSpeed;
        KnightHealth = hp;
        KnightDamage = damage;
    }

    public override void DoUpgrade()
    {
        KnightDamage *= 1.2f;
        KnightHealth *= 1.2f;
        KnightSpeed *= 1.1f;
    }
}
