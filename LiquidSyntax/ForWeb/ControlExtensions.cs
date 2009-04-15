using System;
using System.Collections.Generic;
using System.Web.UI;

namespace LiquidSyntax.ForWeb {
    public static class ControlExtensions {
        public static List<T> FindAll<T>(this Control parent) {
            return parent.FindAll<T>(x => true);
        }

        public static List<T> FindAll<T>(this Control parent, Predicate<Control> predicate) {
            var controls = new List<T>();
            if (typeof(T).IsInstanceOfType(parent) && predicate(parent))
                controls.Add((T) (object) parent);
            foreach (Control child in parent.Controls)
                controls.AddRange(FindAll<T>(child, predicate));
            return controls;
        }
    }
}