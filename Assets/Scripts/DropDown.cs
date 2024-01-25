using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    public string selectedOption;

    public void GetDropValue()
    {
        int pickedEntryIndex = dropdown.value;
        selectedOption = "" + dropdown.options[pickedEntryIndex].text;
    }
}
