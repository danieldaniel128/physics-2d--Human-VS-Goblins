using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

public class Health
{
    float _currnetHP;
    public float CurrnetHP { get => _currnetHP; set { _currnetHP = value; if (_currnetHP == 0) OnDeath?.Invoke(); } }
    public float MaxHP { get; set; } = 100;
    public float Damage { get; set; }
    public Image ImageHealthBar { get; set; }

    public event Action OnDeath;
    
    
    public Health(Image image, float damage = 0) 
    {
        CurrnetHP = MaxHP;
        Damage = damage;
        ImageHealthBar = image;
    }

    public void GotHurt(float dealedDamage) 
    {
        CurrnetHP -= dealedDamage;
        if(CurrnetHP<0)
            CurrnetHP = 0;
        UIManager.Instance.InvokeUpdateHealthBar(ImageHealthBar, CurrnetHP, MaxHP);
    }

    

}
