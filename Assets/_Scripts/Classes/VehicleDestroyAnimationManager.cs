using UnityEngine;

public class VehicleDestroyAnimationManager
{
    public void PlayExplosionAnimation(Transform searchTransform, float explosionForce = 1, float explosionRadius = 1)
    {
        foreach (Transform child in searchTransform)
        {
            if (child.TryGetComponent<MeshRenderer>(out var renderer) &&
                !child.TryGetComponent<Rigidbody>(out var rigidbody))
            {
                var rb = renderer.gameObject.AddComponent<Rigidbody>();
                rb.mass = 1;
                rb.angularDrag = 0;
                rb.drag = .5f;

                var collider = renderer.gameObject.AddComponent<MeshCollider>();
                collider.convex = true;

                rb.AddExplosionForce(explosionForce, -renderer.transform.up, explosionRadius);

                child.SetParent(null);
            }

            PlayExplosionAnimation(child, explosionForce, explosionRadius);
        }
    }
}