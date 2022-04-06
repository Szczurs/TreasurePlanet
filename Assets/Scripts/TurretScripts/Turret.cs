using UnityEditor;
using UnityEngine;


public class Turret : MonoBehaviour
{
    public Gun gun;
    public MountPoint[] mountPoints;
    public Transform target;

    public bool directTargetVisibility;

    public float distanceToTarget;

    public float maxRange = 100;

    bool isDirectlyVisible()
    {
        RaycastHit hit;

        if(Vector3.Distance(transform.position, target.position) < maxRange)
        {
            if(Physics.Raycast(transform.position, (target.position - transform.position), out hit,maxRange))
            {
                if(hit.transform == target.transform)
                {
                    //enemy can see the player
                    return true;
                }
            }
        }
        return false;
    }

    void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (!target) return;

        var dashLineSize = 2f;

        

        foreach (var mountPoint in mountPoints)
        {
            mountPoint.range = maxRange;
            var hardpoint = mountPoint.transform;
            var from = Quaternion.AngleAxis(-mountPoint.angleLimit / 2, hardpoint.up) * hardpoint.forward;
            var projection = Vector3.ProjectOnPlane(target.position - hardpoint.position, hardpoint.up);
            if(isDirectlyVisible()){
                // projection line
                Handles.color = Color.white;
                Handles.DrawDottedLine(target.position, hardpoint.position + projection, dashLineSize);

                // do not draw target indicator when out of angle
                if (Vector3.Angle(hardpoint.forward, projection) > mountPoint.angleLimit / 2) return;

                // target line
                Handles.color = Color.red;
                Handles.DrawLine(hardpoint.position, hardpoint.position + projection);

                
                // range line
                Handles.color = Color.green;
                Handles.DrawWireArc(hardpoint.position, hardpoint.up, from, mountPoint.angleLimit, projection.magnitude);
                Handles.DrawSolidDisc(hardpoint.position + projection, hardpoint.up, .5f);
            }

        }
        #endif
    }

    void Update()
    {
        directTargetVisibility = isDirectlyVisible();
        distanceToTarget = Vector3.Distance(target.position,transform.position);

        // do nothing when no target
        if (!target) return;
            
        // aim target
        var aimed = true;
        foreach (var mountPoint in mountPoints)
        {
            if (!mountPoint.Aim(target.position))
            {
                aimed = false;
            }
        }

        // shoot when aimed
        if (aimed && isDirectlyVisible())
        {
            gun.Fire();
        }
    }
}