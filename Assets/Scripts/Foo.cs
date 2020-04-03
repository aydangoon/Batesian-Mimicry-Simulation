using UnityEngine;
using UnityEditor;

public abstract class Foo : MonoBehaviour
{

    //State
    protected DNA dna;
    protected int generation;

    protected Vector3 dir; //invariant: normalized

    protected int jumpWait;
    protected int childWait;

    protected bool onGround;

    protected float foodNeed;
    protected float sexNeed;

    //Cached
    protected Material mat;
    protected Rigidbody body;
    protected Collider col;

    public Transform cachedTransform { get; private set; }


    /**
     * Instantiate and Initialize
     */

    private void Awake()
    {

        mat = transform.GetComponent<MeshRenderer>().material;
        body = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        cachedTransform = GetComponent<Transform>();

    }

    public virtual void Initialize(DNA dna, int generation = 0)
    {
        Vector2 xy = Random.insideUnitCircle * Settings.ENVIRONMENT_RANGE;

        cachedTransform.position = new Vector3(xy.x, 3, xy.y);
        cachedTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        
        dir = new Vector3(Random.Range(0, 1f), 0, Random.Range(0, 1f)).normalized;

        jumpWait = 0;
        childWait = Settings.CHILD_WAIT;

        onGround = true;

        foodNeed = 0;
        sexNeed = 0;

        this.dna = dna;
        this.generation = generation + 1;

        if (dna.vividness != -1)
        {
            mat.color = new Color(1, 0.2f + (0.8f * (1f - dna.vividness)), 0.3f * (1 - dna.vividness));
        }
    }

    /**
     * Shared Behaviors
     */

    public void Kill()
    {
        if (CompareTag("herbifoo"))
        {
            PopulationHandler.herbifoos.Remove(this);
        }
        else
        {
            PopulationHandler.carnifoos.Remove(this);
        }
        Destroy(gameObject);
    }

    protected void Jump()
    {
        Vector3 v = new Vector3(dir.x, 0, dir.z);
        v *= dna.speed * Settings.MAX_SPEED;
        v.y = Settings.JUMP_HEIGHT;

        body.velocity = v;

        jumpWait = Settings.JUMP_WAIT;
       
    }

    protected void UpdateNeeds()
    {

        if (foodNeed >= Settings.HUNGER_LIMIT && !Settings.INVINCIBILITY)
        {
            Kill();
        }
        else if (foodNeed < 0)
        {
            foodNeed = 0;
        }
        else if (sexNeed < 0)
        {
            sexNeed = 0;
        }

        foodNeed += (dna.speed * Settings.SPEED_COST) + (dna.perception * Settings.PERCEP_COST)
                  + (dna.mimicry * Settings.MIMICRY_COST) + (dna.poison * Settings.POISON_COST) + (8 * dna.sexDrive) + 1;
        sexNeed += IsChild ? 0 : (2 * dna.sexDrive);
    }

    private void OnDrawGizmos()
    {
        Handles.Label(cachedTransform.position, StateString);
    }

    protected void FixedUpdate()
    {

        if (jumpWait > 0)
        {
            jumpWait--;

            if (jumpWait == 0)
            {
                SetOnGround();
            }
        }

        if (childWait > 0)
        {
            childWait--;

            if (childWait == 0)
            {
                cachedTransform.localScale = Vector3.one;
            }
        }
    }

    protected void PadDir()
    {
        float ang = 0;
        float raycastLen = col.bounds.extents.y + dna.speed * Settings.MAX_SPEED;

        for (int i = 0; i < Settings.TURNS; i++)
        {
            Vector3 newDir = Quaternion.Euler(0, ang, 0) * dir;

            bool hitTarget = Physics.SphereCast(cachedTransform.position, 0.5f, newDir, out _, raycastLen, Settings.ENVI_MASK);

            if (!hitTarget)
            {
                Debug.DrawRay(cachedTransform.position + col.bounds.extents.y * newDir, newDir * raycastLen, Color.green, 0.5f);

                dir = newDir;
                break;
            }

            Debug.DrawRay(cachedTransform.position + col.bounds.extents.y * newDir, newDir * raycastLen, Color.red, 0.5f);

            ang += Mathf.Pow(-1, i) * (i + 1) * Settings.TURN_AMNT;

        }

    }

    protected void SetOnGround()
    {
        onGround = Physics.Raycast(cachedTransform.position, Vector3.down, col.bounds.extents.y + 0.1f, Settings.ENVI_MASK);
    }

    public float[] GetStatistics()
    {
        float[] ret = new float[5];
        ret[0] = dna.speed;
        ret[1] = dna.perception;
        ret[2] = dna.sexDrive;
        ret[3] = dna.poison;
        ret[4] = dna.mimicry;
        return ret;
    }

    /**
     *Abstract Behaviors
     */

    protected abstract void PointDirToTarget();
    protected abstract void HaveChild();

    /**
     * Values Derived from State
     */

    protected bool InRange(float e)
    {
        return e <= dna.perception * Settings.MAX_PERCEP;
    }

    protected bool IsChild => childWait > 0;

    protected string StatisticsString => "gen: " + generation + "speed: " + dna.speed + "\n" +
                            "perception: " + dna.perception + "\n" +
                            "sex drive: " + dna.sexDrive + "\n" +
                            "poison: " + dna.poison + "\n" +
                            "mimicry: " + dna.mimicry;

    protected string StateString => "food need: " + foodNeed + "\n" + "sex need: " + sexNeed;

}
