using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeB : MonoBehaviour
{
    public DetectProjectile dp;
    public float dodgeSpeed;
    private bool dodging = false;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Dodge();
    }

    void Dodge(){
        if(dp.detected && !dodging){
            Vector2 direction = Vector2.Perpendicular(dp.GetDirection()*dodgeSpeed);

            StartCoroutine(Dash(direction));
        }
    }

    private IEnumerator Dash(Vector2 d){
        dodging = true;
        
        d = BestDirection(d);


        rb.velocity = d;

        yield return new WaitForSeconds(0.1f);
        rb.velocity = new Vector2(0f,0f);

        dodging = false;
        dp.detected = false;
    }

    private Vector2 BestDirection(Vector2 d){
        Vector2 projPos = dp.GetPosition();
        float x1 = transform.position.x + d.x;
        float y1 = transform.position.y + d.y;
        float x2 = transform.position.x - d.x;
        float y2 = transform.position.y - d.y;

        float dist1, dist2;

        dist1 = Mathf.Abs((projPos.x - x1)*(projPos.x - x1) + (projPos.y - y1)*(projPos.y - y1));
        dist2 = Mathf.Abs((projPos.x - x2)*(projPos.x - x2) + (projPos.y - y2)*(projPos.y - y2));

        if(dist1 > dist2) return d;
        else return -d;
    }
}
