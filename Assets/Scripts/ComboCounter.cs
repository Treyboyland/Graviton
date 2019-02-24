using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textBox;

    // Start is called before the first frame update
    void Start()
    {
        BaseGameManager.Manager.OnPlayerComboUpdated.AddListener(SetText);
        HideBox();        
    }

    void HideBox()
    {
        textBox.text = "";
    }

    void SetText(int combo)
    {

        textBox.text = combo >= 2 ?  "" + combo +  " Combo!" : "";
    }
}
