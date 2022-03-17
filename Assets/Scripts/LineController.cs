using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour

{
    public LayerMask lmColl;
    private RaycastHit2D _hitWallDestroy;
    private RaycastHit2D _hitWallCreate;
    public bool resetBaseList = false;
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public DrawLine drawLine;
    bool isInitialized = false;
    public List<Vector2> baseList;
    public List<Vector2> edgeCollisionList;
    [SerializeField] private List<Vector2> betweenList;
    public Vector2 point1;
    public Vector2 point2;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        drawLine = FindObjectOfType<DrawLine>();
    }
    private void FixedUpdate()
    {
        CreateLinecastDestroy();
        CreateLinecastCreate();
    }



    public void Init(Vector3 from, Vector3 to)
    {
        lineRenderer.SetPosition(0, from);
        lineRenderer.SetPosition(1, to);
        isInitialized = true;
        baseList.Add(from);
        foreach (Vector2 x in betweenList)
        {
            baseList.Add(x);
        }
        baseList.Add(to);


    }
    public void UpdateBaseList(Vector3 from, Vector3 to)
    {
        if (resetBaseList == true)
        {
            baseList.Clear();
            baseList.Add(from);
            foreach (Vector2 x in betweenList)
            {
                baseList.Add(x);
            }
            baseList.Add(to);
            resetBaseList = false;

        }


    }
    public void UpdateLineRenderer()
    {
        for (int i = 0; i < baseList.Count - 1; i++)
        {
            lineRenderer.SetPosition(i, baseList[i]);
        }
    }
    public void UpdateEdgeCollisionList()
    {
        edgeCollisionList.Clear();
        edgeCollisionList.Add(baseList[baseList.Count - 2]);
        edgeCollisionList.Add(baseList[baseList.Count - 1]);
        edgeCollider.points = edgeCollisionList.ToArray();

    }

    void Update()
    {
        UpdateEdgeCollisionList();
        if (_hitWallCreate.collider == null || _hitWallCreate.collider.CompareTag("line"))
        {
            return;
        }
        else
        {
            Debug.Log(_hitWallCreate.collider);
        }

    }
    public void MoveTo(Vector3 from, Vector3 target)
    {
        resetBaseList = true;
        if (!isInitialized) return;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, target);
        UpdateBaseList(from, target);
        UpdateLineRenderer();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("wall")) return;
        if (_hitWallCreate.collider == null)
        {
            return;
        }
        else
        {


            Debug.Log(_hitWallCreate.collider.gameObject);


            Vector2 newFingerPos = new Vector2(other.transform.position.x, other.transform.position.y);
            betweenList.Add(newFingerPos);

            lineRenderer.positionCount++;
        }


    }
    public void CreateLinecastDestroy()
    {
        if (baseList.Count >= 3)
        {


            point1 = baseList[baseList.Count - 3];
            point2 = baseList[baseList.Count - 1];

            _hitWallDestroy = Physics2D.Linecast(point1, point2, lmColl);

#if UNITY_EDITOR
            Debug.DrawLine(point1, point2, Color.red);
#endif
        }

    }

    public void CreateLinecastCreate()
    {



        point1 = baseList[baseList.Count - 2];
        point2 = baseList[baseList.Count - 1];

        _hitWallCreate = Physics2D.Linecast(point1, point2, lmColl);
#if UNITY_EDITOR
        Debug.DrawLine(point1, point2, Color.yellow);
#endif


    }






}
