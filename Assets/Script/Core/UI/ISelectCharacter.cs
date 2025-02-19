using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ISelectCharacter
{
    public Character TCharacter { get; protected set; }
    public void SetCharacter(Character c) => TCharacter = c;
}