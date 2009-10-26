using System.Web.UI.WebControls;

namespace LiquidSyntax.ForWeb {
    public static class EventExtensions {
        public static GridViewRow Row(this GridViewCommandEventArgs eventArgs) {
            return (GridViewRow) eventArgs.GetPropertyValue("Row");
        }

        public static int RowIndex(this GridViewCommandEventArgs eventArgs) {
            return eventArgs.Row().RowIndex;
        }
    }
}