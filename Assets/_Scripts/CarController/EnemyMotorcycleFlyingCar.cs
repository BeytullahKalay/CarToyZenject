using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyMotorcycleFlyingCar : FlyingCar
{
    [SerializeField] private float minCollisionForce;
    [SerializeField] private List<GameObject> particleFx;
    

    private VehicleDestroyAnimationManager _vehicleDestroyAnimationManager;

    [Inject]
    private void Constructor(CarSettings.EnemyMotorcycleSetting carSettings,
        VehicleDestroyAnimationManager vehicleDestroyAnimationManager)
    {
        var settings = carSettings.settings;
        Multiplier = settings.MoveSettings.Multiplier;
        MoveForce = settings.MoveSettings.MoveForce;
        TurnTorque = settings.MoveSettings.TurnForce;
        FlowingDistance = settings.FlowingSettings.FlowingDistance;
        FlowingFrequency = settings.FlowingSettings.FlowingFrequency;
        _vehicleDestroyAnimationManager = vehicleDestroyAnimationManager;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var collisionForce = other.impulse / Time.fixedDeltaTime;

            if (collisionForce.magnitude >= minCollisionForce)
            {
                _vehicleDestroyAnimationManager.PlayExplosionAnimation(transform, 1, 2);
                CarInput.DisableInput();

                foreach (var particle in particleFx)
                {
                    Destroy(particle);
                }
            }
        }
    }
}