using System;
using Microsoft.Xna.Framework.Content.Pipeline;
// TODO: replace these with the processor input and output types.

namespace MonoHzk.ContentPipeline
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentProcessor attribute to specify the correct
    /// display name for this processor.
    /// </summary>
    [ContentProcessor(DisplayName = "MonoHzk Processor")]
    public class MonoHzkProcessor : ContentProcessor<byte[], byte[]>
    {
        public override byte[] Process(byte[] input, ContentProcessorContext context)
        {
            // TODO: process the input object, and return the modified data.
            return input;
        }
    }
}