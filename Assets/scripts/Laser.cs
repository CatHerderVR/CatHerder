using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    public Transform StartPoint;
	public string LaserDirection = "X";
    public bool Use2D = false;
    public LayerMask LaserMask;
	public bool LaserOn = true;

    public bool UseUVPan = false;

    public float EndFlareOffset = 0.0f;

    public LensFlare SourceFlare;
	public LensFlare EndFlare;

	public bool AddSourceLight = true;
    public bool AddEndLight = true;

    public Color LaserColor = new Color(1,1,1,0.5f);

    public float StartWidth = 0.1f;
    public float EndWidth = 0.1f;
    public float LaserDist = 20.0f;
    public float TexScrollX = -0.1f;
    public float TexScrollY = 0.1f;
    public Vector2 UVTexScale = new Vector2( 4f, .4f );

    private int SectionDetail = 2;       
    private LineRenderer lineRenderer;
    private Ray ray = new Ray( new Vector3( 0, 0, 0 ), new Vector3( 0, 1, 0 ) );
    private Vector3 EndPos;
	private RaycastHit hit;
	private RaycastHit2D hit2D;
	private GameObject SourceLight;
	private GameObject EndLight;
	private float ViewAngle;
	private Vector3 LaserDir;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if( lineRenderer.material == null )
            lineRenderer.GetComponent<Renderer>().material = new Material( Shader.Find( "VolumetricLineStripAdditive" ) );

        lineRenderer.castShadows = false;
        lineRenderer.receiveShadows = false;

        lineRenderer.SetVertexCount( SectionDetail );
        lineRenderer = GetComponent<LineRenderer>();

        // Make a lights
        if( AddSourceLight )
        {
            StartPoint.gameObject.AddComponent<Light>();
            StartPoint.GetComponent< Light > ().intensity = 1.5f;
            StartPoint.GetComponent< Light > ().range = .5f;
        }

        if( AddEndLight )
        {
            if( EndFlare )
            {
                EndFlare.gameObject.AddComponent<Light>();
                EndFlare.GetComponent<Light>().intensity = 1.5f;
                EndFlare.GetComponent<Light>().range = .5f;
            }
            else { Debug.Log( "To use End Light, please assign an End Flare" ); }
        }


        if( LaserDirection == "x" || LaserDirection == "y" || LaserDirection == "z" || LaserDirection == "X" || LaserDirection == "Y" || LaserDirection == "Z" )
        {
        }
        else
        {
            Debug.Log( "Laser Direction can only be X, Y or Z" );
        }


    }//end start


    /////////////////////////////////////
    void Update()
    {
        if( LaserDirection == "x" || LaserDirection == "X" )
        {
            LaserDirection = "X";
            LaserDir = -StartPoint.right;
        }
        else if( LaserDirection == "y" || LaserDirection == "Y" )
        {
            LaserDirection = "Y";
            LaserDir = StartPoint.up;
        }
        else if( LaserDirection == "z" || LaserDirection == "Z" )
        {
            LaserDirection = "Z";
            LaserDir = StartPoint.forward;
        }
        else
        {
            LaserDir = StartPoint.forward;
        }

        var CamDistSource = Vector3.Distance( StartPoint.position, Camera.main.transform.position );
        var CamDistEnd = Vector3.Distance( EndPos, Camera.main.transform.position );
        ViewAngle = Vector3.Angle( LaserDir, Camera.main.transform.forward );

        if( LaserOn )
        {
            lineRenderer.enabled = true;
            lineRenderer.SetWidth( StartWidth, EndWidth );
            lineRenderer.material.color = LaserColor;

            //Flare Control
            if( SourceFlare )
            {
                SourceFlare.color = LaserColor;
                SourceFlare.transform.position = StartPoint.position;

                if( ViewAngle > 155 && CamDistSource < 20 && CamDistSource > 0 )
                {
                    SourceFlare.brightness = Mathf.Lerp( SourceFlare.brightness, 20.0f, .001f );
                }
                else
                {
                    SourceFlare.brightness = Mathf.Lerp( SourceFlare.brightness, 0.1f, .05f );
                }
            }

            if( EndFlare )
            {
                EndFlare.color = LaserColor;

                if( CamDistEnd > 20 )
                {
                    EndFlare.brightness = Mathf.Lerp( EndFlare.brightness, 0.0f, .1f );
                }
                else
                {
                    EndFlare.brightness = Mathf.Lerp( EndFlare.brightness, 5.0f, .1f );
                }
            }// end flare        

            //Light Control
            if( AddSourceLight )
                StartPoint.GetComponent< Light > ().color = LaserColor;

            if( AddEndLight )
            {
                if( EndFlare )
                {
                    EndFlare.GetComponent< Light > ().color = LaserColor;
                }
            }




            /////////////////////Ray Hit
            if( Use2D )
            {
                hit2D = Physics2D.Raycast( StartPoint.position, LaserDir, LaserDist, LaserMask );

                var ray2 = new Ray2D( StartPoint.position, LaserDir );
                var dist2D = Vector3.Distance( StartPoint.position, hit2D.point );
                if( hit2D )
                {
                    EndPos = hit2D.point;

                    if( EndFlare )
                    {
                        EndFlare.enabled = true;

                        if( AddEndLight )
                        {
                            if( EndFlare )
                            {
                                EndFlare.GetComponent< Light > ().enabled = true;
                            }
                        }

                        if( EndFlareOffset > 0 )
                            EndFlare.transform.position = hit2D.point + hit2D.normal * EndFlareOffset;
                        else
                            EndFlare.transform.position = EndPos;
                    }
                }
                else
                {
                    if( EndFlare )
                        EndFlare.enabled = false;

                    if( AddEndLight )
                    {
                        if( EndFlare )
                        {
                            EndFlare.GetComponent< Light > ().enabled = false;
                        }
                    }

                    EndPos = ray2.GetPoint( LaserDist );
                }
            }
            ///Else 3D Ray
            else
            {
                ray = new Ray( StartPoint.position, LaserDir );
                if( Physics.Raycast( ray, out hit, LaserDist, LaserMask ) )
                {
                    EndPos = hit.point - LaserDir * 0.05f;

                    if( EndFlare )
                    {
                        EndFlare.enabled = true;

                        if( AddEndLight )
                        {
                            if( EndFlare )
                            {
                                EndFlare.GetComponent< Light > ().enabled = true;
                            }
                        }

                        if( EndFlareOffset > 0 )
                            EndFlare.transform.position = hit.point + hit.normal * EndFlareOffset;
                        else
                            EndFlare.transform.position = EndPos;
                    }
                }
                else
                {
                    if( EndFlare )
                        EndFlare.enabled = false;

                    if( AddEndLight )
                    {
                        if( EndFlare )
                        {
                            EndFlare.GetComponent< Light > ().enabled = false;
                        }
                    }

                    EndPos = ray.GetPoint( LaserDist );
                }
            }//end Ray       


            //Debug.DrawLine (StartPoint.position, EndPos, Color.red);

            //Find Distance
            var dist = Vector3.Distance( StartPoint.position, EndPos );

            //Line Render Positions
            lineRenderer.SetPosition( 0, StartPoint.position );
            lineRenderer.SetPosition( 1, EndPos );

            //Texture Scroller
            if( UseUVPan )
            {
                //lineRenderer.material.SetTextureScale("_Mask", Vector2(dist/4, .1));
                lineRenderer.material.SetTextureScale( "_Mask", new Vector2( dist / UVTexScale.x, UVTexScale.y ) );
                lineRenderer.material.SetTextureOffset( "_Mask", new Vector2( TexScrollX * Time.time, TexScrollY * Time.time ) );
            }

        }
        else
        {
            lineRenderer.enabled = false;

            if( SourceFlare )
                SourceFlare.enabled = false;

            if( EndFlare )
                EndFlare.enabled = false;

            if( AddSourceLight )
                StartPoint.GetComponent< Light > ().enabled = false;

            if( AddEndLight )
                if( EndFlare )
                {
                    EndFlare.GetComponent< Light > ().enabled = false;
                }
        }//end Laser On   

    }//end Update


    /////Icon
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon( transform.position, "LaserIcon.psd", true );
    }
}
