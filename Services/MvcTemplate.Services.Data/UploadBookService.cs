﻿namespace MvcTemplate.Services.Data
{
    using System;
    using System.IO;
    using System.Web;
    using MvcTemplate.Data;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;
    using VersOne.Epub;

    public class UploadBookService : IUploadBookService
    {
        private IFileParserService epubParser;

        public UploadBookService(
            IFileParserService epubParser)
        {
            this.epubParser = epubParser;
        }

        public string UploadFile(HttpPostedFileBase file)
        {
            var epubBook = EpubReader.ReadBook(file.InputStream);
            var book = this.epubParser.ParseEpubBook(epubBook);

            // save

            string resultMessage;

            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string path = @"C:\donchos\BookReader\Files\" + Path.GetFileName(file.FileName);
                    file.SaveAs(path);
                    resultMessage = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    resultMessage = "ERROR:" + ex.Message.ToString();
                }
            }
            else
            {
                resultMessage = "You have not specified a file.";
            }

            return resultMessage;
        }
    }
}