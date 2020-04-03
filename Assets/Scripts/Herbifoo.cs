using UnityEngine;

public class Herbifoo : Foo
{

    //State
    private Nom nomTarget;


    //Cached
    public float vividness { get; private set; }
    public float poison { get; private set; }

    /**
     * Initializer
     */

    public override void Initialize(DNA dna, int generation = 0)
    {
        base.Initialize(dna, generation);

        vividness = dna.vividness;
        poison = dna.poison;
    }

    /*
     * Novel Behaviors
     */

    private new void FixedUpdate()
    {

        base.FixedUpdate();

        if (onGround && CanEatNomTarget)
        {
            nomTarget.getConsumed();
            foodNeed -= 1f;
        }
        else if (onGround && jumpWait <= 0)
        {

            //Sex and Food
            if (!IsChild && sexNeed > foodNeed)
            {
                HaveChild();
            }

            //Look for Predators
            Vector3 flee = FleeDir();
            if (flee.magnitude > 0)
            {
                dir = flee;
            }
            else
            {

                //find Nom to eat
                nomTarget = NearestNom();
                if (nomTarget != null)
                {
                    PointDirToTarget();
                }
            }
            
            //Stay in bounds
            PadDir();

            body.transform.forward = dir;
            Jump();

            UpdateNeeds();
        }

    }

    private Vector3 FleeDir()
    {
        Vector3 v = Vector3.zero;

        foreach (Carnifoo c in PopulationHandler.carnifoos)
        {
            float dist = Vector3.Distance(cachedTransform.position, c.cachedTransform.position);

            if (InRange(dist))
            {
                v += (cachedTransform.position - c.cachedTransform.position);
            }
        }

        v.y = 0;
        return v.normalized;
    }

    private Nom NearestNom()
    {
        Nom closest = null;
        float d = -1;

        foreach (Nom n in PopulationHandler.noms)
        {
            float dist = Vector3.Distance(cachedTransform.position, n.cachedTransform.position);

            if (!n.respawning && InRange(dist))
            {
                if (closest == null)
                {
                    closest = n;
                    d = dist;
                }
                else if (dist < d)
                {
                    closest = n;
                    d = dist;
                }
            }
        }
        return closest;
    }

    /*
     * Inherited Behaviors Overrides
     */

    protected override void PointDirToTarget()
    {
        Vector3 v = (nomTarget.cachedTransform.position - cachedTransform.position).normalized;
        v.y = 0;
        dir = v;
    }

    protected override void HaveChild()
    {

        sexNeed = 0;

        Herbifoo child = Instantiate(this);
        child.Initialize(DNA.Mutate(dna), generation);

        PopulationHandler.herbifoos.Add(child);

    }

    /**
     * Values Derived from State
     */

    bool CanEatNomTarget => nomTarget != null && !nomTarget.respawning &&
        Vector3.Distance(nomTarget.cachedTransform.position, cachedTransform.position) <= Settings.EAT_RANGE;

}
