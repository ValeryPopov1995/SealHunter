using UnityEngine;
using UnityEngine.UI;

public class WebLinkButton : MonoBehaviour
{
    public string WebLink;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(GoWeb);
    }

    void GoWeb()
    {
        Application.OpenURL(WebLink);
    }
}
