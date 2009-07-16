using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;

namespace LiquidSyntax.ForWeb {
    public static class ControlExtensions {
        public static IEnumerable<T> FindAll<T>(this Control parent) {
            return parent.FindAllWhere<T>(x => true);
        }

        public static IEnumerable<T> FindAllWhere<T>(this Control parent, Predicate<Control> predicate) {
            var controls = new List<T>();
            if (typeof(T).IsInstanceOfType(parent) && predicate(parent))
                controls.Add((T) (object) parent);
            foreach (Control child in parent.Controls)
                controls.AddRange(FindAllWhere<T>(child, predicate));
            return controls;
        }

        /// <summary>
        /// Finds all descendants of control of a specific type, but stops recursing when condition is met.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="stopCondition"></param>
        /// <returns></returns>
        public static IEnumerable<T> FindAllUntil<T>(this Control parent, Predicate<Control> stopCondition) {
            var result = new List<T>();
            if (!stopCondition(parent)) {
                if (typeof(T).IsInstanceOfType(parent)) {
                    result.Add((T) (object) parent);
                }
                foreach (Control child in parent.Controls) {
                    result.AddRange(FindAllUntil<T>(child, stopCondition));
                }
            }
            return result;
        }

        public static Control FindDescendantWithId(this Control parent, string childId) {
            if (parent.ID == childId)
                return parent;
            foreach (Control child in parent.Controls) {
                var result = FindDescendantWithId(child, childId);
                if (result != null)
                    return result;
            }
            return null;
        }

        public static string GetHtml(this Control control) {
            var stringWriter = new StringWriter();
            var htmlTextWriter = new HtmlTextWriter(stringWriter);
            control.RenderControl(htmlTextWriter);
            return stringWriter.ToString();
        }
    }
}