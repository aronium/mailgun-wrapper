namespace MailgunWrapper.Models
{
    /// <summary>
    /// Extends <see cref="MailgunPagedResponse{T}"/> using <see cref="MailgunEvent"/> type.
    /// </summary>
    public class MailgunEventCollection : MailgunPagedResponse<MailgunEvent>
    {
    }
}
