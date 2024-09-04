using UnityEngine;

public class ToolTipUI : MonoBehaviour
{
    public void HideTooltip() => gameObject.SetActive(false);

    protected void PositionToolTip(int offset = 5)
    {
        var mousePosition = Input.mousePosition;

        var tooltipPosition = new Vector3(mousePosition.x, mousePosition.y - offset, transform.position.z);

        transform.position = tooltipPosition;

        gameObject.SetActive(true);
    }
}
