using PizzeriaBusinessLogic.OfficePackage.HelperEnums;
using System.Collections.Generic;

namespace PizzeriaBusinessLogic.OfficePackage.HelperModels
{
    public class PdfRowParameters
    {
        public List<string> Texts { get; set; }

        public string Style { get; set; }

        public PdfParagraphAlignmentType ParagraphAlignment { get; set; }
    }
}
