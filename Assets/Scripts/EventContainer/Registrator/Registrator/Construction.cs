using CameraMain;
using UnityEngine;
using UnityEngine.AI;

namespace Registrator
{
    public enum TypeObject
    {
        Player,
        Enemy,
        CameraPoint,
        Other
    }
    public struct Construction : IConstruction
    {
        public int Hash { get; set; }
        public bool IsDead { get; set; }
        public Transform Transform;
        public int[] ChildrenHash { get; set; }
        public TypeObject TypeObject { get; set; }
        public Camera CameraComponent;
        public NavMeshAgent NavMeshAgent;
        public CameraPointObject CameraPointObject;

    }
}

