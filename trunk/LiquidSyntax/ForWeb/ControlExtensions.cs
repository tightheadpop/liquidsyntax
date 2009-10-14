using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace LiquidSyntax.ForWeb {
    public static class ControlExtensions {
        public static IEnumerable<T> FindAll<T>(this Control parent) {
            return parent.FindAll<T>(x => true);
        }

        public static IEnumerable<T> FindAll<T>(this Control parent, Predicate<T> where) {
            var controls = new List<T>();
            foreach (Control child in parent.Controls) {
                controls.AddRange(FindAllRecursive(child, where));
            }
            return controls;
        }

        private static IEnumerable<T> FindAllRecursive<T>(Control control, Predicate<T> where) {
            var controls = new List<T>();
            if (typeof(T).IsInstanceOfType(control) && where((T) (object) control))
                controls.Add((T) (object) control);
            foreach (Control child in control.Controls)
                controls.AddRange(FindAllRecursive(child, where));
            return controls;
        }

        /// <summary>
        /// Finds all descendants of control of a specific type, but stops recursing each control branch when condition is met. Note
        /// that recursion may continue on another branch of the control tree.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="stopCondition"></param>
        /// <returns></returns>
        public static IEnumerable<T> FindAllUntil<T>(this Control parent, Predicate<Control> stopCondition) {
            var result = new List<T>();
            foreach (Control child in parent.Controls) {
                result.AddRange(FindAllRecursiveUntil<T>(child, stopCondition));
            }
            return result;
        }

        private static IEnumerable<T> FindAllRecursiveUntil<T>(Control control, Predicate<Control> stopCondition) {
            var result = new List<T>();
            if (!stopCondition(control)) {
                if (typeof(T).IsInstanceOfType(control)) {
                    result.Add((T) (object) control);
                }
                foreach (Control child in control.Controls) {
                    result.AddRange(FindAllRecursiveUntil<T>(child, stopCondition));
                }
            }
            return result;
        }

        public static T FindAncestor<T>(this Control descendant) where T : Control {
            var parent = descendant.Parent;
            while (parent != null) {
                if (parent is T) return (T) parent;
                parent = parent.Parent;
            }
            return null;
        }

        public static T FindFirst<T>(this Control parent) {
            return parent.FindAll<T>().FirstOrDefault();
        }

        public static T FindFirst<T>(this Control parent, Predicate<T> predicate) {
            return parent.FindAll(predicate).FirstOrDefault();
        }

        public static string GetHtml(this Control control) {
            var stringWriter = new StringWriter();
            var htmlTextWriter = new HtmlTextWriter(stringWriter);
            control.RenderControl(htmlTextWriter);
            return stringWriter.ToString();
        }
    }
}