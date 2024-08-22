using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipUI : MonoBehaviour
{
    public void HideTooltip() => gameObject.SetActive(false);

    protected void PositionToolTip()
    {
        var mousePosition = Input.mousePosition;

        var tooltipPosition = new Vector3(mousePosition.x, mousePosition.y - 5, transform.position.z);

        transform.position = tooltipPosition;

        gameObject.SetActive(true);
    }
}
