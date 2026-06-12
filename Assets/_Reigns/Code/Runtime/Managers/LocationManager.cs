using UnityEngine;

public class LocationManager : MonoBehaviour
{
    public static LocationManager Instance;

    public LocationID CurrentLocation { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        ChangeLocation(LocationID.GuildHall);
    }

    public void ChangeLocation(LocationID newLocation)
    {
        CurrentLocation = newLocation;

        Debug.Log($"Location Changed: {newLocation}");

        EventManager.Instance.CheckLocationEvents(newLocation);
    }
    
}
