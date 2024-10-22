using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Team
{
    public Character[]  Characters = new Character[6];
    public void Clear()
    {
        for (int i = 0; i < Characters.Length; i++)
        {
            Characters[i] = null;
        }
    }
}