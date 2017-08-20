namespace MailgunWrapper.Models
{
    public class MailgunAttachment
    {
        /// <summary>
        /// Initializes new instance of MailgunAttachment class.
        /// </summary>
        public MailgunAttachment() { }

        /// <summary>
        /// Initializes new instance of MailgunAttachment class with specified attachment type and file name.
        /// </summary>
        /// <param name="attachmentType">Indicates whether file is attached inline or as a regular attachment. Value can be "inline" or "attachment".</param>
        /// <param name="fileName">File name to attach.</param>
        public MailgunAttachment(string attachmentType, string fileName)
        {
            AttachmentType = attachmentType;
            FileName = fileName;
        }

        /// <summary>
        /// Gets or sets attachment type. Can be "inline" or "attachment". Default value is "inline".
        /// </summary>
        public string AttachmentType { get; set; } = "inline";

        /// <summary>
        /// Gets or sets file name to attach.
        /// </summary>
        public string FileName { get; set; }
    }
}
