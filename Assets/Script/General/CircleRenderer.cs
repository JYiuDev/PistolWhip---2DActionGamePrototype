using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRenderer : MonoBehaviour
{
    //reference: https://gamedev.stackexchange.com/questions/126427/draw-circle-around-gameobject-to-indicate-radius
    [Range(0,50)] public int segments = 50;
    // [Range(0,5)] public float xradius = 5;
    // [Range(0,5)] public float yradius = 5;
    private float radius = 1;
    private LineRenderer line;
    private EnemyRanged enemyRanged;
    private bool toggled = false;

    void Awake()
    {
        line = gameObject.GetComponent<LineRenderer>();
        enemyRanged = gameObject.GetComponentInParent<EnemyRanged>();
        line.positionCount = (segments + 1);
        line.useWorldSpace = false;
    }
    void Start ()
    {
        
    }

    void Update()
    {
        if(toggled)
        {
            HandleColor();
            CreatePoints();
            line.enabled = true;
        } else
        {
            line.enabled = false;
        }
        
    }
    public void CreatePoints ()
    {
        float x;
        float y;
        radius = enemyRanged.getVisualRange();
        
        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin (Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos (Mathf.Deg2Rad * angle) * radius;

            line.SetPosition (i,new Vector3(x,y,0) );

            angle += (360f / segments);
        }
    }

    public void SetColor(Color c)
    {
        line.startColor = c;
        line.endColor = c;
    }

    public void SetRadius(float r)
    {
       radius = r;
       CreatePoints();
    }

    public void setToggle(bool b)
    {
        toggled = b;
    }

    private void HandleColor()
    {
        switch(enemyRanged.GetState())
        {
            case EnemyRanged.State.patrol:
                SetColor(Color.white);
            break;

            case EnemyRanged.State.aim:
                SetColor(Color.red);
            break;

            case EnemyRanged.State.alert:
                SetColor(Color.yellow);
            break;
        }
    }
}
