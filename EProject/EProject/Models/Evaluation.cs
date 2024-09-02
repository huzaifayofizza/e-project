using System;
using System.Collections.Generic;

namespace EProject.Models
{
    public partial class Evaluation
    {
        public Evaluation()
        {
            ExhibitionPostings = new HashSet<ExhibitionPosting>();
        }

        public int EvaluationId { get; set; }
        public int? EvaluationPostId { get; set; }
        public int? EvaluationStatus { get; set; }
        public string? EvaluationRemarks { get; set; }

        public virtual Posting? EvaluationPost { get; set; }
        public virtual ICollection<ExhibitionPosting> ExhibitionPostings { get; set; }
    }
}
