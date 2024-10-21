using Syncfusion.Pdf;
using Syncfusion.Pdf.Security;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;

namespace DigitalSignatureSample {
    internal class Program {
        static void Main(string[] args) {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Your License Key");

            PdfDocument document = new PdfDocument();
            PdfPageBase page=document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            FileStream certificateStream = new FileStream("../../../Data/PDF.pfx", FileMode.Open, FileAccess.Read);
            PdfCertificate pdfCert = new PdfCertificate(certificateStream, "syncfusion");
            PdfSignature signature=new PdfSignature(document, page, pdfCert, "Signature");
            signature.Bounds = new RectangleF(new PointF(0, 0), new SizeF(100, 100));
            FileStream imageStream = new FileStream("../../../Data/signature.png", FileMode.Open, FileAccess.Read);
            PdfBitmap signatureImage=new PdfBitmap(imageStream);
            signature.ContactInfo= "johndoe@owned.us";
            signature.LocationInfo= "Honolulu, Hawaii";
            signature.Reason= "I am author of this document.";
            signature.Appearance.Normal.Graphics.DrawImage(signatureImage, new RectangleF(0, 0, 100, 100));

            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            document.Close(true);
            stream.Position = 0;
            File.WriteAllBytes("SignedDocument.pdf", stream.ToArray());
            
        }
    }
}