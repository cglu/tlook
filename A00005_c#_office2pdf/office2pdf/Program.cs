using Aspose.Cells;
using Aspose.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace office2pdf
{
    class Program
    {
        static void Main(string[] args)
        {

            //            -----
            //卢：基于上述的企图，又想起了一个用户提出的需求。
            //政府机关在发布文件的时候，他们往往会把不同程序的输出，合并到一个PDF中。这我们在PDF转换中可以见到很多。于是，用户问我们能否在输出PDF的时候，连带输出其他的文件，比如 XLS。
            //当然我回答说使用其他的 PDFmerge的软件就可以实现。
            //现在想来，如果我们提供一个 PDFmerge的工具，而在这个工具中加上设置PDF的各种限制，而且是默认限制的话，这样可以去限制 or 骚扰其他公司的PDF转换了。
            //所以，我想要一个这样的工具
            //1.有个UI，可以指定多个文件，每个文件都生成一个PDF
            //2.对多个PDF，进行merge，改变顺序，删除某页
            //3.生成1个PDF, 可以设置是否加密，允许打印，复制文字
           // https://docs.aspose.com/display/pdfnet/Set+Privileges%2C+Encrypt+and+Decrypt+PDF+File pdf加密
           // https://docs.aspose.com/display/cellsnet/Secure+PDF+Documents 转换加密
           // https://docs.aspose.com/display/pdfnet/Set+Privileges+on+an+Existing+PDF+File pdf权限禁止
           // https://docs.aspose.com/display/wordsnet/Home 官方文档
           //https://docs.aspose.com/display/pdfnet/Delete+PDF+pages 删除pdf页
           //https://docs.aspose.com/display/pdfnet/Append+PDF+files 合并pdf

           //Load an exiting workbook or create from scratch
           Workbook book = new Workbook("test1.xlsx");

            // Instantiate PDFSaveOptions to manage security attributes
            Aspose.Cells.PdfSaveOptions saveOption = new Aspose.Cells.PdfSaveOptions();

            saveOption.SecurityOptions = new Aspose.Cells.Rendering.PdfSecurity.PdfSecurityOptions();
            // Set the user password
            saveOption.SecurityOptions.UserPassword = "user";

            // Set the owner password
            saveOption.SecurityOptions.OwnerPassword = "owner";

            // Disable extracting content permission
            saveOption.SecurityOptions.ExtractContentPermission = false;

            // Disable print permission
            saveOption.SecurityOptions.PrintPermission = false;
            book.Save("test1.pdf", saveOption);



             Aspose.Words.Document doc = new Aspose.Words.Document("test2.docx");
            doc.Save("test2.pdf", Aspose.Words.SaveFormat.Pdf);
            using (Document document = new Document("test2.pdf"))
            {
                // Instantiate Document Privileges object
                // Apply restrictions on all privileges
                Aspose.Pdf.Facades.DocumentPrivilege documentPrivilege = Aspose.Pdf.Facades.DocumentPrivilege.ForbidAll;
                // Only allow screen reading
                documentPrivilege.AllowScreenReaders = true;

                // Encrypt the file with User and Owner password.
                // Need to set the password, so that once the user views the file with user password,
                // Only screen reading option is enabled
                document.Encrypt("user", "owner", documentPrivilege, CryptoAlgorithm.AESx128, false);
                // Save updated document
                document.Save("SetPrivileges_out.pdf");
            }



        }
    }
}
