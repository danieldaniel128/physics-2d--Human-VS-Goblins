using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MinerData : EntetyData
{

    public int MinerNumber { get; set; }
    public int MiningPower { get; set; }
    public MinerData(float moveSpeed,int minerNumber, int miningPower) : base(moveSpeed)
    {
        MoveSpeed = moveSpeed;
        MinerNumber = minerNumber;
        MiningPower = miningPower;
    }

    public override void DoUpgrade()
    {
        MinerNumber +=1;
        MoveSpeed *= 1.1f;
    }
}
