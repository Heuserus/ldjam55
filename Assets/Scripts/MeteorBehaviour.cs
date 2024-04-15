using UnityEditor.UI;
using UnityEngine;

public class MeteorBehaviour : MonoBehaviour
{

    public LineRenderer lineRenderer;

    private int segments = 64;

    public float fallSpeed = 0.5f;

    private float fallSpeedOffset;

    private float scale;


    private int radius = 1;

    private GameMaster gameMaster;

    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();

        transform.rotation = Quaternion.Euler(UnityEngine.Random.Range(0,360), UnityEngine.Random.Range(0,360), UnityEngine.Random.Range(0,360));

        scale = UnityEngine.Random.Range(1,5);

        transform.localScale = new Vector3(scale, scale, scale);

        lineRenderer = gameObject.GetComponent<LineRenderer>();
        fallSpeedOffset = UnityEngine.Random.Range(-10,10) * 0.01f;
        DrawIndicator();
    }

    void FixedUpdate(){
        if(!(gameMaster.state == GameMaster.GameState.phase1||gameMaster.state == GameMaster.GameState.phase2)){
            return;
        } 
        transform.position = new Vector3(transform.position.x, transform.position.y - fallSpeed + fallSpeedOffset, transform.position.z);
    }

    void Update()
    {
        if (transform.position.y < -2){
            Destroy(lineRenderer);
            Destroy(gameObject);
        }
    }

    private void DrawIndicator(){
        lineRenderer.positionCount = segments + 1;

        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            float x = transform.position.x + Mathf.Sin (Mathf.Deg2Rad * angle) * (radius * scale);
            float z = transform.position.z + Mathf.Cos (Mathf.Deg2Rad * angle) * (radius * scale);

            Vector3 pos = new Vector3(x, -1f, z);
            lineRenderer.SetPosition(i, pos);

            angle += (360f / segments);
        }
    }

}
