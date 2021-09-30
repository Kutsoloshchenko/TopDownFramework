using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TopDownFramework;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{

    private void OnSceneGUI()
    {
        var fov = (FieldOfView)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.forward, Vector3.right, 360, fov.radiusOfView);

        var viewAngle1 = DirectionFromAngle(fov.transform.eulerAngles.z*-1, -fov.angleOfView/2);
        var viewAngle2 = DirectionFromAngle(fov.transform.eulerAngles.z*-1, fov.angleOfView / 2);

        Handles.color = Color.yellow;

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle1 * fov.radiusOfView);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle2 * fov.radiusOfView);

        if (fov.canSeeTarget)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.target.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
