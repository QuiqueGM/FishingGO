//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class SteamVR_TestThrow : MonoBehaviour
{
    public GameObject prefab;
    public Rigidbody attachPoint;
    public bool controllerIsAboveWater = false;

    SteamVR_TrackedObject trackedObj;
    FixedJoint joint;

    public virtual void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    public virtual void FixedUpdate()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);

        if (joint == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            //prefab.GetComponent<Fish>().fishState = Fish.FishState.Thrown;
            
            var go = GameObject.Instantiate(prefab);
            go.transform.position = prefab.transform.position;
            go.transform.rotation = prefab.transform.rotation;
            go.name = prefab.name;
            //go.GetComponent<Fish>().fishState = Fish.FishState.Flying;

            //prefab.gameObject.transform.Find("Model").GetComponent<MeshRenderer>().enabled = false;

            joint = go.AddComponent<FixedJoint>();
            joint.connectedBody = attachPoint;
        }
        else if (joint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Destroy(prefab);
            var go = joint.gameObject;
            var rigidbody = go.GetComponent<Rigidbody>();
            Object.DestroyImmediate(joint);
            joint = null;
            Object.Destroy(go, 15.0f);

            // We should probably apply the offset between trackedObj.transform.position
            // and device.transform.pos to insert into the physics sim at the correct
            // location, however, we would then want to predict ahead the visual representation
            // by the same amount we are predicting our render poses.

            var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
            if (origin != null)
            {
                rigidbody.velocity = origin.TransformVector(device.velocity);
                rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
            }
            else
            {
                rigidbody.velocity = device.velocity;
                rigidbody.angularVelocity = device.angularVelocity;
            }

            rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
            DestroyBullet();
        }
    }

    public virtual void DestroyBullet()
    {
    }
}
