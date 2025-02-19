using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "FilterRandom", menuName = "AL/Filter/Random", order = 1)]
public class FilterRandom : FilterData
{
    public int Amount = 1; //Number of random targets selected
    public override List<Unit> FilterTargets(AbilityData ability, Unit caster, List<Unit> source)
    {
        return GameUtilityTool.PickXRandom(source, new(), Amount);
    }
}
