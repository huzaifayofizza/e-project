using System;
using System.Collections.Generic;

namespace EProject.Models
{
    public partial class ExhibitionPosting
    {
        public int ExhibitionPostingId { get; set; }
        public int? ExhibitionPostingEvaluationId { get; set; }
        public int? ExhibitionPostingExhibitionId { get; set; }
        public string? ExhibitionPostingImage { get; set; }

        public virtual Evaluation? ExhibitionPostingEvaluation { get; set; }
        public virtual Exhibition? ExhibitionPostingExhibition { get; set; }
    }
}
