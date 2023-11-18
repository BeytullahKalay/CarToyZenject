using System.Collections.Generic;
using UnityEngine;

public class CarTrailController : MonoBehaviour
{
   [SerializeField] private List<TrailRenderer> openCloseTrails = new List<TrailRenderer>(); 

   public void OpenTrails()
   {
      foreach (var trail in openCloseTrails)
      {
         trail.emitting = true;
      }
   }

   public void CloseTrails()
   {
      foreach (var trail in openCloseTrails)
      {
         trail.emitting = false;
      }
   }
   
}
