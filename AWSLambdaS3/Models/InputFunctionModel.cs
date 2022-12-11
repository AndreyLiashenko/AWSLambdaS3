namespace AWSLambdaS3.Models
{
    public class InputFunctionModel
    {
        public string BucketName { get; set; }

        public string FileName { get; set; }

        public string ColumnName { get; set; }
    }
}
