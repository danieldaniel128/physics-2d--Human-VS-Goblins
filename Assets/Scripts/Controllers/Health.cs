using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

    public class Health
    {
        public float CurrnetHP { get; set; }
        public float MaxHP { get; set; } = 10;
        public float Damage { get; set; }
        public Image ImageHealthBar { get; set; }

        public event Action OnDeath;
        

        public Health(Image image, float damage) 
        {
            CurrnetHP = MaxHP;
            Damage = damage;
            ImageHealthBar = image;
        }


        public void DealDamage(float dealedDamage) 
        {
            CurrnetHP -= dealedDamage;
            if(CurrnetHP<0)
                CurrnetHP = 0;
            UIManager.Instance.InvokeUpdateHealthBar(ImageHealthBar, CurrnetHP, MaxHP);
        }

        

    }
