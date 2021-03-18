using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponImage : MonoBehaviour
{

    [SerializeField] private Image weaponImage;
    [SerializeField] private Sprite weapon1;
    [SerializeField] private Sprite weapon2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignImage(int weapon)
    {
        if (weapon == 1)
        {
            weaponImage.sprite = weapon1;
        }
        else
        {
            weaponImage.sprite = weapon2;
        }
        
    }
}
