using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoker
{
    public class SpellCollection
    {
        public IList<Spell> Spells { get; private set; }
        private Random rand;


        public SpellCollection()
        {
            this.rand = new Random();
            this.Spells = new List<Spell>();
            this.Init();
        }

        private void Init()
        {
            A("Cold Snap", "aaa");
            A("Ghost Walk", "aaz");
            A("Ice Wall", "aae");
            A("EMP", "zzz");
            A("Tornado", "zza");
            A("Alacrity", "zze");
            A("Sun Strike", "eee");
            A("Forge Spirit", "aee");
            A("Chaos Meteor", "zee");
            A("Deafening Blast", "aze");
        }

        private void A(string name, string pattern)
        {
            this.Spells.Add(SpellFactory.Create(name, pattern));
        }

        public Spell GetRandom()
        {
            int pick = rand.Next(this.Spells.Count);
            return (this.Spells[pick]);
        }
    }
}
