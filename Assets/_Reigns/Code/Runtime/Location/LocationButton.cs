using UnityEngine;

public class LocationButton : MonoBehaviour
{
    [SerializeField]
    private GameObject screenToOpen;

    [SerializeField]
    private GameObject screenToClose;

    [SerializeField]
    private LocationID locationID;

    public void OpenLocation()
    {
        if (screenToClose != null)
            screenToClose.SetActive(false);

        if (screenToOpen != null)
            screenToOpen.SetActive(true);

        LocationManager.Instance.ChangeLocation(locationID);

    }
}