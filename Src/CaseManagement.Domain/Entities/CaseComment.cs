using CaseManagement.Domain.Common;

namespace CaseManagement.Domain.Entities
{
    public class CaseComment : BaseEntity
    {
        public Guid CaseId { get; private set; }
        public Guid AuthorUserId { get; private set; }
        public string Text { get; private set; }
        public bool isInternal { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }


        private CaseComment() { }



        internal CaseComment(Guid caseId, Guid authorUserId, string text, bool isInternal)
        {

            if (caseId == Guid.Empty)
                throw new ArgumentException("CaseId cannot be empty.", nameof(caseId));

            if (authorUserId == Guid.Empty)
                throw new ArgumentException("AuthorUserId cannot be empty.", nameof(authorUserId));

            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Text cannot be null or whitespace.", nameof(text));

            var trimmed = text.Trim();

            if (trimmed.Length > 2000)
                throw new ArgumentException("Kommentartekst må maks være 2000 tegn.", nameof(text));

            Id  = Guid.NewGuid();
            CaseId = caseId;
            AuthorUserId = authorUserId;
            Text = text.Trim();
            this.isInternal = isInternal;
            CreatedAtUtc = DateTime.UtcNow;
        }

    }

}
