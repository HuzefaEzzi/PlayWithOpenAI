using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayWithOpenAI
{
    internal class EmbeddingFile
    {
        public List<Section> Sections { get; set; }
    }

    class Section
    {
        public int Index { get; set; }
        public string Content { get; set; }
        public List<double> Vector { get; set; }
    }

    class SimilarSections
    {
        public Section Section { get; set; }
        public double Similarity { get; set; }
    }
}

/*
 * 
 * type Section = {
  content: string
  heading?: string
  slug?: string
}

type ProcessedMdx = {
  checksum: string
  meta: Meta
  sections: Section[]
}
 */