using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace LiquidSyntax.ForTesting {
    public class Be : Is {
        public static Constraint EquivalentTo(params object[] items) {
            return Is.EquivalentTo(items.ToList());
        }
    }
}