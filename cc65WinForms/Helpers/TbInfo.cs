using FastColoredTextBoxNS;

namespace cc65WinForms
{
    /// <summary>
    /// Holds auxiliary information related to a text box editor instance.
    /// Currently this class exposes the <see cref="AutocompleteMenu"/> used to
    /// show completion suggestions for a given text box.
    /// </summary>
    /// <remarks>
    /// This type is a lightweight container and does not take ownership of the
    /// <see cref="AutocompleteMenu"/> lifetime — the caller that creates or
    /// assigns <c>popupMenu</c> is responsible for initializing and disposing it.
    /// </remarks>
    public class TbInfo
    {
        /// <summary>
        /// The auto complete popup menu associated with the text box.
        /// May be <c>null</c> if completion is not enabled or not yet initialized.
        /// </summary>
        public AutocompleteMenu popupMenu;
    }
}
