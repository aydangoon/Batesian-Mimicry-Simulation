using UnityEngine;

public class Carnifoo : Foo
{

    //State
    private Herbifoo fooTarget;

    /*
     * Novel Behaviors
     */

    private new void FixedUpdate()
    {

        base.FixedUpdate();

        if (onGround && CanEatFooTarget)
        {
            TryToEatTarget();
        }
        else if (onGround && jumpWait <= 0)
        {
            //Sex and Food
            if (!IsChild && sexNeed > foodNeed)
            {
                HaveChild();
            }

            //find Herbifoo to eat
            fooTarget = FindNearestHerbifoo();

            if (fooTarget != null)
            {
                PointDirToTarget();
            }

            //Stay in bounds
            PadDir();


            body.transform.forward = dir;
            Jump();

            UpdateNeeds();
        }

    }

    private Herbifoo FindNearestHerbifoo()
    {
        Herbifoo closest = null;
        float d = -1;

        foreach (Herbifoo h in PopulationHandler.herbifoos)
        {
            float dist = Vector3.Distance(cachedTransform.position, h.cachedTransform.position);

            if (InRange(dist) && !ScaredOfPoison(h.vividness))
            {
                if (closest == null)
                {
                    closest = h;
                    d = dist;
                }
                else if (dist < d)
                {
                    closest = h;
                    d = dist;
                }
            }
        }
        return closest;
    }

    private bool ScaredOfPoison(float p)
    {
        return Random.Range(0, 1f) <= p;
    }

    private void TryToEatTarget()
    {
        if (Random.Range(0, 1f) >= fooTarget.poison)
        {
            fooTarget.Kill();
            foodNeed -= 75f;
        }
        else
        {
            Kill();
        }
    }

    /*
     * Inherited Behaviors Overrides
     */

    override protected void PointDirToTarget()
    {
        Vector3 v = (fooTarget.cachedTransform.position - cachedTransform.position).normalized;
        v.y = 0;
        dir = v;
    }

    protected override void HaveChild()
    {

        sexNeed = 0;

        Carnifoo child = Instantiate(this);
        child.Initialize(DNA.Mutate(dna), generation);

        PopulationHandler.carnifoos.Add(child);
    }

    /**
     *  Values Derived from State
     */

    private bool CanEatFooTarget => fooTarget != null &&
        Vector3.Distance(fooTarget.cachedTransform.position, cachedTransform.position) <= Settings.EAT_RANGE;


}
