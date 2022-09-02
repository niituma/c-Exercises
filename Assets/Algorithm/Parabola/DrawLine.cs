using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private int positionCount;
    private Camera mainCamera;
    CatchLine[] targets;
    [SerializeField]
    List<Vector2> _edgelist;
    EdgeCollider2D _collder;

    void Start()
    {
        targets = FindObjectsOfType<CatchLine>();
        lineRenderer = GetComponent<LineRenderer>();
        _collder = GetComponent<EdgeCollider2D>();
        lineRenderer.useWorldSpace = false;
        positionCount = 0;
        mainCamera = Camera.main;
    }

    void Update()
    {
        transform.position = mainCamera.transform.position + mainCamera.transform.forward * 10;
        transform.rotation = mainCamera.transform.rotation;

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 10.0f;

            pos = mainCamera.ScreenToWorldPoint(pos);

            pos = transform.InverseTransformPoint(pos);

            positionCount++;
            lineRenderer.positionCount = positionCount;
            lineRenderer.SetPosition(positionCount - 1, pos);
            _edgelist.Add(pos);
            _collder.SetPoints(_edgelist);
        }

        if (Input.GetMouseButtonUp(0))
        {
            var startPos = lineRenderer.GetPosition(0);
            lineRenderer.SetPosition(positionCount - 1, startPos);
            _edgelist.Add(startPos);
            _collder.SetPoints(_edgelist);
            foreach (var target in targets)
            {
                target.LineSarch();
            }
        }

        //ƒŠƒZƒbƒg‚·‚é
        if (!(Input.GetMouseButton(0)))
        {
            foreach (var target in targets)
            {
                target.CountReset();
            }
            _edgelist.Clear();
            positionCount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("a");
    }

}
