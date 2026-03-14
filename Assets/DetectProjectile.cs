using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/DetectProjectile")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "DetectProjectile", message: "[Agent] has spotted [Projectile]", category: "Events", id: "ec1ed8e6e50f5b756fac6915ca48d0df")]
public sealed partial class DetectProjectile : EventChannel<GameObject, GameObject> { }

