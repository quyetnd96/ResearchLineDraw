using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
    
{
    public bool resetBaseList=false;
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public DrawLine drawLine;
    bool isInitialized = false;
    public List<Vector2> baseList;
    public List<Vector2> edgeCollisionList;
    [SerializeField] private List<Vector2> betweenList; 
    
    void Awake()
    {
        lineRenderer=GetComponent<LineRenderer>();
        edgeCollider=GetComponent<EdgeCollider2D>();
        drawLine=FindObjectOfType<DrawLine>();
    }

   

    public void Init(Vector3 from, Vector3 to)
    {
        lineRenderer.SetPosition(0,from);
        lineRenderer.SetPosition(1,to);
        isInitialized = true;
        baseList.Add(from);
        foreach (Vector2 x in betweenList)
        {
            baseList.Add(x);
        }
        baseList.Add(to);
        
        
    }
    public void UpdateBaseList(Vector3 from,Vector3 to)
    {
        if(resetBaseList==true)
        {
            baseList.Clear();
            baseList.Add(from);
            foreach (Vector2 x in betweenList)
            {
                baseList.Add(x);
            }
            baseList.Add(to);
            resetBaseList=false; 

        }
        
        
    }
    public void UpdateLineRenderer()
    {
        for (int i=0;i< baseList.Count-1;i++)
        {
            lineRenderer.SetPosition(i,baseList[i]);
        }
    }
    public void UpdateEdgeCollisionList()
    {   
        edgeCollisionList.Clear();
        edgeCollisionList.Add(baseList[baseList.Count-2]);
        edgeCollisionList.Add(baseList[baseList.Count-1]);
        edgeCollider.points=edgeCollisionList.ToArray();

    }
    
    void Update()
    {
        UpdateEdgeCollisionList();
    }
     public void MoveTo(Vector3 from,Vector3 target)
    {
        resetBaseList=true;
        if(!isInitialized) return;
        lineRenderer.SetPosition(lineRenderer.positionCount-1,target);
        UpdateBaseList(from,target);
        UpdateLineRenderer();
    }
    private void OnCollisionEnter2D(Collision2D other) {
      
        Debug.Log("here");
            Vector2 newFingerPos= new Vector2 (other.transform.position.x,other.transform.position.y);
            betweenList.Add(newFingerPos);
            
            lineRenderer.positionCount++;
      
    }
   


}
