using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public class Team
{
    public Character[] Characters;
    public void Clear()
    {
        for (int i = 0; i < Characters.Length; i++)
        {
            Characters[i] = null;
        }
    }
    public static Team Create(int Size)
    {
        var t = new Team(); 
        t.Characters = new Character[Size];
        return t;
    }
}