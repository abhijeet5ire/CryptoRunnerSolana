using Mapbox.Unity.Location;
using Mapbox.Utils;
using UnityEngine;

public class AnimationFlow : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private AbstractLocationProvider _locationProvider;
    private Vector2d _previousLocation;
    private bool _isMoving;

    private void Start()
    {
        _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider as AbstractLocationProvider;
        _locationProvider.OnLocationUpdated += UpdateLocation;
        _previousLocation = _locationProvider.CurrentLocation.LatitudeLongitude;
    }

    private void OnDestroy()
    {
        _locationProvider.OnLocationUpdated -= UpdateLocation;
    }
    private float _noMovementTimer = 0f;
    private const float _noMovementThreshold = 0.01f; // 2 seconds

    private void UpdateLocation(Location location)
    {
        Vector2d currentLocation = location.LatitudeLongitude;

        float distance = (float)Vector2d.Distance(currentLocation, _previousLocation);

        if (distance > 0.00000005f)
        {
            _isMoving = true;
            _previousLocation = currentLocation;
            _noMovementTimer = 0f; // Reset the timer since there's movement
        }
        else
        {
            _noMovementTimer += Time.deltaTime; // Increment the timer

            // Check if the no movement threshold has been reached
            if (_noMovementTimer >= _noMovementThreshold)
            {
                _isMoving = false;
            }
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (_animator != null)
        {
            if (_isMoving)
            {
                _animator.SetTrigger("Walk");
            }
            else
            {
                _animator.ResetTrigger("Walk"); // Reset trigger to avoid continuous triggering
            }
        }
    }
}