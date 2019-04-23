using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMaker : MonoBehaviour
{
    public GameObject linePrefab;

    private LineRenderer _currentLine;

    public float minimumSegmentLength = 0.5f;

    private Vector3 _previouseMousePosition;

    private List <GameObject> _undoBuffer;

    //public LineRenderer line;
    //public EdgeCollider2D edge;

    // Start is called before the first frame update
    void Start()
    {
        //initalizes the list
        _undoBuffer = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //Spawn prefab on click
        if(Input.GetMouseButtonDown(0))
        {
            _currentLine = Instantiate(linePrefab).GetComponent<LineRenderer>();
            _previouseMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        //Modify line if it exists.

        if(_currentLine != null)
        {
            Vector3 nowPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            nowPoint.z = 0;

            if(Vector3.Distance(nowPoint, _previouseMousePosition) > minimumSegmentLength)
            {
                 //Add a new point to line
            _currentLine.positionCount +=1;

            //sets the spots that we are drawing to
            _currentLine.SetPosition(_currentLine.positionCount -1, nowPoint);

            _previouseMousePosition = nowPoint;

            EdgeCollider2D currentEdge = _currentLine.gameObject.GetComponent<EdgeCollider2D>();
            Vector2[] edgePoints = new Vector2[_currentLine.positionCount];
            
            for(int i =0; i < edgePoints.Length; i++)
            {
                edgePoints [i] = _currentLine.GetPosition(i);
            
            }
            //Assignes collider points
            currentEdge.points = edgePoints; 



            }
        }
        // Ends line
        if(Input.GetMouseButtonUp(0))
        {

            if(_currentLine.positionCount < 2 )
            {
                Destroy(_currentLine.gameObject);
            }
            else
            {
                _undoBuffer.Add(_currentLine.gameObject);
            }
            _currentLine = null;

    
        }

            if(Input.GetKeyDown(KeyCode.U))
            {
                print("undo");
                Destroy(_undoBuffer [_undoBuffer.Count -1]);
                _undoBuffer.RemoveAt(_undoBuffer.Count -1);
            }

        /* 
        if(Input.GetMouseButtonDown(0))
        {
            //Get world position of mouse point
            Vector3 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            clickPoint.z = 0;

            //Add a new point to line
            line.positionCount +=1;

            //sets the spots that we are drawing 
            line.SetPosition(line.positionCount -1, clickPoint);

            // Makes edge colider the rite size of points
            Vector2[] edgePoints = new Vector2[line.positionCount];

            for(int i =0; i < edgePoints.Length; i++)
            {
                edgePoints [i] = line.GetPosition(i);
            
            }
            edge.points = edgePoints; 
        }
        */


    }
}
