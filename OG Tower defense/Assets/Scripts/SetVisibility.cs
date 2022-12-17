using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetVisibility : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    void Update()
    {
        if (LevelCounter.control.level == 3) {
            continueButton.SetActive(false);
        }
    }
}
