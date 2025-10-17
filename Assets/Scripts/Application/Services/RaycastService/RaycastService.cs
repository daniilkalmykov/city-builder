using UnityEngine;

namespace Application.Services.RaycastService
{
    internal sealed class RaycastService : IRaycastService
    {
        public RaycastHit? Raycast(Ray ray)
        {
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
            {
                return hit;
            }

            return null;
        }
    }
}