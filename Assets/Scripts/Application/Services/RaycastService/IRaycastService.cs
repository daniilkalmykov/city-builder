using UnityEngine;

namespace Application.Services.RaycastService
{
    public interface IRaycastService
    {
        RaycastHit? Raycast(Ray ray);
    }
}