using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class LevelEditorSettings : MonoBehaviour
{
    [SerializeField]
    SymmetricWallPlacer wallPlacer;

    [SerializeField]
    TextMeshProUGUI textBox;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSettings();
        wallPlacer.OnSettingChanged.AddListener((unused) => UpdateSettings());
    }

    void UpdateSettings()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<align=\"center\">Settings</align>\r\n\r\n");

        switch (wallPlacer.Symmetry)
        {
            case SymmetricWallPlacer.WallSymmetry.HORIZONTAL:
                sb.Append("Horizontal Symmetry Enabled\r\n");
                break;
            case SymmetricWallPlacer.WallSymmetry.VERTICAL:
                sb.Append("Vertical Symmetry Enabled\r\n");
                break;
            case SymmetricWallPlacer.WallSymmetry.ROTATIONAL:
                sb.Append("Rotational Symmetry Enabled\r\n");
                break;
            case SymmetricWallPlacer.WallSymmetry.NONE:
            default:
            sb.Append("\r\n");
                break;
        }

        textBox.text = sb.ToString();

    }
}
