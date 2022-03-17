using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject From;
    public GameObject To;
    public int positionCountLine = 0;

    public LineController line;

    // Start is called before the first frame update
    private void Awake()
    {

    }
    private void FixedUpdate()
    {
    }
    void Start()
    {
        line = Instantiate(linePrefab, Vector3.zero, Quaternion.identity).GetComponent<LineController>();
        line.Init(From.transform.position, To.transform.position);

    }
    void Update()
    {
        line.MoveTo(From.transform.position, To.transform.position);


    }


}
