using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RadialTrigger : MonoBehaviour
{

    public float radius = 1.0f;

    public Transform pointTF;


    private void OnDrawGizmos()
    {

         
        float dist = Vector2.Distance(transform.position, pointTF.position);
        bool isInside = dist <= radius;

        Handles.color = isInside ? Color.green : Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, radius);


    }

}
