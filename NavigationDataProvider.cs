using UnityEngine;
using Google.Maps;
using Google.Maps.Event;
using Google.Maps.Unity;

// NOTE: Requires the Google Maps SDK for Unity.

public class NavigationDataProvider : MonoBehaviour
{
    [Header("API Settings")]
    public string mapsApiKey;

    [Header("HUD Reference")]
    public HUDController hudController;

    private MapsService mapsService;

    void Start()
    {
        // Initialize the MapsService with your API key
        mapsService = new MapsService(mapsApiKey);
        
        // Optionally, configure other map settings here
    }

    /// <summary>
    /// Requests a route from currentLocation to destination.
    /// </summary>
    public void UpdateRoute(LatLng currentLocation, LatLng destination)
    {
        mapsService.GetRoute(currentLocation, destination, OnRouteReceived);
    }

    private void OnRouteReceived(Route route, MapError error)
    {
        if (error != MapError.None)
        {
            Debug.LogError($"Route request failed: {error}");
            return;
        }

        // Example: take the first step of the first leg
        var firstLeg = route.Legs[0];
        var firstStep = firstLeg.Steps[0];

        // Convert GPS coordinate to a Unity world position (requires your own transform logic)
        Vector3 worldPos = LatLngToWorldPosition(firstStep.StartLocation);

        float eta = (float)route.Duration.TotalSeconds;
        float distance = (float)firstStep.Distance.Meters;

        // Update the HUD
        hudController.SetNavigationData(worldPos, eta, distance);
    }

    /// <summary>
    /// Converts a LatLng to a Unity world position. 
    /// This requires your AR anchoring or geospatial conversion.
    /// </summary>
    private Vector3 LatLngToWorldPosition(LatLng gpsLocation)
    {
        // Placeholder implementation:
        // In AR, you would place an AR anchor or use ARCore Extensions
        // to convert geospatial coordinates to Unity world coordinates.
        return new Vector3(); 
    }
}
