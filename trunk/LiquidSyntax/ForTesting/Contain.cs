using System;
using NUnit.Framework.Constraints;

namespace LiquidSyntax.ForTesting {
    public class Contain {
        public static Constraint Text(string text) {
            return new TextConstraint(text);
        }
    }

    public class TextConstraint : Constraint {
        private readonly string expected;

        public TextConstraint(string expected) {
            this.expected = expected;
        }

        public override bool Matches(object actual) {
            this.actual = actual;
            return ((string) actual).Contains(expected);
        }

        public override void WriteDescriptionTo(MessageWriter writer) {
            writer.Write(String.Format("Expected {0} to contain {1}", actual, expected));
        }
    }
}