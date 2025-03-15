using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigeonKingGames.BehaviourTree 
{ 
    public class Node
    {
        public enum Statue { Success, Failure, Running }
        public readonly string name;
        public readonly List<Node> nodes = new List<Node>();
        protected int currentChild;

        public Node(string name)
        {

           this.name = name;
        }
    }
}
