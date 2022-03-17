using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine2 : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject currentLine;
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> fingerPositions;
    public GameObject From;
    public GameObject To;

    // Start is called before the first frame update
    void Start()
    {
        fingerPositions[0]=new Vector2(From.transform.position.x,From.transform.position.y);
        fingerPositions[1]=new Vector2(To.transform.position.x,To.transform.position.y);
        currentLine=Instantiate(linePrefab, Vector3.zero,Quaternion.identity);
        lineRenderer=currentLine.GetComponent<LineRenderer>();
        edgeCollider=currentLine.GetComponent<EdgeCollider2D>();
        edgeCollider.points=fingerPositions.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        // lineRenderer.SetPosition(lineRenderer.positionCount-1,fingerPositions[1]);
        // edgeCollider.points=fingerPositions.ToArray();
    }
}
