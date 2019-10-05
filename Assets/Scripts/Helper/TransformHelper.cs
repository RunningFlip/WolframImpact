using UnityEngine;


public static class TransformHelper {

	public static Vector3 GetDirection(Vector3 _position, Vector3 _lastPosition) {
        return (_position - _lastPosition).normalized;
    }

    public static Quaternion LookToDirection(Vector3 _direction)
    {
        return Quaternion.LookRotation(_direction);
    }
}
