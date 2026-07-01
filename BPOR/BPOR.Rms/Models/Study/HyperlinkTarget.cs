namespace BPOR.Rms.Models.Study;

public enum HyperlinkTarget
{
    /// <summary>
    /// Opens the linked document in the same frame as it was clicked
    /// </summary>
    Self,
    /// <summary>
    /// Opens the linked document in the full body of the window
    /// </summary>
    Top,
    /// <summary>
    /// Opens the linked document in a new window or tab 
    /// </summary>
    Blank,
    /// <summary>
    /// Opens the linked document in the parent frame
    /// </summary>
    Parent
}