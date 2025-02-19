using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public abstract class FilterData : ScriptableObject
{
    public abstract List<Unit> FilterTargets(AbilityData ability, Unit caster, List<Unit> source);


}
