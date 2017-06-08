using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using Rock;
using Rock.Attribute;
using Rock.Data;
using Rock.Model;
using Rock.Workflow;

namespace org.newpointe.ExtraActions
{
    /// <summary>
    /// InjectHtml
    /// </summary>
    [ActionCategory( "Extra Actions" )]
    [Description( "Shows HTML in the WorkflowEntry block." )]
    [Export( typeof( ActionComponent ) )]
    [ExportMetadata( "ComponentName", "Workflow Entry Show HTML" )]

    [CodeEditorField( "HTML", "The HTML to show. <span class='tip tip-lava'></span>", Rock.Web.UI.Controls.CodeEditorMode.Html, Rock.Web.UI.Controls.CodeEditorTheme.Rock, 200, true, "Boop", "", 0 )]
    [BooleanField( "Hide Status Message", "Whether or not to hide the built-in status message.", false, "", 1 )]
    class ShowHtml : ActionComponent
    {
        /// <summary>
        /// Executes the specified workflow.
        /// </summary>
        /// <param name="rockContext">The rock context.</param>
        /// <param name="action">The action.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="errorMessages">The error messages.</param>
        /// <returns></returns>
        public override bool Execute( RockContext rockContext, WorkflowAction action, object entity, out List<string> errorMessages )
        {
            errorMessages = new List<string>();

            HttpContext httpContext = HttpContext.Current;
            if ( httpContext != null )
            {
                System.Web.UI.Page page = httpContext.Handler as System.Web.UI.Page;
                if ( page != null )
                {
                    var workflowEntryBlock = page.ControlsOfTypeRecursive<Rock.Web.UI.RockBlock>().Where( x => x.BlockName == "Workflow Entry" ).FirstOrDefault();
                    if ( workflowEntryBlock != null )
                    {
                        workflowEntryBlock.PreRender += ( sender, args ) =>
                        {
                            var notificationBox = workflowEntryBlock.ControlsOfTypeRecursive<Rock.Web.UI.Controls.NotificationBox>().FirstOrDefault();
                            if ( notificationBox != null )
                            {
                                notificationBox.Visible = notificationBox.Visible && !GetAttributeValue( action, "HideStatusMessage" ).AsBoolean();
                                var index = notificationBox.Parent.Controls.IndexOf( notificationBox );
                                if ( index > -1 )
                                    notificationBox.Parent.Controls.AddAt( index + 1, new System.Web.UI.LiteralControl( GetAttributeValue( action, "HTML" ).ResolveMergeFields( GetMergeFields( action ) ) ) );
                            }
                        };
                    }
                }
            }
            return true;
        }
    }
}
