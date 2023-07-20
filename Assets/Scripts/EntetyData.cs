using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class EntetyData
{
    public float MoveSpeed { get; set; }
    public abstract void DoUpgrade();
    public EntetyData(float moveSpeed)
    {
        MoveSpeed = moveSpeed;
    }
}
