using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] Image CastleHealthBarImage;
    [SerializeField] List<Image> _enemiesHealthBarImages;

    public static UIManager Instance;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    public void EnemyHealthBarAdded(Image enemyHealthBarImage)
    {
        _enemiesHealthBarImages.Add(enemyHealthBarImage);
    }


}
