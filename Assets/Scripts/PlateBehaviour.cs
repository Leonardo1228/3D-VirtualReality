using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateBehaviour : MonoBehaviour
{
    GrabManager grabManager;

    [SerializeField] GameObject holder;

    [SerializeField] List<GameObject> heldObjects = new List<GameObject>();

    void Start()
    {
        grabManager = GameObject.Find("GrabManager").GetComponent<GrabManager>();
    }

    void FixedUpdate()
    {
        foreach (var obj in heldObjects)
        {
            if (obj != null)
            {
                obj.transform.Rotate(Vector3.up * (10 * Time.deltaTime));
            }
        }
    }

    public void OnPointerClickXR()
    {
        // 👉 Agregar objeto al plato
        if (grabManager.heldItem != null)
        {
            GameObject newObject = grabManager.heldItem;

            heldObjects.Add(newObject);

            newObject.GetComponent<GrabObject>().Place(GetPositionForIndex(heldObjects.Count - 1));
        }
        else
        {
            // 👉 Sacar último objeto
            if (heldObjects.Count > 0)
            {
                GameObject lastObject = heldObjects[heldObjects.Count - 1];

                lastObject.GetComponent<GrabObject>().Grab();
                heldObjects.RemoveAt(heldObjects.Count - 1);
            }
        }
    }

    Vector3 GetPositionForIndex(int index)
    {
        float spacing = 0.2f;
        return holder.transform.position + new Vector3(index * spacing, 0, 0);
    }
}