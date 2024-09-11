using UnityEngine;
using UnityEngine.UI;

public class ToolTipUI : MonoBehaviour
{
    public void HideTooltip() => gameObject.SetActive(false);

    // Use this for initialization
    void Start()
    {
        Canvas.ForceUpdateCanvases();
    }

    protected void PositionToolTip(int offset = 5)
    {
        var mousePosition = Input.mousePosition;

        var tooltipPosition = new Vector3(mousePosition.x, mousePosition.y - offset, transform.position.z);

        var tooltipHeight = GetComponent<RectTransform>().rect.height;
        var tooltipWidth = GetComponent<RectTransform>().rect.width;

        var screenWidth = Screen.width;
        var screenHeight = Screen.height;

        //avoid the tooltip going out of the screen to the right
        if (tooltipPosition.x + tooltipWidth / 2 > screenWidth)
            tooltipPosition.x = tooltipPosition.x - tooltipWidth / 4;

        // avoid the tooltip going out of the screen to the bottom
        if(tooltipPosition.y - tooltipHeight / 2 < 0)
            tooltipPosition.y = tooltipPosition.y + offset + tooltipHeight / 2;

        // avoid the tooltip going out of the screen to the top
        if (tooltipPosition.y + tooltipHeight / 2 > screenHeight)
            tooltipPosition.y = tooltipPosition.y - offset - tooltipHeight / 2;

        // avoid the tooltip going out of the screen to the left
        if (tooltipPosition.x - tooltipWidth / 4 < 0)
            tooltipPosition.x = tooltipPosition.x + tooltipWidth / 4;

        transform.position = tooltipPosition;

        gameObject.SetActive(true);
    }
}
