using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace MonoHzk.ContentPipeline
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentImporter attribute to specify the correct file
    /// extension, display name, and default processor for this importer.
    /// </summary>

    [ContentImporter(".bin", DisplayName = "MonoHzk Bin Importer", DefaultProcessor = "MonoHzkProcessor")]
    public class MonoHzkImporter : ContentImporter<byte[]>
    {

        public override byte[] Import(string filename, ContentImporterContext context)
        {
            // TODO: process the input object, and return the modified data.
            byte[] data;
            data = System.IO.File.ReadAllBytes(filename);
            return data;
        }

    }

}
